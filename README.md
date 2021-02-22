![Gateways Logo](/Documentation~/gateways_logo_web.png "Logo")
<hr>
Gateways is a cross-scene door reference system for Unity.  It provides a simple system for linking doors/transition points from one scene to another through the inspector.

## Quick Start
Create a class the inherits from Gateway's `Gate` class.  Use the `UseGate` and `OnGateResolved` methods to implement the scene load and what should happen when the scene finishes loading to the current door.

```C#
using Gateways;

public class MyGate : Gate
{
  public override void OnGateResolved()
  {
    // Called when this gate is the one resolved after the scene changes
    // Use this method to move your character to this gate's position
  }
  
  // You can also override the LoadScene method to implement
  // your own scene-loading solution
}
```
Add the `MyGate` component to the doorways in your scenes, then open both scenes at once.  Drag and drop the `Gate` components to their respective output `Gate`s.  Don't worry–The references being displayed are actually serialized unique identifiers!  They will persist after you close the scenes, and are how Gateways will resolve connections between scenes.
<br><br>
![Gateways example](/Documentation~/gateways_example.gif "Example")

## Customization
### Resolution Error Handling
The `GatewaysEvents` class contains a number of callbacks for handling various errors that may arise from failing to resolve a gate.  These include:

- `OnGateUnresolved`: Invoked when the target Guid does not resolve an object.  Throws a `GateResolutionException` by default.
- `OnGateComponentNotFound`: Invoked when a component of type GateBase is not found on the resolved object.  Throws a `GateComponentNotFoundException` by default.
- `OnTargetMissing`: Invoked when no target Guid is available for lookup; usually occurs when a scene is first loaded in game without a Gate.

### Additional Gate Types
Use a `ReturnGate` to create a door that links back to the last door that was used.  This can be used for creating doorways to scenes that are reused, like a shop, or world-hub.

### Scene Loading
Both `Gate` and `ReturnGate` have an overridable `LoadScene` method you can use to integrate Gateways into your own scene-loading code.

## Installation
### Git
This package can be installed with the Unity Package Manager by selecting the add package dropdown, clicking "Add package from git url...", and entering `https://github.com/nmacadam/Gateways.git`.

Alternatively the package can be added directly to the Unity project's manifest.json by adding the following line:
```
{
  "dependencies": {
      ...
      "com.daruma-works.gateways":"https://github.com/nmacadam/Gateways.git"
      ...
  }
}
```
### Manual
Download this repository as a .zip file and extract it, open the Unity Package Manager window, and select "Add package from disk...".  Then select the package.json in the extracted folder.

## License
Gateways utilizes code from Unity's [Guid Based Reference](https://github.com/Unity-Technologies/guid-based-reference) repository ([license](https://unity3d.com/legal/licenses/Unity_Companion_License)) and implements a modified version of the SceneReference code found in [Unity Atoms](https://github.com/AdamRamberg/unity-atoms) ([license](https://github.com/AdamRamberg/unity-atoms/blob/master/LICENSE.md)). All other code is explicitly part of Gateways and is licensed under the [MIT license](https://github.com/nmacadam/Gateways/blob/main/LICENSE.md).
