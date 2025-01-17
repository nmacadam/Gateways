﻿// Gateways, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Gateways.TestUtilities
{
	/// <summary>
    /// Creates a GameObject with a component of the given type; its GameObject is automatically destroyed at the end of the 
	/// TestComponent's lifecycle
    /// </summary>
	/// <remarks> 
	/// This component is meant for testing; it provides a simple way to create an instance of component that automatically 
	/// cleans up after itself
	/// </remarks>
    /// <typeparam name="T">Component type to generate</typeparam>
    public class TestComponent<T> where T : MonoBehaviour
    {
        private GameObject _gameObject;
		private T _instance;

        public T Instance => _instance;

        public TestComponent()
		{
			_gameObject = new GameObject();
			_instance = _gameObject.AddComponent<T>();
		}

		~TestComponent()
		{
			UnityEngine.Object.Destroy(_gameObject);
		}
    }
}