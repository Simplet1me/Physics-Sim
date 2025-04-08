using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFreeController : MonoBehaviour
{
    [Header("�ƶ�����")]
    [SerializeField] private float moveSpeed = 10f;        // �����ƶ��ٶ�
    [SerializeField] private float shiftMultiplier = 2f;   // ����ϵ��
    [SerializeField] private float verticalSpeed = 5f;     // ��ֱ�ƶ��ٶ�

    [Header("�ӽ�����")]
    [SerializeField] private float lookSpeed = 2f;         // �ӽ�ת���ٶ�
    [SerializeField] private float maxVerticalAngle = 90f; // ������Ƕ�
    [SerializeField] private float minVerticalAngle = -90f;// ��С�����Ƕ�

    private Vector2 _rotation = Vector2.zero;
    private bool _isRotating = false;

    void Update() {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement() {
        // ��ȡ�����ƶ�����
        Vector3 moveInput = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );

        // ת��Ϊ����ռ��ƶ�����
        Vector3 moveDirection = transform.TransformDirection(moveInput);

        // ����ֱ�ƶ� (Q/E)
        float vertical = 0;
        if (Input.GetKey(KeyCode.Q))
            vertical = -1;
        if (Input.GetKey(KeyCode.E))
            vertical = 1;

        // ���������ƶ��ٶ�
        float currentSpeed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? shiftMultiplier : 1f);

        // Ӧ���ƶ�
        transform.position +=
            moveDirection.normalized * currentSpeed * Time.deltaTime +
            Vector3.up * vertical * verticalSpeed * Time.deltaTime;
    }

    void HandleRotation() {
        // ����Ҽ�����
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
        // ��ȡ�������
        _rotation.x += Input.GetAxis("Mouse X") * lookSpeed;
        _rotation.y -= Input.GetAxis("Mouse Y") * lookSpeed;

        // ���ƴ�ֱ�Ƕ�
        _rotation.y = Mathf.Clamp(_rotation.y, minVerticalAngle, maxVerticalAngle);

        // Ӧ����ת
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
