// Gateways, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Gateways.Demo
{
	[DisallowMultipleComponent]
	public class ExampleReturnGate : ReturnGate
	{
		[SerializeField] private MoveTarget _moveTarget = default;

        private void Awake()
        {
            GatewaysEvents.OnTargetMissing = () => Debug.Log("No target gate to resolve");
        }

        public override void OnGateResolved()
        {
            _moveTarget.SetPosition(transform.position);
        }
    }
}