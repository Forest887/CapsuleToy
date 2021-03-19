using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BattleCameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _virtualCamera;
    [SerializeField] float _position = 0;

    CinemachineTrackedDolly _dolly;
    [SerializeField] float timer = 0;
    [SerializeField] int moveTime = 8;
    bool move = false;
    void Start()
    {
        _dolly = _virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
    }


    void FixedUpdate()
    {
        if (move)
        {
            _position += 0.001f;
            _dolly.m_PathPosition = _position;
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= moveTime)
            {
                move = true;
                timer = 0;
            }
        }
        if (_position >= 1)
        {
            move = false;
            _position = 0;
        }
    }
}
