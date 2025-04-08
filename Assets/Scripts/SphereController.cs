using TMPro;
using UnityEngine;

public class SphereController : MonoBehaviour {
    [Header("�������")]
    public float initialSpeed = 10f;      // ��ʼ�ٶ�
    public float startHeight = 1f;       // ��ʼ�߶�
    public float rotationSpeed = 80f;    // ������ת�ٶ�

    [Header("���ӻ�")]
    public LineRenderer directionArrow;  // �����ͷ

    public TMP_InputField speedInput;
    public TMP_InputField heightInput;
    public TMP_InputField fInput;

    private Rigidbody rb;
    private Vector3 launchDirection = Vector3.forward;
    private bool hasLaunched = false;

    public bool getLaunch() {
        return hasLaunched;
    }
    
    void Awake() {
        initialSpeed = float.Parse(speedInput.text);
        startHeight = float.Parse(heightInput.text);
    }
    

    void Start() {

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        ResetPosition();
    }

    void Update() {
        launchDirection = directionArrow.transform.forward;
        initialSpeed = float.Parse(speedInput.text);
        startHeight = float.Parse(heightInput.text);
        rb.drag = float.Parse(fInput.text);
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
    }

    void Launch() {
        hasLaunched = true;
        rb.useGravity = true;
        rb.velocity = launchDirection.normalized * initialSpeed;
        directionArrow.gameObject.SetActive(false);
    }

    void ResetProjectile() {
        hasLaunched = false;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        directionArrow.gameObject.SetActive(true);
        ResetPosition();
    }

    void ResetPosition() {
        transform.position = new Vector3(0, startHeight + 20, 50);
        launchDirection = Vector3.forward;
    }
}
