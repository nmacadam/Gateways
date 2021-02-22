// Gateways, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Gateways.Internal;

namespace Gateways.Editor
{
	[CustomPropertyDrawer(typeof(GateReference))]
	public class GateReferenceDrawer: PropertyDrawer 
	{
		private SerializedProperty _guidProp;
		private SerializedProperty _sceneProp;
		private SerializedProperty _nameProp;
		private SerializedProperty _lockedReference;
		private SerializedProperty _sceneRefInstance;

		// cache off GUI content to avoid creating garbage every frame in editor
    	private GUIContent _sceneLabel = new GUIContent("Containing Scene", "The target object is expected in this scene asset.");
    	private GUIContent _clearButtonGUI = new GUIContent("Clear", "Remove Cross Scene Reference");

		// add an extra line to display source scene for targets
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			_lockedReference = property.FindPropertyRelative("_sceneReference");
			return base.GetPropertyHeight(property, label) + EditorGUIUtility.singleLineHeight + EditorGUI.GetPropertyHeight(_lockedReference, true);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
		{
			_guidProp = property.FindPropertyRelative("_serializedGuid");
			_nameProp = property.FindPropertyRelative("_cachedName");
			_sceneProp = property.FindPropertyRelative("_cachedScene");
			_lockedReference = property.FindPropertyRelative("_sceneReference");
			_sceneRefInstance = _lockedReference.FindPropertyRelative("_instance");

			// Using BeginProperty / EndProperty on the parent property means that
			// prefab override logic works on the entire property.
			EditorGUI.BeginProperty(position, label, property);

			// Draw prefix label, returning the new rect we can draw in
        	var guidCompPosition = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
			guidCompPosition.height = EditorGUIUtility.singleLineHeight;

			System.Guid currentGuid;
        	GameObject currentGO = null;

			// working with array properties is a bit unwieldy
			// you have to get the property at each index manually
			byte[] byteArray = new byte[16];
			int arraySize = _guidProp.arraySize;
			for( int i = 0; i < arraySize; ++i )
			{
				var byteProp = _guidProp.GetArrayElementAtIndex(i);
				byteArray[i] = (byte)byteProp.intValue;
			}

			currentGuid = new System.Guid(byteArray);
			currentGO = GuidManager.ResolveGuid(currentGuid);
			GateBase currentGate = currentGO != null ? currentGO.GetComponent<GateBase>() : null;

			GateBase component = null;

			if (currentGuid != System.Guid.Empty && currentGate == null)
			{
				// if our reference is set, but the target isn't loaded, we display the target and the scene it is in, and provide a way to clear the reference
				Rect buttonRect;
				Rect newPosition = IMGUIUtils.SnipRectH(guidCompPosition, guidCompPosition.width - 70, out buttonRect, 6f);

				bool guiEnabled = GUI.enabled;
				GUI.enabled = false;
				EditorGUI.LabelField(newPosition, new GUIContent(_nameProp.stringValue, "Target GameObject is not currently loaded."), EditorStyles.objectField);
				GUI.enabled = guiEnabled;

				if (GUI.Button(buttonRect, _clearButtonGUI, EditorStyles.miniButton))
				{
					ClearPreviousGuid();
				}
			}
			else
			{
				// if our object is loaded, we can simply use an object field directly
				component = EditorGUI.ObjectField(guidCompPosition, currentGate, typeof(GateBase), true) as GateBase;
			}
			
			if (currentGate != null && component == null)
			{
				// clear out an old Guid after the user has changed/removed a reference in inspector
				ClearPreviousGuid();
			}

			// if we have a valid reference, draw the scene name of the scene it lives in so users can find it
			if (component != null)
			{
				_nameProp.stringValue = component.name;
				string scenePath = component.gameObject.scene.path;
				SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
				_sceneProp.objectReferenceValue = sceneAsset;

				Scene scene = EditorSceneManager.GetSceneByName(((SceneAsset)_sceneProp.objectReferenceValue).name);

				_sceneRefInstance.FindPropertyRelative("_sceneAsset").objectReferenceValue = sceneAsset;
				_sceneRefInstance.FindPropertyRelative("_sceneName").stringValue = scene.name;
				_sceneRefInstance.FindPropertyRelative("_scenePath").stringValue = scene.path;
				_sceneRefInstance.FindPropertyRelative("_buildIndex").intValue = scene.buildIndex;

				// only update the GUID Prop if something changed. This fixes multi-edit on GUID References
				if (component != currentGate)
				{
					byteArray = component.GetGuid().ToByteArray();
					arraySize = _guidProp.arraySize;
					for (int i = 0; i < arraySize; ++i)
					{
						var byteProp = _guidProp.GetArrayElementAtIndex(i);
						byteProp.intValue = byteArray[i];
					}
				}
			}

			position.y += EditorGUIUtility.singleLineHeight + 2;
			Rect sceneFieldPosition = new Rect(position);

			sceneFieldPosition.height = EditorGUI.GetPropertyHeight(_lockedReference, true);

			EditorGUI.PropertyField(sceneFieldPosition, _lockedReference, true);
		
			EditorGUI.EndProperty();
		}

		void ClearPreviousGuid()
		{
			_nameProp.stringValue = string.Empty;
			_sceneProp.objectReferenceValue = null;

			_sceneRefInstance.FindPropertyRelative("_sceneAsset").objectReferenceValue = null;
			_sceneRefInstance.FindPropertyRelative("_sceneName").stringValue = string.Empty;
			_sceneRefInstance.FindPropertyRelative("_scenePath").stringValue = string.Empty;
			_sceneRefInstance.FindPropertyRelative("_buildIndex").intValue = -1;

			int arraySize = _guidProp.arraySize;
			for (int i = 0; i < arraySize; ++i)
			{
				var byteProp = _guidProp.GetArrayElementAtIndex(i);
				byteProp.intValue = 0;
			}
		}
	}
}