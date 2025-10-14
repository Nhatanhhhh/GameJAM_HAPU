using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector3 MouseWorldPosition { get; private set; }
    public bool IsRightClick { get; private set; }

    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        IsRightClick = Mouse.current.rightButton.wasPressedThisFrame;

        if (IsRightClick)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                MouseWorldPosition = hit.point;
            }
        }
    }
}
