using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class BadRequestExpection:Exception
    {
        public BadRequestExpection(string message):base(message)
        {
        }

        public BadRequestExpection(string message, string details):base(message)
        {
            Details = details;
        }
        public string? Details { get; }
    }
}
