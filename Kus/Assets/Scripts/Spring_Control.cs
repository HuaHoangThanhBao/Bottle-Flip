using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring_Control : MonoBehaviour {

    Audio_Handler audio_Handler;

    PlayerControl playerControl;

    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();

        audio_Handler = FindObjectOfType<Audio_Handler>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (!playerControl.p_variables.end_Game)
        {
            audio_Handler.Play("Flip");

            FindObjectOfType<PlayerControl>().p_variables.can_Press = true;

            other.transform.GetComponent<PlayerControl>().Jump();
        }
    }
}
