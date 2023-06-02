using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using UserService.Service;

namespace UserService.Handlers
{
    public class UserHandler : UserServiceRPC.UserServiceRPCBase
    {
        private readonly ILogger<UserHandler> _logger;
        private readonly IAppUserService _appUserService;

        public UserHandler(ILogger<UserHandler> logger, IAppUserService appUserService)
        {
            _logger = logger;
            _appUserService = appUserService;
        }

        public override async Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
        {
            await _appUserService.RegisterUser(request.User);
            return new RegisterResponse()
            {
                Response = ""
            };
        }

        public override async Task<LogInResponse> LogIn(LogInRequest request, ServerCallContext context)
        {
            string accessToken = await _appUserService.LogInUserAsync(request.Credentials);
            return new LogInResponse()
            {
                AccessToken = accessToken
            };
        }

        [Authorize(Roles = "GUEST")]
        public override async Task<EditUserProfileResponse> EditUserProfile(EditUserProfileRequest request, ServerCallContext context)
        {
            await _appUserService.UpdateUser(request.User);
            return new EditUserProfileResponse()
            {
                Response = ""
            };
        }
    }
}