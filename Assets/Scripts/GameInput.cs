using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    PlayerInput _playerInput;

    bool _isSprinting;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        Instance = this;
    }

    private void OnEnable()
    {
        _playerInput.player.move.Enable();
        _playerInput.player.sprint.Enable();
        _playerInput.player.look.Enable();
    }

    private void Start()
    {
        _playerInput.player.sprint.performed += Sprint_performed;
        _playerInput.player.sprint.canceled += Sprint_canceled;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Sprint_canceled(InputAction.CallbackContext obj)
    {
        _isSprinting = false;
    }

    private void Sprint_performed(InputAction.CallbackContext obj)
    {
        _isSprinting = true;
    }

    public Vector3 GetInputVectorNormalized()
    {
        Vector2 MoveInputVector = _playerInput.player.move.ReadValue<Vector2>();

        return new Vector3(MoveInputVector.x,0,MoveInputVector.y).normalized;
    }
    public Vector2 GetMouseDelta()
    {
        Vector2 MoveDelVector = _playerInput.player.look.ReadValue<Vector2>();
        return MoveDelVector;
    }

    public bool PlayerIsSprinting()
    {
        return _isSprinting;
    }
}
