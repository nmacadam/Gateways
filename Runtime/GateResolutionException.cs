// Gateways, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using System.Runtime.Serialization;

namespace Gateways
{
    public class GateResolutionException : Exception
    {
        public GateResolutionException()
        {
        }

        public GateResolutionException(string message) : base(message)
        {
        }

        public GateResolutionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GateResolutionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}