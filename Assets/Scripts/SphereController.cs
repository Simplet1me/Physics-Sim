using UnityEngine;

public class SphereController : MonoBehaviour {
    [Header("发射参数")]
    public float initialSpeed = 10f;      // 初始速度
    public float startHeight = 1f;       // 起始高度
    public float rotationSpeed = 80f;    // 方向旋转速度

    [Header("可视化")]
    public LineRenderer directionArrow;  // 方向箭头
    public float arrowLength = 2f;       // 箭头长度


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
        UpdateArrow();
    }

    void UpdateArrow() {
        if (directionArrow != null) {
            // 设置箭头位置和方向
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
