using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    public List<GameObject> chickList;

    public void RemoveBird()
    {
        if(chickList.Count > 0)
        {
            GameObject deleteBird = chickList[0];

            Destroy(deleteBird);

            chickList.RemoveAt(0);
        }

        else
        {
            Debug.Log("Empty Nest!");
        }
    }

}
