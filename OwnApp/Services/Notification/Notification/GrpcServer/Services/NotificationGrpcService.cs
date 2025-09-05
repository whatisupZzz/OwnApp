using Grpc.Core;
using Notification.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.GrpcServer.Services
{
    public class NotificationGrpcService(ISmsService smsService):NotificationService.NotificationServiceBase
    {
        public override async Task<SendSmsReply> SendSms(SendSmsRequest request, ServerCallContext context)
        {
            var res = await smsService.SendSmsAsync(request.PhoneNumber, request.TemplateId);
            return res;
        }
    }
}
