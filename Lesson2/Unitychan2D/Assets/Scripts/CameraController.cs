using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    private Transform target;

    private Camera m_camera;

    void Awake()
    {
        target = FindObjectOfType<PlayerController>().transform;
        m_camera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        var right = m_camera.ViewportToWorldPoint(new Vector2(1, 0));
        var center = m_camera.ViewportToWorldPoint(new Vector2(0.5f,0.5f));

        // 카메라의 x축 중심이 플레이어의 x 좌표보다 왼쪽에 있으면
        if (center.x < target.position.x)
        {
            Vector3 pos = m_camera.transform.position;
            // + - 부호를 날리고 양수
            m_camera.transform.position = new Vector3(target.position.x, pos.y, pos.z);
        }

    }
}
