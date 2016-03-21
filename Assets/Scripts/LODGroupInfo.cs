using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

namespace VRLODController
{ 

    public class LODGroupInfo : MonoBehaviour {

        public enum ThresholdType
        {
            Square = 0,
            Circle = 1
        }
        
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
	    bool usingOriginalLod = true;
        public bool UsingOriginalLod
        {
            get { return usingOriginalLod; }
        }

        public ThresholdType thresholdType = ThresholdType.Square;

        //Maximum threshold in percentage of the screen from the border of the screen
        //Order is clockwise. X:Left, Y:Top, Z:Right, W:Bottom
        //0-.5 value from left/bottom border to center, .5-1 from center to top/right border
        public Vector4 screenPercentageBorder = new Vector4(0.0f,0.0f,0.0f,0.0f);

        //Used to calculate if object is inside circular threshold
        [Range(0.0f,1.0f)]
        public float distanceFromCenter = 1.0f;

	    //Minimun distance in meters from camera before applying modifiers 
	    public float minActivationDistance = 1.0f;

        private Vector2 screenCenter;

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

            screenCenter = new Vector2((Screen.width / 2) / (float)Screen.width, (Screen.height / 2) / (float)Screen.height);
        }
        
        public void LODUpdate(Camera cam) 
	    {
            //Get the screen space of the object
            Vector3 screenPos = cam.WorldToScreenPoint(cachedTransform.position);

            //Is the object on the borders set by the threshold
            bool outsideThreshold = false;

            switch (thresholdType)
            {
                case ThresholdType.Square:
                    outsideThreshold = OutsideThresholdRect(screenPos);
                    break;
                case ThresholdType.Circle:
                    outsideThreshold = OutsideThresholdCircle(screenPos);
                    break;
                default:
                    Debug.LogError("No function defined");
                    break;
            }

		    //Is the object far enough away from the camera
		    bool outside_range_border = Mathf.Abs(screenPos.z) > minActivationDistance;

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

        /// Defined Threshold functions
        private bool OutsideThresholdRect(Vector3 screenPos)
        {
            //Get the 0-1 percent of where the object is on the screen from the left/bottom
            //Clamp to 0-1 since if the LOD is outside the screen, the system shouldn't care
            float screenPercentPositionX = Mathf.Clamp01(screenPos.x / Screen.width);
            float screenPercentPositionY = Mathf.Clamp01(screenPos.y / Screen.height);

            //Is the object on the borders set by the threshold
            bool outsideThreshold = (screenPercentPositionX < screenPercentageBorder.x || screenPercentPositionX > (screenPercentageBorder.z)) ||
                                    (screenPercentPositionY > screenPercentageBorder.y || screenPercentPositionY < (screenPercentageBorder.w));

            return outsideThreshold;
        }

        private bool OutsideThresholdCircle(Vector3 screenPos)
        {
            //Get the 0-1 percent of where the object is on the screen from the left/bottom
            //Clamp to 0-1 since if the LOD is outside the screen, the system shouldn't care
            float screenPercentPositionX = Mathf.Clamp01(screenPos.x / Screen.width);
            float screenPercentPositionY = Mathf.Clamp01(screenPos.y / Screen.height);

            float xDistance = screenPercentPositionX - screenCenter.x;
            float yDistance = screenPercentPositionY - screenCenter.y;
            float distanceSquared = xDistance * xDistance + yDistance * yDistance;
            
            //Is the object on the borders set by the threshold
            bool outsideThreshold = distanceSquared > (distanceFromCenter* distanceFromCenter);

            return outsideThreshold;
        }
    }
}