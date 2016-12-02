using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Lesson1CameraController : MonoBehaviour
{
    public Transform target;

    private Camera m_camera;

    void Awake()
    {
        m_camera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        var right = m_camera.ViewportToWorldPoint(Vector2.right);
        var center = m_camera.ViewportToWorldPoint(Vector2.one * 0.5f);

        if (center.x < target.position.x)
        {
            var pos = m_camera.transform.position;

            if (Math.Abs(pos.x - target.position.x) > 0f)
            {
                m_camera.transform.position = new Vector3(target.position.x, pos.y, pos.z);
            }
        }
    }
}
