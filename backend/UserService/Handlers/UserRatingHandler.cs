using AutoMapper;
using Grpc.Core;
using UserService.Service;

namespace UserService.Handlers
{
    public class UserRatingHandler : UserRatingRPC.UserRatingRPCBase
    {
        private readonly IAppUserService appUserService;
        private IMapper _mapper;
        public UserRatingHandler(IAppUserService appUserService, IMapper mapper)
        {
            this.appUserService = appUserService;
            _mapper = mapper;
        }

        public override async Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            User user = await appUserService.GetCurrentUser(request.Id);
            UserRating userRating = _mapper.Map<UserRating>(user);
            return new GetUserResponse()
            {
                User = userRating,
            };
        }
    }
}
