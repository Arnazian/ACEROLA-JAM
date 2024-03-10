using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Transform target;
    public Camera mainCamera;
    public float edgeOffset = 0.05f;

    void Update()
    {
        Vector3 targetScreenPos = mainCamera.WorldToScreenPoint(target.position);
        Vector3 direction = (target.position - transform.position).normalized;

        // Rotate arrow to face the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Check if target is within screen bounds
        if (targetScreenPos.x >= 0 && targetScreenPos.x <= Screen.width && targetScreenPos.y >= 0 && targetScreenPos.y <= Screen.height)
        {
            // Target is within screen bounds, position arrow close to target
            transform.position = mainCamera.ScreenToWorldPoint(new Vector3(targetScreenPos.x, targetScreenPos.y, mainCamera.nearClipPlane + 1));
        }
        else
        {
            // Target is outside screen bounds, calculate edge position
            Vector3 edgePosition = targetScreenPos;

            if (targetScreenPos.x < 0) edgePosition.x = edgeOffset;
            else if (targetScreenPos.x > Screen.width) edgePosition.x = Screen.width - edgeOffset;

            if (targetScreenPos.y < 0) edgePosition.y = edgeOffset;
            else if (targetScreenPos.y > Screen.height) edgePosition.y = Screen.height - edgeOffset;

            // Convert back to world space, but keep the arrow in front of the camera
            edgePosition = mainCamera.ScreenToWorldPoint(new Vector3(edgePosition.x, edgePosition.y, mainCamera.nearClipPlane + 1));
            transform.position = new Vector3(edgePosition.x, edgePosition.y, transform.position.z);
        }
    }
}
