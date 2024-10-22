using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerScript : MonoBehaviour
{
    private Rigidbody2D rb;

    private float _accelerate = 5f, deccelerate = 5f;
    private float _durToMaxSpeed = 2;
    private float _moveSpeed;
    private float _playerChargeStrength;
    private float timer;
    private Vector2 _inputVector;
    private Vector2 _currentvelocity;

    [Header("Reference")]
    [SerializeField] PlayerInput _inputs;
    InputActionMap _playerInputEditor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerInputEditor = _inputs.actions.FindActionMap("_playerMovement");
        _playerInputEditor.FindAction("Move").performed += onMovedPeformed;
        _playerInputEditor.FindAction("Move").canceled += onMovedCancelled;
    }
    #region
    private void OnEnable()
    {
        _playerInputEditor.Enable();
    }
    private void OnDisable()
    {
        _playerInputEditor.Disable();
    }
    #endregion

    void Start()
    {
        timer = 0f;
        _currentvelocity = Vector2.zero;
        if(playerManager.Instance != null)
        {
            _moveSpeed = playerManager.Instance.getMoveSpeed();
            _playerChargeStrength = playerManager.Instance.getPlayerChargeStrength();
        }
    }
    private void Update()
    {
        movePlayer();
    }
    void onMovedPeformed(InputAction.CallbackContext ctx)
    {
        _inputVector = ctx.ReadValue<Vector2>();
        _inputVector.y = 0f;
    }

    private void onMovedCancelled(InputAction.CallbackContext ctx) => _inputVector = Vector2.zero;

    void movePlayer()
    {

    }
}
