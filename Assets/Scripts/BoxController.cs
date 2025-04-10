using TMPro;
using UnityEngine;

public class BoxController : MonoBehaviour {
    [Header("�������")]
    public float startHeight = 1f;       // ��ʼ�߶�
    public float startWidth = 1f;
    public float rotationSpeed = 80f;    // ������ת�ٶ�

    public TMP_InputField heightInput;
    public TMP_InputField widthInput;
    public TMP_InputField fInput;

    private Rigidbody rb;
    private bool hasLaunched = false;

    private void Awake() {
        startHeight = float.Parse(heightInput.text);
        startWidth = float.Parse(widthInput.text);
    }
    public bool getLaunch() {
        return hasLaunched;
    }

    void Start() {

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        ResetPosition();
    }

    void Update() {
        startHeight = float.Parse(heightInput.text);
        startWidth = float.Parse(widthInput.text);
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
    }

    void ResetProjectile() {
        hasLaunched = false;
        rb.useGravity = false;
        ResetPosition();
    }

    void ResetPosition() {
        transform.position = new Vector3(0, startHeight + 5, -startWidth);
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
