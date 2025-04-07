using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ArrowController : MonoBehaviour {
    [Header("��ת����")]
    public float rotationSpeed = 0.5f;  // ��ת������
    public float maxVerticalAngle = 80f; // ������Ƕ�

    [Header("���ӻ�����")]
    public float lineLength = 3f;      // �����߳���
    public Color lineColor = Color.red; // ��������ɫ
    public float lineWidth = 0.1f;     // �����ߴ�ϸ

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

        // ��������ת�Ƕ�
        float newXRotation = Mathf.Clamp(-delta.y + initialRotation.eulerAngles.x, -maxVerticalAngle, maxVerticalAngle);
        float newYRotation = delta.x + initialRotation.eulerAngles.y;

        // Ӧ������ת
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
