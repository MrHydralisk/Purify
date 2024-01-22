using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothTime = 0.5f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 wantLook;
    public Vector2 border = new Vector2(1.6f, 1f);

    public Transform lookAt;

    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, wantLook, ref velocity, smoothTime);
    }

    private void LateUpdate()
    {
        wantLook = transform.position;
        Vector3 delta = new Vector3(lookAt.position.x - transform.position.x, lookAt.position.y - transform.position.y, 0);
        if (Mathf.Abs(delta.x) > border.x)
        {
            if (delta.x > 0)
            {
                wantLook.x += delta.x - border.x;
            }
            else
            {
                wantLook.x += delta.x + border.x;
            }
        }
        if (Mathf.Abs(delta.y) > border.y)
        {
            if (delta.y > 0)
            {
                wantLook.y += delta.y - border.y;
            }
            else
            {
                wantLook.y += delta.y + border.y;
            }
        }
    }
}
