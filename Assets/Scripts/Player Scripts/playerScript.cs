using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{
    private Rigidbody2D rb;

    //player
    [SerializeField] private Slider chargeSlider;
    private float _accelerate = 10f, deccelerate = 5f;
    private float _durToMaxSpeed = 1.5f;
    private float _playerChargeStrength;
    private Vector2 _inputVector;
    private Vector2 _currentvelocity;
    private bool _isUsingJetpack;

    private float timer;
    [SerializeField] private LayerMask groundMask;

    //jetpack
    [Header("Jetpack")]
    [SerializeField] private Slider fuelSlider;
    private float _jetpackInput;
    private float _fuelDepleteRate;
    private float _fuelRefileRate;
    private float _jetpackFuel;
    private float _jetpackThrustForce;
    private bool isGrounded = false;
    private bool startCharging = false;
    private bool releaseCharge = false;


    [Header("Reference")]
    [SerializeField] PlayerInput _inputs;
    InputActionMap _playerInputEditor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerInputEditor = _inputs.actions.FindActionMap("_playerMovement");
        _playerInputEditor.FindAction("Move").performed += onMovedPeformed;
        _playerInputEditor.FindAction("Move").canceled += onMovedCancelled;

        _playerInputEditor.FindAction("jetpackThrust").started += onPlayerJetpackPerformed;
        _playerInputEditor.FindAction("jetpackThrust").canceled += onPlayerJetpackCancelled;
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
        fuelSlider.maxValue = 100; // Set the maximum value of the slider
        fuelSlider.value = _jetpackFuel;

        _currentvelocity = Vector2.zero;
        if (playerManager.Instance != null)
        {
            _playerChargeStrength = playerManager.Instance.getPlayerChargeStrength();
            _fuelRefileRate = playerManager.Instance.getFuelRefileRate();
            _fuelDepleteRate = playerManager.Instance.getFuelDepleteRate();
            _jetpackFuel = playerManager.Instance.getFuel();
            _jetpackThrustForce = playerManager.Instance.getJetpackThrustForce();
            _isUsingJetpack = playerManager.Instance.getIsUsingJetpackStatus();
            Debug.Log(_playerChargeStrength);
        }
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    private void Update()
    {
        useJetpack();
        chargeSlider.transform.position = new Vector2(transform.position.x, transform.position.y + 2f);
    }

    #region move player
    void onMovedPeformed(InputAction.CallbackContext ctx)
    {
        _inputVector = ctx.ReadValue<Vector2>();
        _inputVector.y = 0f;
    }

    private void onMovedCancelled(InputAction.CallbackContext ctx) => _inputVector = Vector2.zero;


    void movePlayer()
    {
        if (_inputVector.x != 0)
        {
            timer += Time.deltaTime;
            // Ensure the timer doesn't go beyond the duration to max speed
            timer = Mathf.Clamp(timer, 0, _durToMaxSpeed);
            float t = timer / _durToMaxSpeed;
            _currentvelocity = new Vector2(Mathf.Lerp(0, _accelerate * _inputVector.x, t), rb.velocity.y);
            rb.velocity = _currentvelocity;
        }
        else
        {
            timer -= Time.deltaTime * deccelerate;
            timer = Mathf.Clamp(timer, 0, _durToMaxSpeed);
            float t = timer / _durToMaxSpeed;
            _currentvelocity = new Vector2(Mathf.Lerp(0, rb.velocity.x, t), rb.velocity.y);

            rb.velocity = _currentvelocity;
        }
    }
    #endregion

    #region jetpack logic
    public void onPlayerJetpackPerformed(InputAction.CallbackContext ctx)
    {
        _jetpackInput = ctx.ReadValue<float>();
        if (_jetpackInput > 0)
        {
            _isUsingJetpack = true;
        }
    }

    public void onPlayerJetpackCancelled(InputAction.CallbackContext ctx)
    {
        _jetpackInput = ctx.ReadValue<float>();

        if (_jetpackInput < 1)
        {
            _isUsingJetpack = false;
        }
    }

    void useJetpack()
    {
        // Fuel usage logic
        if (_isUsingJetpack && _jetpackFuel > 0)
        {
            _jetpackFuel -= _fuelDepleteRate * Time.deltaTime;

            if (_jetpackFuel > 0)
            {
                rb.AddForce(new Vector2(0, _jetpackThrustForce), ForceMode2D.Force);
            }
            else
            {
                // If fuel hits 0, stop using jetpack
                _isUsingJetpack = false;
            }
        }

        // Refilling logic
        if (isGrounded && _jetpackFuel < 100) // Refill fuel only when grounded
        {
            _jetpackFuel += _fuelRefileRate * Time.deltaTime;
        }

        _jetpackFuel = Mathf.Clamp(_jetpackFuel, 0, 100);
        fuelSlider.value = _jetpackFuel;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int groundLayer = LayerMask.NameToLayer("Ground");
        if (collision.gameObject.layer == groundLayer)
        {
            isGrounded = true;
           // Debug.Log("grounded");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        int groundLayer = LayerMask.NameToLayer("Ground");
        if (collision.gameObject.layer == groundLayer)
        {
            isGrounded = false;
            //Debug.Log("left ground");
        }
    }
    #endregion

    #region player charge logic
    void playerCharge()
    {

    }

    #endregion
}