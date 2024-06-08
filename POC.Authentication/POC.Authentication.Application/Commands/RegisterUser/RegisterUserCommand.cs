using MediatR;

namespace POC.Authentication.Application.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
