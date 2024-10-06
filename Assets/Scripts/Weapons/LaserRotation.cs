using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRotation : MonoBehaviour
{
    Camera cam;

    Vector2 MousePos 
    {
        get 
        {
            Vector2 Pos = cam.ScreenToWorldPoint(GameInput.Instance.GetMousePosition());
            return Pos;
        }
    }

    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector2 dir = (Vector2)transform.position - MousePos;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        transform.eulerAngles = new Vector3(0f, 0f, angle + 180f);
    }
}
