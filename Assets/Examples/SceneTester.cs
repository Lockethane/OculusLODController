using UnityEngine;
using System.Collections;

public class SceneTester : MonoBehaviour {

	public GameObject prefab;

	public Transform cameraController;
	public Camera camera;

	// Use this for initialization
	void Start () {
		Debug.Log ("Screen Resolution:" + Screen.currentResolution);

		UnityEngine.VR.InputTracking.Recenter ();

		//Set up 10x10 grid
		for (int row=0; row<10; ++row) {
			for(int col=0;col<10; ++col)
			{
				Vector3 pos = transform.position;
				pos.x += (-5+row);
				pos.z += (col);
				GameObject obj = (GameObject)Instantiate(prefab,pos,Quaternion.identity);
				LODGroupInfo groupInfo = obj.GetComponent<LODGroupInfo>();
				groupInfo.cam = camera;

				//Set the screen percentage that each side should be affected
				groupInfo.screen_percentage_border = new Vector4(0.15f,0.15f,0.15f,0.15f);
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
