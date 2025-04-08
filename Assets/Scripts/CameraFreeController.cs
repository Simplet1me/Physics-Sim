using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFreeController : MonoBehaviour
{
    [Header("移动设置")]
    [SerializeField] private float moveSpeed = 10f;        // 基础移动速度
    [SerializeField] private float shiftMultiplier = 2f;   // 加速系数
    [SerializeField] private float verticalSpeed = 5f;     // 垂直移动速度

    [Header("视角设置")]
    [SerializeField] private float lookSpeed = 2f;         // 视角转动速度
    [SerializeField] private float maxVerticalAngle = 90f; // 最大俯仰角度
    [SerializeField] private float minVerticalAngle = -90f;// 最小俯仰角度

    private Vector2 _rotation = Vector2.zero;
    private bool _isRotating = false;

    void Update() {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement() {
        // 获取基础移动输入
        Vector3 moveInput = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );

        // 转换为世界空间移动方向
        Vector3 moveDirection = transform.TransformDirection(moveInput);

        // 处理垂直移动 (Q/E)
        float vertical = 0;
        if (Input.GetKey(KeyCode.Q))
            vertical = -1;
        if (Input.GetKey(KeyCode.E))
            vertical = 1;

        // 计算最终移动速度
        float currentSpeed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? shiftMultiplier : 1f);

        // 应用移动
        transform.position +=
            moveDirection.normalized * currentSpeed * Time.deltaTime +
            Vector3.up * vertical * verticalSpeed * Time.deltaTime;
    }

    void HandleRotation() {
        // 鼠标右键控制
        if (Input.GetMouseButtonDown(1)) {
            StartRotation();
        } else if (Input.GetMouseButtonUp(1)) {
            StopRotation();
        }

        if (_isRotating) {
            UpdateRotation();
        }
    }

    void StartRotation() {
        _isRotating = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _rotation = new Vector2(
            transform.eulerAngles.y,
            transform.eulerAngles.x
        );
    }

    void StopRotation() {
        _isRotating = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void UpdateRotation() {
        // 获取鼠标输入
        _rotation.x += Input.GetAxis("Mouse X") * lookSpeed;
        _rotation.y -= Input.GetAxis("Mouse Y") * lookSpeed;

        // 限制垂直角度
        _rotation.y = Mathf.Clamp(_rotation.y, minVerticalAngle, maxVerticalAngle);

        // 应用旋转
        transform.rotation = Quaternion.Euler(
            _rotation.y,
            _rotation.x,
            0
        );
    }

    void OnValidate() {
        maxVerticalAngle = Mathf.Clamp(maxVerticalAngle, -90f, 90f);
        minVerticalAngle = Mathf.Clamp(minVerticalAngle, -90f, maxVerticalAngle);
    }
}
