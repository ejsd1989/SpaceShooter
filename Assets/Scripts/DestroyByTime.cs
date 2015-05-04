using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour
{
    public float lifetime;
    public int lifeSec;
    private float doneTime;
    void Start()
    {
        doneTime = Time.time + lifeSec;
        Destroy(gameObject, lifetime);
    }

    //void FixedUpdate()
    //{
    //    renderer.enabled = true;
        
    //    while (Time.time > doneTime)
    //    {
    //        if (Time.time % 0.5 == 0)
    //            renderer.enabled = !renderer.enabled;
    //    }
    //}
}
