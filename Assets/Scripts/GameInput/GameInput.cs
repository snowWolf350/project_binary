using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour,IHasProgress
{
    public static GameInput Instance;

    public event EventHandler onPlayerJumped;
    public event EventHandler onPlayerInteract;
    public event EventHandler onPlayerHack;
    public event EventHandler<IHasProgress.onProgressChangedEventArgs> onProgressChanged;

    PlayerInput _playerInput;

    float _keyHeldMax = 1;
    float _keyHeldTimer = 0;

    bool _isSprinting;
    bool _hackHeldDown;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _playerInput = new PlayerInput();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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

        _playerInput.player.sprint.performed += Sprint_performed;
        _playerInput.player.sprint.canceled += Sprint_canceled;
        _playerInput.player.jump.performed += Jump_performed;
        _playerInput.player.nextDialogue.performed += NextDialogue_performed;
        _playerInput.player.interact.performed += Interact_performed;
        _playerInput.player.hack.performed += Hack_performed;
        _playerInput.player.hack.canceled += Hack_canceled;
    }

    private void Hack_canceled(InputAction.CallbackContext obj)
    {
        _hackHeldDown = false;
        _keyHeldTimer = 0;
        onProgressChanged?.Invoke(this, new IHasProgress.onProgressChangedEventArgs
        {
            progressNormalized = _keyHeldTimer / _keyHeldMax
        });
    }

    private void Hack_performed(InputAction.CallbackContext obj)
    {
        StartCoroutine(holdKey());
    }

    IEnumerator holdKey()
    {
        _hackHeldDown = true;
        _keyHeldTimer = 0;
        while (_hackHeldDown)
        {
            if (_hackHeldDown == false) break;
            if (Player.Instance.GetCurrentHackable() == null) break;
            _keyHeldTimer += Time.deltaTime;
            onProgressChanged?.Invoke(this, new IHasProgress.onProgressChangedEventArgs
            {
                progressNormalized = _keyHeldTimer / _keyHeldMax
            });
            yield return null;
            if (_keyHeldTimer > _keyHeldMax)
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

    private void OnDestroy()
    {
        if (_playerInput != null)
        {
            _playerInput.player.sprint.performed -= Sprint_performed;
            _playerInput.player.sprint.canceled -= Sprint_canceled;
            _playerInput.player.jump.performed -= Jump_performed;
            _playerInput.player.nextDialogue.performed -= NextDialogue_performed;
            _playerInput.player.interact.performed -= Interact_performed;
            _playerInput.player.hack.performed -= Hack_performed;
            _playerInput.player.hack.canceled -= Hack_canceled;
        }

        if (Instance == this)
            Instance = null;

        _playerInput.Disable();
    }
}
