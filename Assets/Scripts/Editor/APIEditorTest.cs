using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using VRLODController;

public class NewEditorTest {

    //Test Order
    // Left LOD with border
    // Left LOD with no border
    // Center
    // Center Top with border
    // Center Bottom with border
    // Right LOD with border
    // Right LOD with no border

    [Test]
    public void LeftWithBorder()
    {
        GameObject lod = GameObject.Find("LOD");
        Camera camera = GameObject.FindObjectOfType<Camera>();
        Vector3 startLoc = camera.ScreenToWorldPoint(new Vector3(Screen.width*0.10f, Screen.height / 2, -8));
        GameObject testLOD = (GameObject)GameObject.Instantiate(lod, startLoc, Quaternion.identity);
        LODGroupInfo info = testLOD.GetComponent<LODGroupInfo>();
        
        info.screenPercentageBorder = new Vector4(0.15f, 0.85f, 0.85f, 0.15f);
        info.Start();
        info.LODUpdate(camera);
        
        Assert.AreEqual(info.UsingOriginalLod, false);
    }

    [Test]
    public void LeftWithNoBorder()
    {
        GameObject lod = GameObject.Find("LOD");
        Camera camera = GameObject.FindObjectOfType<Camera>();
        Vector3 startLoc = camera.ScreenToWorldPoint(new Vector3(Screen.width * 0.10f, Screen.height / 2, -8));
        GameObject testLOD = (GameObject)GameObject.Instantiate(lod, startLoc, Quaternion.identity);
        LODGroupInfo info = testLOD.GetComponent<LODGroupInfo>();
        

        info.Start();
        info.LODUpdate(camera);

        Assert.AreEqual(info.UsingOriginalLod, true);
    }

    [Test]
    public void CenterWithBorder()
    {
        GameObject lod = GameObject.Find("LOD");
        Camera camera = GameObject.FindObjectOfType<Camera>();
        Vector3 startLoc = camera.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height / 2, -8));
        GameObject testLOD = (GameObject)GameObject.Instantiate(lod, startLoc, Quaternion.identity);
        LODGroupInfo info = testLOD.GetComponent<LODGroupInfo>();
        

        info.screenPercentageBorder = new Vector4(0.15f, 0.85f, 0.85f, 0.15f);
        info.Start();
        info.LODUpdate(camera);

        Assert.AreEqual(info.UsingOriginalLod, true);
    }

    [Test]
    public void CenterTopWithBorder()
    {
        GameObject lod = GameObject.Find("LOD");
        Camera camera = GameObject.FindObjectOfType<Camera>();
        Vector3 startLoc = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height *0.1f, -8));
        GameObject testLOD = (GameObject)GameObject.Instantiate(lod, startLoc, Quaternion.identity);
        LODGroupInfo info = testLOD.GetComponent<LODGroupInfo>();
        

        info.screenPercentageBorder = new Vector4(0.15f, 0.85f, 0.85f, 0.15f);
        info.Start();
        info.LODUpdate(camera);

        Assert.AreEqual(info.UsingOriginalLod, false);
    }

    [Test]
    public void CenterBottomWithBorder()
    {
        GameObject lod = GameObject.Find("LOD");
        Camera camera = GameObject.FindObjectOfType<Camera>();
        Vector3 startLoc = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height * 0.9f, -8));
        GameObject testLOD = (GameObject)GameObject.Instantiate(lod, startLoc, Quaternion.identity);
        LODGroupInfo info = testLOD.GetComponent<LODGroupInfo>();
        
        info.screenPercentageBorder = new Vector4(0.15f, 0.15f, 0.85f, 0.15f);
        info.Start();
        info.LODUpdate(camera);

        Assert.AreEqual(info.UsingOriginalLod, false);
    }

    [Test]
    public void RightWithBorder()
    {
        GameObject lod = GameObject.Find("LOD");
        Camera camera = GameObject.FindObjectOfType<Camera>();
        Vector3 startLoc = camera.ScreenToWorldPoint(new Vector3(Screen.width *0.9f, Screen.height/2, -8));
        GameObject testLOD = (GameObject)GameObject.Instantiate(lod, startLoc, Quaternion.identity);
        LODGroupInfo info = testLOD.GetComponent<LODGroupInfo>();
        
        info.screenPercentageBorder = new Vector4(0.15f, 0.15f, 0.85f, 0.15f);
        info.Start();
        info.LODUpdate(camera);

        Assert.AreEqual(info.UsingOriginalLod, false);
    }

    [Test]
    public void RightWithNoBorder()
    {
        GameObject lod = GameObject.Find("LOD");
        Camera camera = GameObject.FindObjectOfType<Camera>();
        Vector3 startLoc = camera.ScreenToWorldPoint(new Vector3(Screen.width * 0.9f, Screen.height / 2, -8));
        GameObject testLOD = (GameObject)GameObject.Instantiate(lod, startLoc, Quaternion.identity);
        LODGroupInfo info = testLOD.GetComponent<LODGroupInfo>();
        
        info.Start();
        info.LODUpdate(camera);

        Assert.AreEqual(info.UsingOriginalLod, true);
    }
}
