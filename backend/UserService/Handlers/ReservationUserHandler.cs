using Grpc.Core;
using UserService.Service;

namespace UserService.Handlers
{
    public class ReservationUserHandler : ReservationUserRPC.ReservationUserRPCBase
    {
        private readonly IAppUserService _userService;
        public ReservationUserHandler(IAppUserService userService)
        {
            _userService= userService;
        }

        public override async  Task<GetNameByIdResponse> GetNameById(GetNameByIdRequest request, ServerCallContext context)
        {
            string name = await _userService.GetFullNameById(request.Id);
            return new GetNameByIdResponse() { FullName= name };
        }
    }
}
