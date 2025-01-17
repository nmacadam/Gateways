﻿// Guid based Reference copyright © 2018 Unity Technologies ApS
// 
// Licensed under the Unity Companion License for Unity-dependent projects--see Unity Companion License.
// https://unity3d.com/legal/licenses/Unity_Companion_License
// 
// Unless expressly provided otherwise, the Software under this license is made available strictly on an 
// “AS IS” BASIS WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED. Please review the license for details 
// on these and other terms and conditions.

// Modified for Gateways

using UnityEditor;
using UnityEngine;
using Gateways.Internal;

namespace Gateways.Editor
{
    [CustomEditor(typeof(GuidComponent), true)]
    public class GuidComponentDrawer : UnityEditor.Editor
    {
        private GuidComponent guidComp;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (guidComp == null)
            {
                guidComp = (GuidComponent)target;
            }
        
            // Draw label
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUI.enabled = false;
            EditorGUILayout.LabelField("Guid", guidComp.GetGuid().ToString(), EditorStyles.miniLabel);
            GUI.enabled = true;
        }
    }
}