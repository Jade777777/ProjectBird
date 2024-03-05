using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public static List<Follower> FollowerList;
    Transform followTarget;
    float seperationDistance = 4f;
    float seed;

    private bool hasBeenFed = false;
    // Start is called before the first frame update
    void Start()
    {
        if (FollowerList == null)
        {
            FollowerList = new List<Follower>();
        }
        FollowerList.Add(this);
        followTarget = FindObjectOfType<PlayerCollecting>().transform;
        Animator birdAnimator = GetComponentInChildren<Animator>();
        birdAnimator.Play("Fly");
        seed = Random.Range(0f, 10000f);
        hasBeenFed = true;
    }


    Vector3 currentVelocity;
    // Update is called once per frame
    void Update()
    {
        if (!hasBeenFed) { return; }

        Vector3 averagePos = new Vector3();
        foreach(Follower l in FollowerList)
        {
            averagePos += l.transform.position;
        }
        averagePos /= FollowerList.Count;
        Vector3 seperation =  transform.position - averagePos;
        seperation = seperation.normalized * seperationDistance- seperation;
        seperation += new Vector3(
            Mathf.PerlinNoise(7 + seed + Time.time * 0.1f, 59 + seed + Time.time * 0.1f),
            Mathf.PerlinNoise(23 + seed + Time.time * 0.1f, 47 + seed + Time.time * 0.1f), 
            Mathf.PerlinNoise(27 + seed + Time.time * 0.1f, 31 + seed + Time.time * 0.1f));
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, followTarget.position -  followTarget.forward + seperation, ref currentVelocity, 0.3f);
        transform.rotation= Quaternion.LookRotation(Vector3.ProjectOnPlane(currentVelocity, Vector3.up),Vector3.up);
        transform.position = targetPosition;
        
    }
}
