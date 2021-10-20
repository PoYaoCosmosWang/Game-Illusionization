##Hierarchical Motion Organization Illusion
Hierarchial Motion Organization is an illusion that affects how we perceive the motion of the center object.
While the center object rotates around a circle all the time, with the effect of the surrounding objects, players will think the center object moves in a oval path horizontally or vertically.
## How to start
Open the demo scene. Relative Motion Creator generates relative motion groups.
Change the center object perceived motion to other modes and see the illusion.
## Tuning Parameters
Surrounding Objects: the gameobjects that rotate around the center object. PLEASE DO NOT move the objects by yourself. 
Center Object: the gameobject that rotates in a circular path. PLEASE DO NOT move the objects by yourself.
Center Object Perceived Motion: how players will perceive the center object's motion (moves in horizontally or vertically). You can also customize it.
For Customization: changing the speeds and initial angles would give you a lot of amazing effects. Try it!

For those who want to edit our scripts:
## RelativeMotionCreator.cs
SpawnGroup (position): Generates a relative motion group.
## Move.cs
RevealPath(): Reveal the actual path. Type "R" in the demo scene.
HidePath(): Hide the actual path. Tyoe "H" in the demo scene.
 
