using UnityEngine;
using System.Collections;
using System.Reflection;

public class LODGroupInfo : MonoBehaviour {

	//Camera viewing this object
	public Camera cam;
	//Cached Transform of LOD parent(This object)
    Transform cached_transform;
	//Cached LODGroup component(This object)
	LODGroup lodGroup;
	//Cached LODs
    LOD[] lods;
	//Original LOD values
	float[] cachedTransitionsLods;
	//Modified LOD ranges
	public float[] customLODs;
	//Whether or not it is in regular LOD mode
	bool using_original_lod = true;
    
	//Maximum threshold in percentage of the screen from the border of the screen
	//Order is clockwise. X:Left, Y:Top, Z:Right, W:Bottom
	//0-1 value from border
	public Vector4 screen_percentage_border = new Vector4(0.0f,0.0f,0.0f,0.0f);

	//Min meters from camera 
	public float min_range_buffer = 1.0f;

    void Start () {

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

        cached_transform = transform;
    }
	
	void Update () 
	{
        //Get the screen space of the object
        Vector3 screenPos = cam.WorldToScreenPoint(cached_transform.position);

		//Get the 0-1 percent of where the object is on the screen from the left/bottom
		float screen_percent_positionX = screenPos.x / Screen.width;
		float screen_percent_positionY = screenPos.y / Screen.height;

		//Is the object on the borders set within the threshold
		bool within_threshold = (screen_percent_positionX < screen_percentage_border.x || screen_percent_positionX > (1.0f - screen_percentage_border.z)) ||
								(screen_percent_positionY < screen_percentage_border.y || screen_percent_positionY > (1.0f - screen_percentage_border.w));

		//Is the object far enough away from the camera
		bool outside_range_border = screenPos.z > min_range_buffer;

		//Test whether or not to use custom transitions
		if (outside_range_border && within_threshold)
			SetLods (customLODs, false);
		else
			SetLods (cachedTransitionsLods, true);
    }

	private void SetLods(float[] lod_transitions, bool original_lod)
	{
		//Camera conditions haven't changed between thresholds
		if (using_original_lod == original_lod)
			return;

		//Change the screen perecentage that the object has to be for the lod to take place
		for(int index=0; index < lod_transitions.Length; ++index)
		{
			lods[index].screenRelativeTransitionHeight = lod_transitions[index];
		}
		//Update the objects lod manager
		lodGroup.SetLODs(lods);

		//Toggle wether or not the object is using original LOD's to shortcut calling SetLODs
		using_original_lod = original_lod;
	}
}
