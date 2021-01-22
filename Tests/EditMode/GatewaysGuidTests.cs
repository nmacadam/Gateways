using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Gateways.TestUtilities;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

namespace Gateways.Tests
{
    public class GatewaysGuidTests
    {
        private TestComponent<FakeGate> _fake;

        [SetUp]
        public void Setup()
        {
            _fake = new TestComponent<FakeGate>();
        }

        [UnityTest]
        public IEnumerator GateCreation_CreatesNewGuid()
        {
            FakeGate guid1 = _fake.Instance;
            FakeGate guid2 = new TestComponent<FakeGate>().Instance;

            Assert.AreNotEqual(guid1.GetGuid(), guid2.GetGuid());

            yield return null;
        }

        [UnityTest]
        public IEnumerator GateDuplication_DetectsGuidCollision()
        {
            LogAssert.Expect(LogType.Warning, "Guid Collision Detected while creating New Game Object(Clone).\nAssigning new Guid.");

            FakeGate clone = GameObject.Instantiate<FakeGate>(_fake.Instance);

            Assert.AreNotEqual(_fake.Instance.GetGuid(), clone.GetGuid());

            yield return null;
        }

        [UnityTest]
        public IEnumerator NewGateReference_IsValid()
        {
            GateReference reference = new GateReference(_fake.Instance);
            Assert.AreEqual(reference.gameObject, _fake.Instance.gameObject);

            yield return null;
        }

        [UnityTest]
        public IEnumerator GateReference_BecomesInvalidWhenDestroyed()
        {
            var gameObject = new GameObject();
			var instance = gameObject.AddComponent<FakeGate>();
            
            GateReference reference = new GateReference(instance);
            Object.DestroyImmediate(gameObject);

            Assert.IsNull(reference.gameObject);

            yield return null;
        }
    }
}
