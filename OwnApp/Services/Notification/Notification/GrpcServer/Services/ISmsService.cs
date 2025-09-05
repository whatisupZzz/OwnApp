using Notification.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.GrpcServer.Services
{
    public interface ISmsService
    {
        Task<SendSmsReply> SendSmsAsync(string phone, SmsTemplate type);
    }
}
