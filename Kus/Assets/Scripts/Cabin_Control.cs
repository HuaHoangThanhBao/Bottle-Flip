using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabin_Control : MonoBehaviour {

    public Vector3 left_Dir;
    public Vector3 right_Dir;

    public float offset;

    public float length;

    public float time_Ping;

    public bool player_Standing;

    void Start()
    {
        left_Dir = transform.position + Vector3.left / offset;

        right_Dir = transform.position + Vector3.right / offset;
    }

    void Update()
    {
        float time = Mathf.PingPong(Time.time * time_Ping, length);
        transform.position = Vector3.Lerp(left_Dir, right_Dir, time);
    }

    void OnCollisionEnter(Collision other)
    {
        other.collider.transform.SetParent(transform);
    }

    void OnCollisionExit(Collision other)
    {
        other.collider.transform.SetParent(null);
    }
}
