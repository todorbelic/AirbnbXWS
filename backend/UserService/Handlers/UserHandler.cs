using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using UserService.Service;

namespace UserService.Handlers
{
    public class UserHandler : UserServiceRPC.UserServiceRPCBase
    {
      
        private readonly IAppUserService _appUserService;

        public UserHandler(IAppUserService appUserService)
        {
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

        [Authorize]
        public override async Task<EditUserProfileResponse> EditUserProfile(EditUserProfileRequest request, ServerCallContext context)
        {
            await _appUserService.UpdateUser(request.User);
            return new EditUserProfileResponse()
            {
                Response = ""
            };
        }

        public override async Task<GetCurrentUserResponse> GetCurrentUser(GetCurrentUserRequest request, ServerCallContext context)
        {
            User user = await _appUserService.GetCurrentUser(request.Id);
            return new GetCurrentUserResponse()
            {
                User = user
            };
        }
        [Authorize]
        public override async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request, ServerCallContext context)
        {
            await _appUserService.ChangePassword(request);
            return new ChangePasswordResponse();
        }
    }
}