using UnityEngine;
using VRLODModifier;

public class SceneTester : MonoBehaviour {

	public GameObject prefab;

	public Transform cameraController;
	public Camera VRCamera;

	// Use this for initialization
	void Start () {
		Debug.Log ("Screen Resolution:" + Screen.currentResolution);

		UnityEngine.VR.InputTracking.Recenter ();

        int grid_size = 30;

		//Set up 20x20 grid
		for (int row=0; row<grid_size; ++row) {
			for(int col=0;col< grid_size; ++col)
			{
				Vector3 pos = transform.position;
				pos.x += ((-(float)(grid_size/2.0f))+row);
				pos.z += (col);
				GameObject obj = (GameObject)Instantiate(prefab,pos,Quaternion.identity);
				LODGroupInfo groupInfo = obj.GetComponent<LODGroupInfo>();
				groupInfo.cam = VRCamera;

				//Set the screen percentage that each side should be affected
				groupInfo.screenPercentageBorder = new Vector4(0.15f,0.15f,0.15f,0.15f);

                this.GetComponent<LODController>().AddLodGroupInfo(groupInfo);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Test controls for testing/observing changes in parameters
		if (Input.GetKey (KeyCode.UpArrow))
			cameraController.transform.position += new Vector3(0,0,0.1f*Time.deltaTime);

		if (Input.GetKey (KeyCode.DownArrow))
			cameraController.transform.position += new Vector3(0,0,-0.1f*Time.deltaTime);

		if (Input.GetKey (KeyCode.LeftArrow))
			cameraController.transform.position += new Vector3(-0.1f*Time.deltaTime,0,0);
		
		if (Input.GetKey (KeyCode.RightArrow))
			cameraController.transform.position += new Vector3(0.1f*Time.deltaTime,0,0);
	}
}
