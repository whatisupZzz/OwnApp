using Notification.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.GrpcServer.Services
{
    public class SmsService : ISmsService
    {
        public Task<SendSmsReply> SendSmsAsync(string phone, SmsTemplate type)
        {
            var smsCode = new Random().Next(100000, 999999).ToString();
            var messageId = Guid.NewGuid().ToString();
            // 根据模板 ID 处理逻辑（不同模板可以不同内容）
            switch (type)
            {
                case SmsTemplate.LoginCode:
                    Console.WriteLine($"发送登录验证码短信到 {phone}, code={smsCode}");
                    break;

                case SmsTemplate.RegisterCode:
                    Console.WriteLine($"发送注册验证码短信到 {phone}, code={smsCode}");
                    break;

                case SmsTemplate.ResetPassword:
                    Console.WriteLine($"发送重置密码验证码短信到 {phone}, code={smsCode}");
                    break;

                default:
                    Console.WriteLine($"默认模板： {phone}, code={smsCode}");
                    break;
            }
            // TODO: send sms
            return Task.FromResult(new SendSmsReply() { Success = true, SmsCode = "1111", MessageId = messageId });
        }



    }
}
