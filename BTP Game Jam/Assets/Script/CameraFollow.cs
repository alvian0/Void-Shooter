using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    public Vector3 offset;
    public float Speed;

    void FixedUpdate()
    {
        if (Player != null)
        {
            Vector3 CamPos = Player.position + offset;
            Vector3 Smoothed = Vector3.Lerp(transform.position, CamPos, Speed * Time.deltaTime);

            transform.position = Smoothed;
        }
    }
}
