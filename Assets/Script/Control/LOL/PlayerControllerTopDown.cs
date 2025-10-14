using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    // Hung da o day
    private PlayerInputHandler input;
    private PlayerMovement movement;

    private void Awake()
    {
        input = GetComponent<PlayerInputHandler>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (input.IsRightClick)
        {
            movement.MoveTo(input.MouseWorldPosition);
        }
    }
}
