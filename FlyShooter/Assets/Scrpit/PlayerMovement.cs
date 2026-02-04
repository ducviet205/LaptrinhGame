using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    void Update()
    {
        // Chỉ di chuyển khi giữ chuột trái
        if (Input.GetMouseButton(0)) // 0 = chuột trái
        {
            var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = 0;
            transform.position = worldPoint;
        }
    }
}
