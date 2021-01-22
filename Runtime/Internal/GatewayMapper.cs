// Gateways, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gateways.Internal
{
    internal static class GatewayMapper
	{
        private static bool _hasTarget;
		private static Guid _target;

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            SceneManager.activeSceneChanged += TryResolveTargetGate;
        }

        public static void WriteTargetGate(GateReference gatewayReference)
        {
            WriteTargetGate(gatewayReference.Guid);
        }

        public static void WriteTargetGate(Gate gate)
        {
            WriteTargetGate(gate.GetGuid());
        }

		public static void WriteTargetGate(Guid guid)
		{
            _hasTarget = true;
            _target = guid;
		}

		private static void TryResolveTargetGate(Scene a, Scene b)
		{
            if (!_hasTarget)
            {
                return;
            }

            GameObject gameObject = GuidManager.ResolveGuid(_target);
            if (gameObject == null)
            {
                UnityEngine.Debug.LogError("There is no GameObject with the stored guid");
                return;
            }

            Gate gate = gameObject.GetComponent<Gate>();
            if (gate == null)
            {
                UnityEngine.Debug.LogError("There is no Gateway with the stored guid");
                return;
            }

            _hasTarget = false;
            _target = Guid.Empty;

            gate.OnGateResolved();
		}
	}
}