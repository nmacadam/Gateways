// Gateways, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using Gateways.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using Oni.SceneManagement;

namespace Gateways
{
    /// <summary>
    /// Base class for implementing a Gate
    /// </summary>
    [DisallowMultipleComponent]
	public abstract class GateBase : GuidComponent
	{
		/// <summary>
		/// This method utilizes this gate for a scene load
		/// </summary>
		public void UseGate()
		{
			WriteTargets();

			LoadScene(GetTargetSceneReference());
		}

		/// <summary>
		/// This method is invoked when this gate is the gate to start at for a scene
		/// </summary>
		public abstract void OnGateResolved();

		/// <summary>
		/// Get a scene reference for the target
		/// </summary>
		/// <returns>SceneReference for the target</returns>
		protected abstract SceneReference GetTargetSceneReference();

		/// <summary>
		/// The concrete scene loading implementation
		/// </summary>
		/// <param name="info">Info about the target scene (name, path, build index)</param>
		protected virtual void LoadScene(SceneReference sceneReference)
		{
			SceneManager.LoadScene(sceneReference.BuildIndex);
		}

		/// <summary>
		/// Write the to and from targets to the mapper
		/// </summary>
		internal abstract void WriteTargets();
	}
}