// Guid based Reference copyright © 2018 Unity Technologies ApS
// 
// Licensed under the Unity Companion License for Unity-dependent projects--see Unity Companion License.
// https://unity3d.com/legal/licenses/Unity_Companion_License
// 
// Unless expressly provided otherwise, the Software under this license is made available strictly on an 
// “AS IS” BASIS WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED. Please review the license for details 
// on these and other terms and conditions.

// Modified for Gateways, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;
using System;
using Gateways.Internal;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gateways
{
	public struct SceneInfo
	{
		private string _sceneName;
		private string _scenePath;
		private int _buildIndex;

        public string SceneName => _sceneName;
        public string ScenePath => _scenePath;
        public int BuildIndex => _buildIndex;

		public SceneInfo(string name, string path, int buildIndex)
		{
			_sceneName = name;
			_scenePath = path;
			_buildIndex = buildIndex;
		}
    }

    [System.Serializable]
    public class GateReference : ISerializationCallbackReceiver
    {
		// cache the referenced Game Object if we find one for performance
		private GameObject _cachedGameObject;
		private bool _isGameObjectCacheSet;

		private Gate _cachedReference;
		private bool _isComponentCacheSet;

		[SerializeField] private LockedSceneReference _sceneReference;

		// store our GUID in a form that Unity can save
		[SerializeField]
		private byte[] _serializedGuid;
		private System.Guid _guid;

#if UNITY_EDITOR
		// decorate with some extra info in Editor so we can inform a user of what that GUID means
		[SerializeField] private string _cachedName;
		[SerializeField] private SceneAsset _cachedScene;
#endif

		// Set up events to let users register to cleanup their own cached references on destroy or to cache off values
		public event Action<GameObject> OnGuidAdded = delegate (GameObject go) { };
		public event Action OnGuidRemoved = delegate() { };

		// create concrete delegates to avoid boxing. 
		// When called 10,000 times, boxing would allocate ~1MB of GC Memory
		private Action<GameObject> addDelegate;
		private Action removeDelegate;

		// optimized accessor, and ideally the only code you ever call on this class
		public GameObject gameObject
		{
			get
			{
				if( _isGameObjectCacheSet )
				{
					return _cachedGameObject;
				}

				_cachedGameObject = GuidManager.ResolveGuid(_guid, addDelegate, removeDelegate);
				_isGameObjectCacheSet = true;
				return _cachedGameObject;
			}
		}

		public Gate Gateway
		{
			get
			{
				if (_isComponentCacheSet && _isGameObjectCacheSet)
				{
					return _cachedReference;
				}

				return gameObject.GetComponent<Gate>();
			}
		}

		public Guid Guid
		{
			get
			{
				if (_guid == Guid.Empty)
				{
					if (_serializedGuid != null || _serializedGuid.Length == 0)
					{
						return new Guid(_serializedGuid);
					}
					else
					{
						Debug.LogError("No guid found");
						return Guid.Empty;
					}
				}
				
				return _guid;
			}
		}

		public SceneInfo GetSceneInfo()
		{
			return new SceneInfo(_sceneReference.SceneName, _sceneReference.ScenePath, _sceneReference.BuildIndex);
		}

		public GateReference() { }

		public GateReference(GuidComponent target)
		{
			_guid = target.GetGuid();
		}

		private void GuidAdded(GameObject go)
		{
			_cachedGameObject = go;
			OnGuidAdded(go);
		}

		private void GuidRemoved()
		{
			_cachedGameObject = null;
			_isGameObjectCacheSet = false;

			_cachedReference = null;
			_isComponentCacheSet = false;

			OnGuidRemoved();
		}

        //convert system guid to a format unity likes to work with
		public void OnBeforeSerialize()
		{
			_serializedGuid = _guid.ToByteArray();
		}

		// convert from byte array to system guid and reset state
		public void OnAfterDeserialize()
		{
			_cachedGameObject = null;
			_isGameObjectCacheSet = false;

			_cachedReference = null;
			_isComponentCacheSet = false;
			
			if (_serializedGuid == null || _serializedGuid.Length != 16)
			{
				_serializedGuid = new byte[16];
			}
			_guid = new System.Guid(_serializedGuid);
			addDelegate = GuidAdded;
			removeDelegate = GuidRemoved;
		}
    }
}