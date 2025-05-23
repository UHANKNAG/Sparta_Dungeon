using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;
    public LayerMask wallLayerMask;


    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;
    public bool canLook = true;

    public Action inventory;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Cursor를 Lock 시키는 (보이지 않게)
       Cursor.lockState = CursorLockMode.Locked;
    }

   
    // rigidbody와 같은 물리 연산을 실행할 땐 FixedUpdate
    void FixedUpdate() {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    // 저장된 curMovementInput을 실제 이동에 적용하는 메소드
    void Move() 
    {
        if (IsWalled())
        {
            Vector3 wallPos = transform.forward;
            CharacterManager.Instance.Player.GetComponent<Rigidbody>().useGravity = false;
            CharacterManager.Instance.Player.GetComponent<Rigidbody>().AddForce(wallPos * 10f);

            if (curMovementInput == Vector2.zero) {
                _rigidbody.velocity = Vector3.zero;
                return;
            }

            Vector3 dir = transform.up * curMovementInput.y + transform.right * curMovementInput.x;
            dir *= moveSpeed;
            // dir.y = _rigidbody.velocity.y;

            _rigidbody.velocity = dir;

        }
        else
        {
            if (!CharacterManager.Instance.Player.condition.isZeroGravity)
            {
                CharacterManager.Instance.Player.GetComponent<Rigidbody>().useGravity = true;
            }

            if (curMovementInput == Vector2.zero) {
                _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, 0);
                return;
            }

            Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
            dir *= moveSpeed;
            dir.y = _rigidbody.velocity.y;

            _rigidbody.velocity = dir;
        }
    }

    void CameraLook() 
    {
        // 상하 각도 조절 / 마우스 각도에 따른 카메라 상하 회전
        // 최소, 최대 값을 정해 그 안에서만 움직일 수 있도록 (카메라만 움직임)
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // 좌우 움직임 / 마우스값에 따라 transform(player)에게 회전을 적용한다
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    // 현재 상태를 받아오는 context
    // 입력 값을 받아 curMovementInput에 저장하는 메소드
    public void OnMove(InputAction.CallbackContext context) 
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context) 
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context) 
    {
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("Jump Pressed!");

            if (IsGrounded())
            {
                _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
        }
    }

    private bool IsGrounded()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        float rayLength = 1f;

        Debug.DrawRay(origin, Vector3.down * rayLength, Color.red);
        
        bool grounded = Physics.Raycast(origin, Vector3.down, rayLength, groundLayerMask);

        if (grounded)
        {
            Debug.Log("바닥 감지됨!");
        }
        else
        {
            Debug.Log("바닥 감지 실패");
        }

        return grounded;
    }


    private bool IsWalled()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        float rayLength = 0.5f;

        Debug.DrawRay(origin, transform.forward * rayLength, Color.red);
        
        bool walled = Physics.Raycast(origin, transform.forward, rayLength, wallLayerMask);

        // if (walled)
        // {
        //     Debug.Log("벽 감지됨!");
        // }
        // else
        // {
        //     Debug.Log("벽 감지 실패");
        // }

        return walled;
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            if (!CharacterManager.Instance.Player.condition.canDash)
            {
                return;
            }

            CharacterManager.Instance.Player.condition.isDash = true;
            moveSpeed *= 2f;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            CharacterManager.Instance.Player.condition.isDash = false;
            moveSpeed /= 2f;
        }
    }
}

