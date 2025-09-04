using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class NotFoundExpection :Exception
    {
        public NotFoundExpection(string message) : base(message)
        {
        }
        public NotFoundExpection(string name, object value) : base($"{name}:{value}未找到")
        {
        }
    }
}
