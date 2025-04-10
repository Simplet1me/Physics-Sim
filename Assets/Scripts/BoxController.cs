using TMPro;
using UnityEngine;

public class BoxController : MonoBehaviour {
    [Header("发射参数")]
    public float startHeight = 1f;       // 起始高度
    public float startWidth = 1f;
    public float rotationSpeed = 80f;    // 方向旋转速度

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
