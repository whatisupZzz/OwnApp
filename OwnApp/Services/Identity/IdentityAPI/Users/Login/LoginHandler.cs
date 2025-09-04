using BuildingBlocks.CQRS;
using BuildingBlocks.JWT;
using FluentValidation;
using IdentityAPI.Repository.Repo;
using MediatR;
using System.Windows.Input;

namespace IdentityAPI.Users.Login
{

    public record LoginCommand(string PhoneNumber, string CmsCode) : ICommand<LoginResult>;
    public record LoginResult(string Token, string RefreshToken);

    public class LoginCommandValidator
    : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.PhoneNumber).NotNull().WithMessage("请输入手机号");
            RuleFor(x => x.CmsCode).NotEmpty().WithMessage("请输入验证码");
        }
    }

    public class LoginHandler(IUserRepository userRepository, IJwtService jwtService) : ICommandHandler<LoginCommand, LoginResult>
    {
        public async Task<LoginResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetUserByPhoneAsync(command.PhoneNumber, cancellationToken);
            var token = jwtService.GenerateToken(user.Id, user.PhoneNumber);
            var refreshToken = jwtService.GenerateRefreshToken(user.Id);
            var res = new LoginResult(token, refreshToken);
            return res;
        }
    }
}
