using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Walk,
        Run,
        Jump,
    }

    private PlayerState _state;
    private PlayerState State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            switch (State)
            {
                case PlayerState.Idle:
                    _animator.CrossFade("WAIT00", 0.1f);
                    break;
                case PlayerState.Walk:
                    _animator.Play("WALK");
                    break;
                case PlayerState.Run:
                    _animator.Play("RUN");
                    break;
            }
        }
    }

    [SerializeField] private Transform _characterBody;
    [SerializeField] private Transform _cameraArm;
    [SerializeField] private bool _canRun = true;

    Animator _animator;
    public float _walkSpeed = 3f;
    public float _runSpeed = 8f;

    void OnKeyboard(Define.KeyEvent evt)
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;

        if (evt == Define.KeyEvent.Press)
        {
            Vector3 dirFoward = new Vector3(_cameraArm.forward.x, 0f, _cameraArm.forward.z).normalized;
            Vector3 dirRight = new Vector3(_cameraArm.right.x, 0f, _cameraArm.right.z).normalized;
            Vector3 moveDir = dirFoward * moveInput.y + dirRight * moveInput.x;
            float moveSpeed = _walkSpeed;

            if (_canRun)
            {
                moveSpeed = _runSpeed;
                State = PlayerState.Run;
            }
            else
            {
                moveSpeed = _walkSpeed;
                State = PlayerState.Walk;
            }
            
            _characterBody.forward = moveDir;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        else
        {
            State = PlayerState.Idle;
        }
    }

    void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = _cameraArm.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;

        if (x < 180)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }
        
        _cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    private void Start()
    {
        _animator = _characterBody.GetComponent<Animator>();
        Manager.Input.KeyAction += OnKeyboard;
    }

    private void Update()
    {
        LookAround();
    }
}
