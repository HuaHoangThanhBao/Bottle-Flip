using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private PlayerControl player;

    private Vector3 offset = new Vector3(1, 1.5f, -3.42f);

    private float speed = 5;

    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, 0, 0) + offset, speed);
    }
}
