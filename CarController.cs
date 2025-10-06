using UnityEngine;

public class CarController : MonoBehaviour
{
    public float forwardSpeed = 20f;
    public float sideSpeed = 8f;
    public float maxSpeed = 40f;
    public float acceleration = 0.2f;

    private float currentSpeed;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = forwardSpeed;
    }

    void Update()
    {
        // Forward acceleration (endless movement)
        currentSpeed += acceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, forwardSpeed, maxSpeed);

        // Move forward continuously
        Vector3 move = transform.forward * currentSpeed * Time.deltaTime;

        // Horizontal control (A/D or Left/Right)
        float h = Input.GetAxis("Horizontal");
        move += transform.right * h * sideSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + move);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AICar"))
        {
            Debug.Log("Game Over! You hit another car!");
            // Here you can trigger game over logic.
        }
    }
}
