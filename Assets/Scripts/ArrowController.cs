using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ArrowController : MonoBehaviour {
    [Header("旋转设置")]
    public float rotationSpeed = 0.5f;  // 旋转灵敏度
    public float maxVerticalAngle = 80f; // 最大俯仰角度

    [Header("可视化设置")]
    public float lineLength = 3f;      // 方向线长度
    public Color lineColor = Color.red; // 方向线颜色
    public float lineWidth = 0.1f;     // 方向线粗细

    private LineRenderer directionLine;
    private bool isDragging = false;
    private Vector2 dragStartPosition;
    private Quaternion initialRotation;

    void Start() {
        InitializeLineRenderer();
    }

    void Update() {
        HandleMouseInput();
        UpdateDirectionLine();
    }

    void InitializeLineRenderer() {
        directionLine = GetComponent<LineRenderer>();
        directionLine.positionCount = 2;
        directionLine.material = new Material(Shader.Find("Sprites/Default"));
        directionLine.startColor = lineColor;
        directionLine.endColor = lineColor;
        directionLine.startWidth = lineWidth;
        directionLine.endWidth = lineWidth;
        //directionLine.enabled = false;
    }

    void HandleMouseInput() {
        if (Input.GetMouseButtonDown(1)) {
            StartDragging();
        } else if (Input.GetMouseButtonUp(1)) {
            StopDragging();
        }

        if (isDragging) {
            UpdateRotation();
        }
    }

    void StartDragging() {
        isDragging = true;
        dragStartPosition = Input.mousePosition;
        initialRotation = transform.rotation;
        directionLine.enabled = true;
    }

    void StopDragging() {
        isDragging = false;
        directionLine.enabled = false;
    }

    void UpdateRotation() {
        Vector2 currentMousePos = Input.mousePosition;
        Vector2 delta = (currentMousePos - dragStartPosition) * rotationSpeed;

        // 计算新旋转角度
        float newXRotation = Mathf.Clamp(-delta.y + initialRotation.eulerAngles.x, -maxVerticalAngle, maxVerticalAngle);
        float newYRotation = delta.x + initialRotation.eulerAngles.y;

        // 应用新旋转
        transform.rotation = Quaternion.Euler(newXRotation, newYRotation, 0);
    }

    void UpdateDirectionLine() {
        if (directionLine.enabled) {
            Vector3 lineEnd = transform.position + transform.forward * lineLength;
            directionLine.SetPosition(0, transform.position);
            directionLine.SetPosition(1, lineEnd);
        }
    }
}
