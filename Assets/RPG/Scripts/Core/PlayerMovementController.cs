using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerMovement4Dir : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal"); // A/D, Left/Right
        float z = Input.GetAxisRaw("Vertical");   // W/S, Up/Down

        var input = new Vector3(x, 0f, z);
        if (input.sqrMagnitude > 1f) input.Normalize();

        transform.position += input * (moveSpeed * Time.deltaTime);

        // Quay mặt theo hướng di chuyển (nếu đang di chuyển)
        if (input.sqrMagnitude > 0.0001f)
        {
            transform.forward = Vector3.Lerp(transform.forward, input, 20f * Time.deltaTime);
        }
    }
}
