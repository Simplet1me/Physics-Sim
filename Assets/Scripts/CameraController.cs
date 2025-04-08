using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Ŀ������")]
    [Tooltip("��Ҫע�ӵ�Ŀ�����")]
    public Transform target;

    [Header("��ת����")]
    [Tooltip("����ƽ����ת")]
    public bool smoothRotation = true;
    [Tooltip("��תƽ���ٶ�")]
    [Range(0.1f, 5f)] public float rotationSpeed = 2f;

    void LateUpdate() {
        if (target == null)
            return;

        // ����Ŀ�귽��
        Vector3 direction = target.position - transform.position;

        if (direction != Vector3.zero) {
            // ����Ŀ����ת
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            if (smoothRotation) {
                // ƽ����ת
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            } else {
                // ֱ�ӳ���
                transform.rotation = targetRotation;
            }
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos() {
        if (target != null) {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, target.position);
            Gizmos.DrawWireSphere(target.position, 0.5f);
        }
    }
#endif
}
