using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcAnimFlap : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    public float WingPos = 0;


    [SerializeField]
    GameObject pivot1;
    [SerializeField]
    GameObject pivot2;
    [SerializeField]
    float maxWingAngle =60;
    [SerializeField]
    float minWingAngle=-60;
    // Update is called once per frame
    void Update()
    {
        WingPos = GetComponentInParent<RhythmTracker>().CurrentPosition;
        Quaternion wingRot = Quaternion.Euler(0, 0, Mathf.Lerp(maxWingAngle, minWingAngle, WingPos));
        pivot1.transform.localRotation = wingRot;
        pivot2.transform.localRotation = Quaternion.Inverse(wingRot);
    }
}
