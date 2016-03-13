using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using VRLODModifier;

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
        GameObject lod = GameObject.Find("Lod1");
        GameObject testLOD = GameObject.Instantiate(lod);
        LODGroupInfo info = testLOD.GetComponent<LODGroupInfo>();
        info.screenPercentageBorder = new Vector4(0.15f, 0.15f, 0.15f, 0.15f);
        info.Start();
        info.LODUpdate();
        
        Assert.AreEqual(info.usingOriginalLod, false);
    }

    [Test]
    public void LeftWithNoBorder()
    {
        GameObject lod = GameObject.Find("Lod1");
        GameObject testLOD = GameObject.Instantiate(lod);
        LODGroupInfo info = testLOD.GetComponent<LODGroupInfo>();
        info.Start();
        info.LODUpdate();

        Assert.AreEqual(info.usingOriginalLod, true);
    }

    [Test]
    public void CenterWithBorder()
    {
        GameObject lod = GameObject.Find("CenterLOD");
        GameObject testLOD = GameObject.Instantiate(lod);
        LODGroupInfo info = testLOD.GetComponent<LODGroupInfo>();
        info.Start();
        info.LODUpdate();

        Assert.AreEqual(info.usingOriginalLod, true);
    }

    [Test]
    public void CenterTopWithBorder()
    {
        GameObject lod = GameObject.Find("CenterTopLOD");
        GameObject testLOD = GameObject.Instantiate(lod);
        LODGroupInfo info = testLOD.GetComponent<LODGroupInfo>();
        info.Start();
        info.LODUpdate();

        Assert.AreEqual(info.usingOriginalLod, false);
    }

    [Test]
    public void CenterBottomWithBorder()
    {
        GameObject lod = GameObject.Find("CenterBottomLOD");
        GameObject testLOD = GameObject.Instantiate(lod);
        LODGroupInfo info = testLOD.GetComponent<LODGroupInfo>();
        info.Start();
        info.LODUpdate();

        Assert.AreEqual(info.usingOriginalLod, false);
    }

    [Test]
    public void RightWithBorder()
    {
        GameObject lod = GameObject.Find("Lod3");
        GameObject testLOD = GameObject.Instantiate(lod);
        LODGroupInfo info = testLOD.GetComponent<LODGroupInfo>();
        info.screenPercentageBorder = new Vector4(0.15f, 0.15f, 0.15f, 0.15f);
        info.Start();
        info.LODUpdate();

        Assert.AreEqual(info.usingOriginalLod, false);
    }

    [Test]
    public void RightWithNoBorder()
    {
        GameObject lod = GameObject.Find("Lod3");
        GameObject testLOD = GameObject.Instantiate(lod);
        LODGroupInfo info = testLOD.GetComponent<LODGroupInfo>();
        info.Start();
        info.LODUpdate();

        Assert.AreEqual(info.usingOriginalLod, true);
    }
}
