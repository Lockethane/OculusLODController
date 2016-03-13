using UnityEngine;
using System.Collections;
using VRLODModifier;

public class LODCuller : MonoBehaviour {

	private LODController controller;
	private LODGroupInfo groupInfo;

	// Use this for initialization
	void Start () {
		controller = GameObject.FindObjectOfType<LODController>();
		if (controller == null)
		{
			Debug.LogError("No LODController found");
		}

		groupInfo = this.GetComponentInParent<LODGroupInfo>();
		if (groupInfo == null) 
		{
			Debug.LogError("No LODGroupInfo found");
		}

	}

	void OnBecameVisible()
	{
		if (groupInfo) 
		{
			controller.AddLodGroupInfo(groupInfo);
		}
	}
	
	void OnBecameInvisible() 
	{
		if (groupInfo) 
		{
			controller.RemoveLODGroupInfo(groupInfo);
		}
	}
}
