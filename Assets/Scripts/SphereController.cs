using UnityEngine;

public class SphereController : MonoBehaviour {
    [Header("�������")]
    public float initialSpeed = 10f;      // ��ʼ�ٶ�
    public float startHeight = 1f;       // ��ʼ�߶�
    public float rotationSpeed = 80f;    // ������ת�ٶ�

    [Header("���ӻ�")]
    public LineRenderer directionArrow;  // �����ͷ
    public float arrowLength = 2f;       // ��ͷ����


    private Rigidbody rb;
    private Vector3 launchDirection = Vector3.forward;
    private bool hasLaunched = false;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        ResetPosition();
        UpdateArrow();
    }

    void Update() {
        if (!hasLaunched) {
            // �������
            HandleRotationInput();

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

    void HandleRotationInput() {
        // ʹ�ü��̿��Ʒ���
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ������ת��
        Vector3 rotation = new Vector3(vertical, horizontal, 0) * rotationSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(rotation);

        // Ӧ����ת�����·���
        launchDirection = deltaRotation * launchDirection;
        UpdateArrow();
    }

    void UpdateArrow() {
        if (directionArrow != null) {
            // ���ü�ͷλ�úͷ���
            directionArrow.SetPosition(0, transform.position);
            directionArrow.SetPosition(1, transform.position + launchDirection * arrowLength);
        }
    }

    void Launch() {
        hasLaunched = true;
        rb.useGravity = true;
        rb.velocity = launchDirection.normalized * initialSpeed;

        if (directionArrow != null)
            directionArrow.enabled = false;
    }

    void ResetProjectile() {
        hasLaunched = false;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        ResetPosition();

        if (directionArrow != null)
            directionArrow.enabled = true;
    }

    void ResetPosition() {
        transform.position = new Vector3(0, startHeight + 20, 50);
        launchDirection = Vector3.forward;
    }
}
