// Gateways, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using Gateways.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using Oni.SceneManagement;

namespace Gateways
{
	[DisallowMultipleComponent]
    public abstract class Gate : GuidComponent
	{
		[SerializeField] private GateReference _attachedGate = new GateReference();

		private static System.Action _onGateUnresolved = delegate{};
        public static Action OnGateUnresolved { get => _onGateUnresolved; set => _onGateUnresolved = value; }

        public static void HandleGateUnresolved()
		{
			_onGateUnresolved.Invoke();
		}

		/// <summary>
		/// This method utilizes this gate for a scene load
		/// </summary>
		public void UseGate()
		{
			GatewayMapper.WriteTargetGate(_attachedGate);
			LoadScene(GetAttachedSceneReference());
		}

		/// <summary>
		/// This method is invoked when this gate is the gate to start at for a scene
		/// </summary>
		public abstract void OnGateResolved();

		/// <summary>
		/// The concrete scene loading implementation
		/// </summary>
		/// <param name="info">Info about the target scene (name, path, build index)</param>
		protected virtual void LoadScene(SceneReference sceneReference)
		{
			SceneManager.LoadScene(sceneReference.BuildIndex);
		}

		/// <summary>
		/// Gets the Scene information for the scene that the attached gate is in
		/// </summary>
		protected SceneReference GetAttachedSceneReference()
		{
			return _attachedGate.SceneReference;
		}
	}
}