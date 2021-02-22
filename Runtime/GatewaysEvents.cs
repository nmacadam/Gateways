// Gateways, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using UnityEngine;

namespace Gateways
{
    /// <summary>
    /// Contains callbacks for customizing how Gateways handles resolution and failed resolutions
    /// </summary>
    public static class GatewaysEvents
	{
		private static System.Action _onGateUnresolved = delegate { throw new GateResolutionException("Guid did not resolve any objects"); };
		public static Action OnGateUnresolved { get => _onGateUnresolved; set => _onGateUnresolved = value; }

        private static System.Action _onGateComponentNotFound = delegate { throw new GateComponentNotFoundException("Gate component was not found on the resolved object"); };
		public static Action OnGateComponentNotFound { get => _onGateComponentNotFound; set => _onGateComponentNotFound = value; }

        private static System.Action _onTargetMissing = delegate { Debug.Log("No target ID was available for lookup"); };
		public static Action OnTargetMissing { get => _onTargetMissing; set => _onTargetMissing = value; }
	}
}