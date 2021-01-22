namespace Gateways.TestUtilities
{
    /// <summary>
    /// Testable concrete implementation of Gate
    /// </summary>
	public class FakeGate : Gate
    {
        public override void OnGateResolved()
        {

        }

        public SceneInfo GetAttachedSceneInfoTest()
		{
			return GetAttachedSceneInfo();
		}
    }
}