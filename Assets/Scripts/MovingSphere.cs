using UnityEngine;

public class MovingSphere : MonoBehaviour
{
    [SerializeField, Range(0f, 5f)]
    float maxSpeed = 1f;
    [SerializeField, Range(0f, 1f)]
    float maxAccleration = 0.1f;
    [SerializeField, Range(0f, 0.05f)]
    float friction = 0.001f;
    [SerializeField]
    Rect allowArea = new Rect(-4.5f, -4.5f, 9f, 9f);
    Vector3 velocity;
    Vector3 accelarion;
    void Awake()
    {
        velocity = accelarion = new Vector3(0, 0);
    }
    void Update() {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        if (!playerInput.Equals(Vector2.zero))
        {
            playerInput = Vector2.ClampMagnitude(playerInput, 1f);
            Vector3 desiredVelocity = new Vector3(playerInput.x, 0.0f, playerInput.y) * maxSpeed;
            float speedChange = maxAccleration * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, speedChange);
            velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, speedChange);
        }
        else
        {
            float frictionNow = friction * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(velocity.x, 0f, frictionNow);
            velocity.z = Mathf.MoveTowards(velocity.z, 0f, frictionNow);
        }
        velocity += accelarion * Time.deltaTime;
        Vector3 newPosition = transform.localPosition + velocity;
        if (!allowArea.Contains(new Vector2(newPosition.x, newPosition.z)))
        {
            if(newPosition.x < allowArea.xMin || newPosition.x>allowArea.xMax)
            {
                velocity.x *= -1;
            }
            if (newPosition.z < allowArea.yMin || newPosition.z > allowArea.yMax)
            {
                velocity.z *= -1;
            }
            newPosition.x = Mathf.Clamp(newPosition.x, allowArea.xMin, allowArea.xMax);
            newPosition.z = Mathf.Clamp(newPosition.z, allowArea.yMin, allowArea.yMax);
        }
        transform.localPosition = newPosition;
    }
}
