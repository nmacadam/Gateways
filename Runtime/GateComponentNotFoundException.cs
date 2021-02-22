// Gateways, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using System.Runtime.Serialization;

namespace Gateways
{
    public class GateComponentNotFoundException : Exception
    {
        public GateComponentNotFoundException()
        {
        }

        public GateComponentNotFoundException(string message) : base(message)
        {
        }

        public GateComponentNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GateComponentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}