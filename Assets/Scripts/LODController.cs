using UnityEngine;
using System.Collections.Generic;

namespace VRLODController
{
    public class LODController : MonoBehaviour {

        private Camera cam;

        private List<LODGroupInfo> lodObjects;

	    void Start () {
			//Since the startup order cannot be guaranteed need to potentially instantiate list
			if (lodObjects == null) 
			{
				lodObjects = new List<LODGroupInfo>();
			}
            cam = this.GetComponent<Camera>();
        }
	
	    // Update is called once per frame
	    void Update () {
            int lodObjectCount = lodObjects.Count;
	        for(int i=0;i< lodObjectCount;++i)
            {
                lodObjects[i].LODUpdate(cam);
            }
        }

		public void AddLodGroupInfo(LODGroupInfo info)
		{
			//Since the startup order cannot be guaranteed need to potentially instantiate list
			if (lodObjects == null) 
			{
				lodObjects = new List<LODGroupInfo>();
			}

            lodObjects.Add(info);
		}

		public void RemoveLODGroupInfo(LODGroupInfo info)
		{
            Transform transform = info.transform;
            lodObjects.Remove(info);
		}
    }
}