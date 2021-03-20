using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BattleCameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _virtualCameraRotate;
    [SerializeField] CinemachineVirtualCamera _virtualPlayerAttack;
    [SerializeField] CinemachineVirtualCamera _virtualEnemyAttack;
    [SerializeField] CinemachineVirtualCamera _virtualCamera3;
    [SerializeField] float _position = 0;

    CinemachineTrackedDolly _dolly;
    [SerializeField] float timer = 0;
    [SerializeField] int moveTime = 8;
    float _fov = 40;
    bool move = false;

    enum CameraMove
    {
        rotare, attackMove, defaultMove
    }
    CameraMove cameraMove = CameraMove.defaultMove;
    void Start()
    {
        _dolly = _virtualCameraRotate.GetCinemachineComponent<CinemachineTrackedDolly>();
    }


    void FixedUpdate()
    {
        MoveCheck();
        switch (cameraMove)
        {
            case CameraMove.rotare:
                CameraRotate();
                break;
            case CameraMove.attackMove:
                RotareCameraReset();
                break;
            case CameraMove.defaultMove:
                break;
            default:
                break;
        }
    }
    void CameraRotate()
    {
        _fov = _virtualCameraRotate.m_Lens.FieldOfView;
        if (move)
        {
            _position += 0.001f;
            _dolly.m_PathPosition = _position;
            if (_fov < 75)
            {
                _fov += 0.2f;
                _virtualCameraRotate.m_Lens.FieldOfView = _fov;
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= moveTime)
            {
                move = true;
                timer = 0;
            }
            if (_fov > 60)
            {
                _fov -= 0.2f;
                _virtualCameraRotate.m_Lens.FieldOfView = _fov;
            }
        }
        if (_position >= 1)
        {
            move = false;
            _position = 0;
        }
    }

    void RotareCameraReset()
    {
        _position = 0;
        _dolly.m_PathPosition = 0;
        timer = 0;
        _fov = 40;
        _virtualCameraRotate.m_Lens.FieldOfView = 40;
        move = false;
    }

    void MoveCheck()
    {
        int[] cameraPriority = new int[3] {
            _virtualCameraRotate.Priority,
            _virtualPlayerAttack.Priority,
            _virtualEnemyAttack.Priority
        };
        int max = 0;
        foreach (var num in cameraPriority)
        { 
            if (num > max) 
            { max = num; }
        }
        for (int i = 0; i < cameraPriority.Length; i++)
        {
            if (max == cameraPriority[i])
            {
                if (i >= 1)
                {
                    cameraMove = CameraMove.attackMove;
                }
                else
                {
                    cameraMove = CameraMove.rotare;
                }
            }
        }
    }

    public void CameraChange(int num)
    {
        if (num == 0)//rotateCameraに注目
        {
            _virtualCameraRotate.Priority = 10;
            _virtualPlayerAttack.Priority = 1;
            _virtualEnemyAttack.Priority = 1;
        }
        else if(num == 1)//PlayerAttackに注目
        {
            _virtualCameraRotate.Priority = 1;
            _virtualPlayerAttack.Priority = 10;
            _virtualEnemyAttack.Priority = 1;
        }
        else if (num == 2)//EnemyAttackに注目
        {
            _virtualCameraRotate.Priority = 1;
            _virtualPlayerAttack.Priority = 1;
            _virtualEnemyAttack.Priority = 10;
        }
    }
}
