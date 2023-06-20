using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Repository;
using UserService.Model;
using UserService.Settings;
using UserService.Exceptions;

namespace UserService.Service
{
    public class AppUserService : IAppUserService
    {
        private IMapper _mapper;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IConfiguration _configuration;
        public JwtOptions? _jwtOptions { get; private set; }
        public object JwtHelper { get; private set; }

        public AppUserService(IMapper mapper, IRepository<AppUser> userRepository, IConfiguration configuration)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AppUser> GetById(string id) => await _userRepository.FindByIdAsync(id);
        public List<AppUser> Get() => _userRepository.AsQueryable().ToList();

        public async Task<bool> CheckIfUsernameExistsAsync(string username)
        {
            AppUser user = await GetUserByUsernameAsync(username);
            if (user != null) return true;
            return false;
        }


        private async Task<AppUser> GetUserByUsernameAsync(string username) => await _userRepository.FindOneAsync(user => user.Username.Equals(username));


        public async Task RegisterUser(RegistrationUser dto)
        {
            if(await GetUserByUsernameAsync(dto.Username) != null)
            {
                throw new UserAlreadyExistsException();
            }
            dto.Password = HashPassword(dto.Password);
            AppUser user = _mapper.Map<AppUser>(dto);
            await _userRepository.InsertOneAsync(user);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);

        }

        public async Task<bool> UsernameMatchesPasswordAsync(Credentials dto)
        {
            if(dto == null)
            {
                throw new IncorrectCredentialsException();
            }

            AppUser userByEmail = await GetUserByUsernameAsync(dto.Username);
            if (userByEmail != null)
            {
                return BCrypt.Net.BCrypt.Verify(dto.Password, userByEmail.Password);
            }

            return false;
        }

        public async Task<string> LogInUserAsync(Credentials dto)
        {
            if (dto == null)
            {
                throw new IncorrectCredentialsException();
            }

            if (!await UsernameMatchesPasswordAsync(dto))
            {
                throw new IncorrectCredentialsException();
            }
            AppUser user = await GetUserByUsernameAsync(dto.Username);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Username)
            };
            return GenerateToken(claims);

        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            _jwtOptions = _configuration.GetSection(JwtOptions.Jwt).Get<JwtOptions>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityTokenHandler().WriteToken(

                new JwtSecurityToken(
                    _jwtOptions.Issuer,
                    _jwtOptions.Audience,
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: credentials
                    ));

            return token;
        }

        public async Task UpdateUser(User userDto)
        {
            AppUser user = await GetById(userDto.Id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            AppUser userByUsername = await GetUserByUsernameAsync(userDto.Username);
            if(userByUsername != null && userByUsername.Id != user.Id)
            {
                throw new UserAlreadyExistsException();
            }
            userDto.Password = HashPassword(user.Password);
            AppUser userToUpdate = _mapper.Map<AppUser>(userDto);
            await _userRepository.ReplaceOneAsync(userToUpdate);
        }

        public async Task<User> GetCurrentUser(string id)
        {
            var user = await GetById(id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return _mapper.Map<User>(user);
        }


        public async Task ChangePassword(ChangePasswordRequest request)
        {
            var user = await GetById(request.UserId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (BCrypt.Net.BCrypt.Verify(request.OldPassword, user.Password))
            {
                user.Password = HashPassword(request.NewPassword);
                await _userRepository.ReplaceOneAsync(user);
            }
            else
            {
                throw new IncorrectCredentialsException();
            }
        }
        public async Task<string> GetFullNameById(string id)
        {
            var user = await GetById(id);

            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return user.FirstName + " " + user.LastName;
        }
    }
}
