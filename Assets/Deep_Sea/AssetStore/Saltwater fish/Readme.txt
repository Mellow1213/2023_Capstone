If you want to move around in the scene, you will need to add an FPS controller.  You can you use the one included in the standard assets.
In the Unity editor, go to Assets < Import Package < Characters, and import all the assets.
In the project window, go to Assets < Standard Assets < Characters < FirstPersonCharacter < Prefabs , and coose the FPSController and add it to the scenea at 0,0,0.

USING THE SCRIPTS
The package contains Shoal and ShoalManager scripts.  ShoalManager is the spawner, and needs to be attached to an empty GameObject. The fish will spawn around
this GameObject. Drag your terrain object into the Terrain field in the inspector, drag your fish prefab into the Fish Prefab field (the fish prefab must have the 
Shoal script attached to it in order to work properly), drag your water plane object into the Water Plane field , the swim limits (the y limit is not important, as it will be determined by the terrain
level and water level) and set Num Fish to be the number of fish you want spawned.  Finally, set the minimum and maximum speed.  You can ignore the other fields, as 
they will be controlled by the script.  You can also ignore the settings in the Shoal script, as they are also controlled within the script.

The attack.cs and PlayerHealth.cs scripts are used in the attack demo.  The PlayerHealth script goes on to the player game object (the player needs to be tagged as MainCamera for the attack script to work).
The attack script works with animation events (damage is dealt at a specific animation frame).  These scripts are intended for the demo, but may be useful elsewhere.

