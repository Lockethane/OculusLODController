# OculusLODController
Initial protoype/proof of concept for a screen space LOD remapper, similar concept to Nvidia MultiRes in reducing wasted resources in peripheral regions.

Objects using the LODGroupInfo script test their sceen space position using the central camera of the Oculus rig, then users can assign regions of the screen to use a modified LOD transition range. There is a minimum distance threshold to make sure long/taller/large objects close to the near clip plane do not have sudden LOD jumps.

Currently researching Android performance and necessarily changes for GearVR usage.

Current performance:
Intel i5 3570k: 0.04/0.16/0.4 milliseconds with 10x10/20x20/30x30 grid with a border of 15%

Current limitations:
Modified LOD array must be same size of built in LOD set.
