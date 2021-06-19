using UnityEngine;

public class MovingSphere : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)]
    float maxSpeed = 5f;
    [SerializeField, Range(0f, 100f)]
    float maxAccleration = 10f;
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
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        Vector3 desiredVelocity = new Vector3(playerInput.x, 0.0f, playerInput.y) * maxSpeed;
        float speedChange = maxAccleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, speedChange);
        velocity.y = Mathf.MoveTowards(velocity.y, desiredVelocity.y, speedChange);
        velocity += accelarion * Time.deltaTime;
        Vector3 newPosition = transform.localPosition + velocity;
        if(!allowArea.Contains(new Vector2(newPosition.x, newPosition.y))){
            newPosition.x = Mathf.Clamp(newPosition.x, allowArea.xMin, allowArea.xMax);
        }
        transform.localPosition = newPosition;
    }
}
