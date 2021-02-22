// Gateways, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using Gateways.Internal;
using UnityEngine;
using Oni.SceneManagement;
using UnityEngine.SceneManagement;

namespace Gateways
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class ReturnGate : GateBase
	{
		private int _previousSceneIndex = -1;
		private bool _targetsOverriden = false;

		/// <summary>
		/// Gets the Scene information for the scene that the attached gate is in
		/// </summary>
		protected override SceneReference GetTargetSceneReference()
		{
			return new SceneReference(_previousSceneIndex);
		}

		/// <summary> 
		/// Writes the gate to return to using a raw Guid. This should only be used when the game state is reset (e.g. loading from a file) 
		/// </summary>
		/// <remarks>
		/// This method will cause a Gate resolution failure if an invalid Guid is used
		/// </remarks>
		/// <param name="guid">The Guid to write as the target</param>
		public void WriteTarget(System.Guid guid)
		{
			GatewayMapper.WritePreviousGate(this);
			GatewayMapper.WriteTargetGate(guid);
			
			_previousSceneIndex = SceneManager.GetActiveScene().buildIndex;

			_targetsOverriden = true;
		}

		internal sealed override void WriteTargets()
		{
			if (_targetsOverriden) return;

			// a ReturnGate does not write a previous gate

			if (GatewayMapper.TryGetPreviousGuid(out System.Guid guid, out SceneReference scene))
			{
				GatewayMapper.WriteTargetGate(guid);
				_previousSceneIndex = scene.BuildIndex;
			}
			else
			{
				throw new GateResolutionException("There is no previous return gate to reference.");
            }
		}
	}
}