﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Exceptions
{
    public class ForbidException : Exception
    {
        public ForbidException() { }
        public ForbidException(string message) : base(message)
        {
        }
    }
}