
using Notification.Grpc;

namespace IdentityAPI.Users.Login
{
    public record SendSmsCommand(string PhoneNumber,int SmsType) : ICommand<SendSmsResult>;
    public record SendSmsResult(bool IsSuccess);
    public class SendSmsHandler(IUserRepository userRepository,NotificationService.NotificationServiceClient notification) : ICommandHandler<SendSmsCommand, SendSmsResult>
    {
        public async Task<SendSmsResult> Handle(SendSmsCommand command, CancellationToken cancellationToken)
        {
            var request = new SendSmsRequest
            {
                PhoneNumber = command.PhoneNumber
                ,
                TemplateId = (SmsTemplate)command.SmsType
            };
            var smsReply = await notification.SendSmsAsync(request);
            if (smsReply.Success)
            {
                await userRepository.UpdateOrInsertUserAsync(command.PhoneNumber, smsReply.SmsCode, cancellationToken);
            }
            return new SendSmsResult(smsReply.Success);

            
        }
    }
}
