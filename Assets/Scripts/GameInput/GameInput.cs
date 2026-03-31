using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    public event EventHandler onPlayerJumped;
    public event EventHandler onPlayerInteract;
    public event EventHandler onPlayerHack;

    PlayerInput _playerInput;

    float _keyHeldTime = 1;

    bool _isSprinting;
    bool _hackHeldDown;

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
        _playerInput.player.jump.Enable();
        _playerInput.player.nextDialogue.Enable();
        _playerInput.player.interact.Enable();
        _playerInput.player.hack.Enable();
    }

    private void Start()
    {
        _playerInput.player.sprint.performed += Sprint_performed;
        _playerInput.player.sprint.canceled += Sprint_canceled;
        _playerInput.player.jump.performed += Jump_performed;
        _playerInput.player.nextDialogue.performed += NextDialogue_performed;
        _playerInput.player.interact.performed += Interact_performed;
        _playerInput.player.hack.performed += Hack_performed;
        _playerInput.player.hack.canceled += Hack_canceled;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Hack_canceled(InputAction.CallbackContext obj)
    {
        _hackHeldDown = false;
    }

    private void Hack_performed(InputAction.CallbackContext obj)
    {
        StartCoroutine(holdKey());
    }

    IEnumerator holdKey()
    {
        _hackHeldDown = true;
        float timer = 0;
        while (_hackHeldDown)
        {
            if (_hackHeldDown == false) break;
            timer += Time.deltaTime;
            yield return null;
            if (timer > _keyHeldTime)
            {
                _hackHeldDown = false;
                onPlayerHack?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        onPlayerInteract?.Invoke(this,EventArgs.Empty);
    }

    private void NextDialogue_performed(InputAction.CallbackContext obj)
    {
        DialogueManager.Instance.displayNextSentence();
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        onPlayerJumped?.Invoke(this, EventArgs.Empty);
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
