// Gateways, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using Gateways.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gateways
{
	[DisallowMultipleComponent]
    public abstract class Gate : GuidComponent
	{
		[SerializeField] private GateReference _attachedGate = new GateReference();

		/// <summary>
		/// This method utilizes this gate for a scene load
		/// </summary>
		public virtual void UseGate()
		{
			GatewayMapper.WriteTargetGate(_attachedGate);
			SceneManager.LoadScene(_attachedGate.GetSceneInfo().BuildIndex);
		}

		/// <summary>
		/// This method is invoked when this gate is the gate to start at for a scene
		/// </summary>
		public abstract void OnGateResolved();

		/// <summary>
		/// Gets the Scene information for the scene that the attached gate is in
		/// </summary>
		protected SceneInfo GetAttachedSceneInfo()
		{
			return _attachedGate.GetSceneInfo();
		}
	}
}