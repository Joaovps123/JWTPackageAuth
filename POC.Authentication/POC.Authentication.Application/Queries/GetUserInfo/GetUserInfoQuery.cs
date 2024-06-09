using MediatR;
using POC.Authentication.Application.DTOs;

namespace POC.Authentication.Application.Queries.GetUserInfo
{
    public class GetUserInfoQuery : IRequest<UserInfoDto>
    {
        public long UserId { get; set; }

        public GetUserInfoQuery(long userId)
        {
            UserId = userId;
        }
    }
}
