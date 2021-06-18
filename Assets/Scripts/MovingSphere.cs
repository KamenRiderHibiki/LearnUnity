using UnityEngine;

public class MovingSphere : MonoBehaviour
{
    void Update() {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        transform.localPosition = new Vector3(playerInput.x, 1f, playerInput.y);
    }
}
