using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public Material newMaterial;
    public GameObject TreeObject;

    // Start is called before the first frame update
    void Start()
    {
    }


    public void changeMaterial()
    {
        Debug.Log("change material");
        MeshRenderer renderer = TreeObject.GetComponent<MeshRenderer>();

        Material[] materials = renderer.materials;

        if (materials.Length > 1 && newMaterial != null)
        {
            materials[1] = newMaterial;
            renderer.materials = materials;
        }

    }
}
