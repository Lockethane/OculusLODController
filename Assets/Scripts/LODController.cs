using UnityEngine;
using System.Collections.Generic;

namespace VRLODModifier
{
    public class LODController : MonoBehaviour {

        public List<LODGroupInfo> lodObjects;

	    void Start () {
        }
	
	    // Update is called once per frame
	    void Update () {
            int lodObjectCount = lodObjects.Count;
	        for(int i=0;i< lodObjectCount;++i)
            {
                lodObjects[i].LODUpdate();
            }
        }
    }
}