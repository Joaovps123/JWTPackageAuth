using MediatR;
using POC.Authentication.Application.DTOs;
using POC.Authentication.Domain.Interfaces;

namespace POC.Authentication.Application.Queries.GetUserInfo
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserInfoDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserInfoQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserInfoDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.UserId);
            return new UserInfoDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
