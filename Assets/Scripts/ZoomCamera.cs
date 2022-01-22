using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class ZoomCamera : MonoBehaviour
{
    private CinemachineVirtualCamera m_Camera;
    private float zoomMin = 10f;
    private float zoomMax = 50f;
    private float zoomSpeed = 5f;

    void Awake()
    {
        m_Camera = GetComponent<CinemachineVirtualCamera>();
    }

    public void Zoom (float FOW)
    {
        
        if (FOW > 0) m_Camera.m_Lens.FieldOfView -= zoomSpeed;
        if(FOW < 0) m_Camera.m_Lens.FieldOfView+= zoomSpeed;
        m_Camera.m_Lens.FieldOfView = Mathf.Clamp(m_Camera.m_Lens.FieldOfView, zoomMin, zoomMax);
    }
}
