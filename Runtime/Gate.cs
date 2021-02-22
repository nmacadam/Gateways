// Gateways, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using Gateways.Internal;
using UnityEngine;
using Oni.SceneManagement;

namespace Gateways
{
    /// <summary>
    /// Defines a scene-independent entry/exit point from one location to another
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class Gate : GateBase
	{
		[SerializeField] private GateReference _attachedGate = new GateReference();

		internal sealed override void WriteTargets()
		{
			GatewayMapper.WritePreviousGate(this.GetGuid());
			GatewayMapper.WriteTargetGate(_attachedGate);
		}

		/// <summary>
		/// Gets the Scene information for the scene that the attached gate is in
		/// </summary>
		protected override SceneReference GetTargetSceneReference()
		{
			return _attachedGate.SceneReference;
		}
	}
}