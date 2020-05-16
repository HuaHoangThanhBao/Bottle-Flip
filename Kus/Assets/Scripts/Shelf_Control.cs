using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf_Control : MonoBehaviour {

    PlayerControl playerControl;

    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
    }

    void OnCollisionEnter()
    {
        if (playerControl.p_variables.game_Begin)
            StartCoroutine(Rotate_Shelf());
    }

    IEnumerator Rotate_Shelf(float duration = 1)
    {
        Quaternion startRot = transform.rotation;

        Quaternion dirRot = Quaternion.Euler(new Vector3(0, 0, -18)) * startRot;

        for (float t = 0; t < duration; t+= Time.deltaTime)
        {
            transform.rotation = Quaternion.Lerp(startRot, dirRot, t / duration);

            yield return null;
        }

        transform.rotation = dirRot;
    }
}
