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
    [SerializeField] private float chargeSpeed = 20f;
    [SerializeField] private float maxCharge = 100f;
    [SerializeField] private float accelerationDuration = .3f;
    private float _accelerate = 5f, deccelerate = 5f;
    private float _durToMaxSpeed = 1.5f;
    private float _playerChargeStrength;
    private float _playerChargeInput;
    private float playerChargeRelease;
    private float _currentCharge = 0f;
    private float accelerationTimer = 0f;
    private Vector2 _inputVector;
    private Vector2 _currentvelocity;
    private Vector2 _lastMovementDirection;
    private Vector2 targetForce;
    private bool _isChargingDash = false;
    private bool _reverseCharge = false;
    private bool isAccelerating = false;
    private bool stopMoveWhenChargingDash = false;
    private bool usingDash = false;

    private float timer;
    private float sliderTimer;
    //[SerializeField] private LayerMask groundMask;

    //jetpack
    [Header("Jetpack")]
    [SerializeField] private Slider fuelSlider;
    private float _jetpackInput;
    private float _fuelDepleteRate;
    private float _fuelRefileRate;
    private float _jetpackFuel;
    private float _jetpackThrustForce;
    private bool _isUsingJetpack = false;
    private bool isGrounded = false;
    private bool _haveJetpack;

    [Header("Reference")]
    [SerializeField] PlayerInput _inputs;
    InputActionMap _playerInputEditor;
    #region get input
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerInputEditor = _inputs.actions.FindActionMap("_playerMovement");
        _playerInputEditor.FindAction("Move").performed += onMovedPeformed;
        _playerInputEditor.FindAction("Move").canceled += onMovedCancelled;

        _playerInputEditor.FindAction("jetpackThrust").started += onPlayerJetpackPerformed;
        _playerInputEditor.FindAction("jetpackThrust").canceled += onPlayerJetpackCancelled;

        _playerInputEditor.FindAction("playerCharge").started += onPlayerChargePerformed;
        _playerInputEditor.FindAction("playerCharge").canceled += onPlayerChargeCancelled;
    }
    #endregion

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
        sliderTimer = 0f;
        fuelSlider.maxValue = 100; // Set the maximum value of the slider
        fuelSlider.value = _jetpackFuel;
        chargeSlider.gameObject.SetActive(false);
        _currentvelocity = Vector2.zero;

        if (playerManager.Instance != null)
        {
            _playerChargeStrength = playerManager.Instance.getPlayerChargeStrength();
            _fuelRefileRate = playerManager.Instance.getFuelRefileRate();
            _fuelDepleteRate = playerManager.Instance.getFuelDepleteRate();
            _jetpackFuel = playerManager.Instance.getFuel();
            _jetpackThrustForce = playerManager.Instance.getJetpackThrustForce();
            _isUsingJetpack = playerManager.Instance.getIsUsingJetpackStatus();
            _haveJetpack = playerManager.Instance.getHaveJetpack();
            //Debug.Log(_playerChargeStrength);
        }
    }

    private void FixedUpdate()
    {
        movePlayer();

        ApplyCharge();
    }

    private void Update()
    {
        useJetpack();
        playerChargeSliderVisual();
        chargeSlider.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
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
        if (_inputVector.x != 0 && !stopMoveWhenChargingDash)
        {
            timer += Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, _durToMaxSpeed);
            float t = timer / _durToMaxSpeed;
            _currentvelocity = new Vector2(Mathf.Lerp(0, _accelerate * _inputVector.x, t), rb.velocity.y);
            rb.velocity = _currentvelocity;

            // Store the last movement direction
            _lastMovementDirection = _inputVector.normalized; // Normalize to ensure direction is valid
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
        if (_jetpackInput > 0 && _haveJetpack)
        {
            _isUsingJetpack = true;
        }
    }

    public void onPlayerJetpackCancelled(InputAction.CallbackContext ctx)
    {
        _jetpackInput = ctx.ReadValue<float>();

        if (_jetpackInput < 1 && _haveJetpack)
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
        int boxLayer = LayerMask.NameToLayer("Box");

        if (collision.gameObject.layer == groundLayer)
        {
            isGrounded = true;
            // Debug.Log("grounded");
        }
        if (collision.gameObject.layer == boxLayer)
        {
            var colRB = collision.gameObject.GetComponent<Rigidbody2D>();
            if(usingDash)
            {
                colRB.bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                colRB.bodyType = RigidbodyType2D.Static;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("jetpack"))
        {
            _haveJetpack = true;
            collision.gameObject.SetActive(false);
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
    public void onPlayerChargePerformed(InputAction.CallbackContext ctx)
    {
        _playerChargeInput = ctx.ReadValue<float>();
        if (_playerChargeInput > 0)
        {
            _isChargingDash = true;
            stopMoveWhenChargingDash = true;
        }
    }

    public void onPlayerChargeCancelled(InputAction.CallbackContext ctx)
    {
        _playerChargeInput = ctx.ReadValue<float>();

        if (_playerChargeInput < 1)
        {
            _isChargingDash = false;
            _reverseCharge = false;
            stopMoveWhenChargingDash = false;

            float maxChargeStrength = _playerChargeStrength;
            // Calculate the percentage of charge (0 to 1)
            float chargePercentage = _currentCharge / maxCharge;

            // Calculate the final strength based on percentage of charge
            playerChargeRelease = maxChargeStrength * chargePercentage;

            // Reset the charge slider
            chargeSlider.value = 0f;
            // Debug log to check the released charge
            Debug.Log("Charge released: " + playerChargeRelease);
        }
    }


    void ApplyCharge()
    {
        // Check for player charge release
        if (!_isChargingDash && playerChargeRelease > 0)
        {
            if (isGrounded)
            {
                // Use the last movement direction for applying force
                targetForce = _lastMovementDirection * playerChargeRelease;

                // Start accelerating
                isAccelerating = true;
                accelerationTimer = 0f; // Reset the timer
                Debug.Log("Starting acceleration with force: " + targetForce);
            }

            playerChargeRelease = 0f;
        }

        // Handle acceleration
        if (isAccelerating)
        {
            accelerationTimer += Time.deltaTime; // Increment the timer
            usingDash = true;
            // Calculate the current acceleration
            float t = accelerationTimer / accelerationDuration;
            Vector2 currentForce = Vector2.Lerp(Vector2.zero, targetForce, t);

            // Apply the current force to the Rigidbody2D
            rb.velocity = currentForce;

            // Check if acceleration is complete
            if (t >= 1f)
            {
                isAccelerating = false; // Stop acceleration
                usingDash = false;
            }
        }
    }

    void playerChargeSliderVisual()
    {
        if (_isChargingDash && isGrounded)
        {
            chargeSlider.gameObject.SetActive(true);
            if (!_reverseCharge)
            {
                _currentCharge += chargeSpeed * Time.deltaTime;
                if (_currentCharge >= maxCharge)
                {
                    _currentCharge = maxCharge;
                    _reverseCharge = true;
                }
            }
            else
            {
                _currentCharge -= chargeSpeed * Time.deltaTime;
                if (_currentCharge <= 0f)
                {
                    _currentCharge = 0f;
                    _reverseCharge = false;
                }
            }

            chargeSlider.value = _currentCharge / maxCharge;
        }
        else
        {
            sliderTimer += Time.deltaTime;
            if (sliderTimer >.9f)
            {
                chargeSlider.gameObject.SetActive(false);
                sliderTimer = 0f;
            }

        }
        //Debug.Log(sliderTimer);
    }
    #endregion

}