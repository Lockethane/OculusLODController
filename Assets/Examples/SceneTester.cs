using UnityEngine;
using System.Collections;

public class SceneTester : MonoBehaviour {

	public Transform target;

	public GameObject prefab;
	public Camera cam;

	// Use this for initialization
	void Start () {
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
				groupInfo.cam = cam;
				groupInfo.screen_percentage_border = new Vector4(0.15f,0.15f,0.15f,0.15f);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (target == null)
			return;

		if (Input.GetKeyUp (KeyCode.UpArrow))
			target.position += new Vector3(0,0,0.1f);

		if (Input.GetKeyUp (KeyCode.DownArrow))
			target.position += new Vector3(0,0,-0.1f);

		cam.transform.Rotate(Vector3.up, 10*Time.deltaTime);
	}
}
