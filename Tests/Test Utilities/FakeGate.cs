using Oni.SceneManagement;

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

        public SceneReference GetAttachedSceneInfoTest()
		{
			return GetAttachedSceneReference();
		}
    }
}