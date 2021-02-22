// Gateways, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Oni.SceneManagement;

namespace Gateways.Internal
{
    /// <summary>
    /// Handles looking up IDs and handing off resolved Gates
    /// </summary>
    internal static class GatewayMapper
	{
        private static bool _hasTarget;
		private static Guid _target;

        private static bool _hasPrevious;
		private static Guid _previous;
        private static SceneReference _previousSceneReference;

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            SceneManager.activeSceneChanged += TryResolveTargetGate;
        }

        public static void WritePreviousGate(GateReference gatewayReference)
        {
            WritePreviousGate(gatewayReference.Guid);
        }

        public static void WritePreviousGate(GateBase gate)
        {
            WritePreviousGate(gate.GetGuid());
        }

        public static void WritePreviousGate(Guid guid)
        {
            _hasPrevious = true;
            _previousSceneReference = new SceneReference(SceneManager.GetActiveScene().buildIndex);
            _previous = guid;
        }

        public static bool TryGetPreviousGuid(out Guid previous, out SceneReference scene)
        {
            previous = _previous;
            scene = _previousSceneReference;
            return _hasPrevious;
        }

        public static void WriteTargetGate(GateReference gatewayReference)
        {
            WriteTargetGate(gatewayReference.Guid);
        }

        public static void WriteTargetGate(GateBase gate)
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
                GatewaysEvents.OnTargetMissing();
                return;
            }

            GameObject gameObject = GuidManager.ResolveGuid(_target);
            if (gameObject == null)
            {
                GatewaysEvents.OnGateUnresolved();
                return;
            }

            GateBase gate = gameObject.GetComponent<GateBase>();
            if (gate == null)
            {
                GatewaysEvents.OnGateComponentNotFound();
                return;
            }

            _hasTarget = false;
            _target = Guid.Empty;

            gate.OnGateResolved();
		}
	}
}