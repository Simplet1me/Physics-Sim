using TMPro;
using UnityEngine;

public class SphereController : MonoBehaviour {
    [Header("发射参数")]
    public float initialSpeed = 10f;      // 初始速度
    public float startHeight = 1f;       // 起始高度
    public float rotationSpeed = 80f;    // 方向旋转速度

    [Header("可视化")]
    public LineRenderer directionArrow;  // 方向箭头

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
            // 方向控制
            HandleRotationInput();

            // 空格键发射
            if (Input.GetKeyDown(KeyCode.Space)) {
                Launch();
            }
        }

        // R键重置
        if (Input.GetKeyDown(KeyCode.R)) {
            ResetProjectile();
        }
    }

    void HandleRotationInput() {
        // 使用键盘控制方向
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 创建旋转量
        Vector3 rotation = new Vector3(vertical, horizontal, 0) * rotationSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(rotation);

        // 应用旋转并更新方向
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
