﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class InternalSeverExpection:Exception
    {
        public InternalSeverExpection(string message):base(message)
        {
        }
        public InternalSeverExpection(string message, string details):base(message)
        {
            Details = details;
        }
        public string? Details { get; }
    }
}
