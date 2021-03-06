﻿using System;
using FlexiMvvm.Exceptions;

namespace Company.App.Infrastructure.Connectivity
{
    public class ConnectivityException : Exception, IUserFriendlyException
    {
        public ConnectivityException(string message)
            : base(message)
        {
        }
    }
}
