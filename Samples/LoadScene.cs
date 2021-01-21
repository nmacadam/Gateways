// Gateways, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gateways.Demo
{
	public class LoadScene : MonoBehaviour
	{
		public void Load(int index)
		{
			SceneManager.LoadScene(index);
		}
	}
}