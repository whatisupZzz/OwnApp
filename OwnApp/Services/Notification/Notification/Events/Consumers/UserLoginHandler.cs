using BuildingBlocks.Message.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Events.Consumers
{
    public class UserLoginHandler(ILogger<UserLoginHandler> logger) : IConsumer<UserLogined>
    {
        public async Task Consume(ConsumeContext<UserLogined> context)
        {           
            logger.LogInformation("用户登录: {UserName}", context.Message.UserName);           
        }
    }
}
