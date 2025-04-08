using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("目标设置")]
    [Tooltip("需要注视的目标对象")]
    public Transform target;

    [Header("旋转设置")]
    [Tooltip("启用平滑旋转")]
    public bool smoothRotation = true;
    [Tooltip("旋转平滑速度")]
    [Range(0.1f, 5f)] public float rotationSpeed = 2f;

    void LateUpdate() {
        if (target == null)
            return;

        // 计算目标方向
        Vector3 direction = target.position - transform.position;

        if (direction != Vector3.zero) {
            // 计算目标旋转
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            if (smoothRotation) {
                // 平滑旋转
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            } else {
                // 直接朝向
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
