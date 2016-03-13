using UnityEngine;
using VRLODController;

public class SceneTester : MonoBehaviour {

	public GameObject prefab;

	public Transform cameraController;
	public Camera VRCamera;

    public Vector4 screenPercentageBorder;

	// Use this for initialization
	void Start () {
		Debug.Log ("Screen Resolution:" + Screen.currentResolution);

		UnityEngine.VR.InputTracking.Recenter ();
        QualitySettings.lodBias = 1;

        int gridSize = 30;

        //Set up gridSize x gridSize grid
        for (int row=0; row<gridSize; ++row) {
			for(int col=0;col< gridSize; ++col)
			{
				Vector3 pos = transform.position;
				pos.x += ((-(float)(gridSize/2.0f))+row);
				pos.z += (col);
				GameObject obj = (GameObject)Instantiate(prefab,pos,Quaternion.identity);
				LODGroupInfo groupInfo = obj.GetComponent<LODGroupInfo>();
				groupInfo.cam = VRCamera;

                //Set the screen percentage that each side should be affected
                groupInfo.screenPercentageBorder = screenPercentageBorder;
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
