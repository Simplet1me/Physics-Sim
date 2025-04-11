using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SphereCollController : MonoBehaviour
{
    [Header("�������")]
    public float initialSpeed = 10f;      // ��ʼ�ٶ�
    public float startPosition = 1f;       // ��ʼ�߶�

    public TMP_InputField speedInput;
    public TMP_InputField positionInput;
    public TMP_InputField fInput;

    private Rigidbody rb;
    private bool hasLaunched = false;

    public bool getLaunch() {
        return hasLaunched;
    }

    void Awake() {
        initialSpeed = float.Parse(speedInput.text);
        startPosition = float.Parse(positionInput.text);
    }


    void Start() {

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        ResetPosition();
    }

    void Update() {
        initialSpeed = float.Parse(speedInput.text);
        startPosition = float.Parse(positionInput.text);
        rb.drag = float.Parse(fInput.text);
        if (!hasLaunched) {
            // �ո������
            if (Input.GetKeyDown(KeyCode.Space)) {
                Launch();
            }
        }

        // R������
        if (Input.GetKeyDown(KeyCode.R)) {
            ResetProjectile();
        }
    }

    void Launch() {
        hasLaunched = true;
        rb.useGravity = true;
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, initialSpeed);
    }

    void ResetProjectile() {
        hasLaunched = false;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        ResetPosition();
    }

    void ResetPosition() {
        transform.position = new Vector3(0,2,startPosition);
    }
}
