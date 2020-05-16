using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body_Handler : MonoBehaviour {

    PlayerControl playerControl;

    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
    }

    public void Disable_Jump()
    {
        playerControl.p_variables.jump = false;

        playerControl.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void Disable_Jump_Twice()
    {
        playerControl.p_variables.jump_Twice = false;

        playerControl.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
