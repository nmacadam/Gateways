// Gateways, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Gateways.Demo
{
	[DisallowMultipleComponent]
	public class ExampleGateway : Gate
	{
		[SerializeField] private MoveTarget _moveTarget = default;
        
        private void Awake()
        {
            Gate.OnGateUnresolved = () => Debug.Log("No matching gate");
        }


        public override void OnGateResolved()
        {
            _moveTarget.SetPosition(transform.position);
        }
    }
}