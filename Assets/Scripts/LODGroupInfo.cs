using UnityEngine;
using System.Collections;
using System.Reflection;

namespace VRLODModifier
{ 

    public class LODGroupInfo : MonoBehaviour {

	    //Camera viewing this object
	    public Camera cam;
	    //Cached Transform of LOD parent(This object)
        Transform cachedTransform;
	    //Cached LODGroup component(This object)
	    LODGroup lodGroup;
	    //Cached LODs
        LOD[] lods;
	    //Original LOD values
	    float[] cachedTransitionsLods;
	    //Modified LOD ranges
	    public float[] customLODs;
	    //Whether or not it is in regular LOD mode
	    public bool usingOriginalLod = true;
    
	    //Maximum threshold in percentage of the screen from the border of the screen
	    //Order is clockwise. X:Left, Y:Top, Z:Right, W:Bottom
	    //0-1 value from border
	    public Vector4 screenPercentageBorder = new Vector4(0.0f,0.0f,0.0f,0.0f);

	    //Minimun distance in meters from camera before applying modifiers 
	    public float minActivationDistance = 1.0f;

        public void Start()
        {

		    //Get the LODGroup controller to get manipulate LOD ranges
            lodGroup = GetComponent<LODGroup>();

		    //Error if no LODGroup controller is on this object
		    if (lodGroup == null)
			    Debug.LogError ("No LODGroup found", this);

		    //Cache LOD objects
		    lods = lodGroup.GetLODs();

		    //Error if the number of transitions do not match
		    if (lods.Length != customLODs.Length)
			    Debug.LogError ("Inconsistent number of LOD transitions detected", this);

		    //Error if the LOD transitions are out of order
		    for (int testIndex=0; testIndex<customLODs.Length; ++testIndex)
		    {
			    if(testIndex > 0)
			    {
				    if(customLODs[testIndex] > customLODs[testIndex-1])
				    {
					    Debug.LogError("LOD transitions are out of order", this);
				    }
			    }
		    }

		    //Save the original LOD transition levels for when the object is front and center
		    cachedTransitionsLods = new float[lods.Length];
            for(int index=0; index < customLODs.Length; ++index) 
			    cachedTransitionsLods[index] = lods[index].screenRelativeTransitionHeight;

            cachedTransform = transform;
        }
        
        public void LODUpdate() 
	    {
            //Get the screen space of the object
            Vector3 screenPos = cam.WorldToScreenPoint(cachedTransform.position);
		
            //Get the 0-1 percent of where the object is on the screen from the left/bottom
            //Clamp to 0-1 since if the LOD is outside the screen, the system shouldn't care
		    float screenPercentPositionX = Mathf.Clamp01( screenPos.x / Screen.width);
		    float screenPercentPositionY = Mathf.Clamp01( screenPos.y / Screen.height);

		    //Is the object on the borders set by the threshold
		    bool outsideThreshold = (screenPercentPositionX < screenPercentageBorder.x || screenPercentPositionX > (1.0f - screenPercentageBorder.z)) ||
								    (screenPercentPositionY < screenPercentageBorder.y || screenPercentPositionY > (1.0f - screenPercentageBorder.w));

		    //Is the object far enough away from the camera
		    bool outside_range_border = screenPos.z > minActivationDistance;

		    //Test whether or not to use custom transitions
		    if (outside_range_border && outsideThreshold)
			    SetLods (customLODs, false);
		    else
			    SetLods (cachedTransitionsLods, true);
        }

	    /// <summary>
	    /// Helper function to set LODs only when the conditions change
	    /// </summary>
	    /// <param name="lodTransitions">New set of LOD transition ranges</param>
	    /// <param name="originaLOD">Wether it is the original set of LOD transition ranges</param>
	    private void SetLods(float[] lodTransitions, bool originaLOD)
	    {
		    //Camera conditions haven't changed between thresholds
		    if (usingOriginalLod == originaLOD)
			    return;

		    //Change the screen perecentage that the object has to be for the lod to take place
		    for(int index=0; index < lodTransitions.Length; ++index)
		    {
			    lods[index].screenRelativeTransitionHeight = lodTransitions[index];
		    }
		    //Update the objects lod manager
		    lodGroup.SetLODs(lods);

		    //Toggle wether or not the object is using original LOD's to shortcut calling SetLODs
		    usingOriginalLod = originaLOD;
	    }
    }
}