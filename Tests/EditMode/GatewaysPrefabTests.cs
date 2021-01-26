using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Gateways.TestUtilities;
using UnityEditor;

namespace Gateways.Tests
{
    public class GatewaysPrefabTests
    {
        string prefabPath;
        FakeGate guidBase;
        GameObject prefab;
        FakeGate guidPrefab;

        [OneTimeSetUp]
        public void Setup()
        {
            prefabPath = "Assets/TemporaryTestGuid.prefab";
            
            GameObject newGO = new GameObject("GuidTestGO");
            guidBase = newGO.AddComponent<FakeGate>();

            prefab = PrefabUtility.SaveAsPrefabAsset(guidBase.gameObject, prefabPath, out bool success);

            if (!success)
            {
                Assert.Fail("Failed to create prefab for testing");
            }

            guidPrefab = prefab.GetComponent<FakeGate>();

            Assert.IsNotNull(prefab);
            Assert.IsNotNull(guidPrefab);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            AssetDatabase.DeleteAsset(prefabPath);
        }

        [UnityTest]
        public IEnumerator GuidPrefab_HasEmptyGuid()
        {
            Assert.AreNotEqual(guidBase.GetGuid(), guidPrefab.GetGuid());
            Assert.AreEqual(guidPrefab.GetGuid(), System.Guid.Empty);

            yield return null;
        }

        [UnityTest]
        public IEnumerator GuidPrefabInstance_HasUniqueGuid()
        {
            FakeGate instance = GameObject.Instantiate<FakeGate>(guidPrefab);
            Assert.AreNotEqual(guidBase.GetGuid(), instance.GetGuid());
            Assert.AreNotEqual(instance.GetGuid(), guidPrefab.GetGuid());

            yield return null;
        }
    }
}
