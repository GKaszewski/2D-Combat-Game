using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private CameraShake _cameraShake;
    private CombatSystem _combatSystem;
    private float _moveInput;
    private float _dashTime;
    private float _radius = 0.5f;
    private int _jumps;
    private int _dashDirection;
    private bool _isGrounded;

    public float speed;
    public float jumpForce;
    public float dashSpeed;
    public float dashStartTime;
    public int extraJumpsQuantity;
    [HideInInspector]
    public bool isDashing;
    [HideInInspector]
    public bool isFacingRight = true;
    public LayerMask groundLayer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cameraShake = GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>();
        _combatSystem = GetComponent<CombatSystem>();
        _dashTime = dashStartTime;
    }

    private void Update()
    {
        if (_isGrounded)
            _jumps = extraJumpsQuantity;

        Jump();

        if (_dashDirection == 0)
        {
            isDashing = false;
            if (Input.GetButtonDown("Dash") & Input.GetAxisRaw("Horizontal") > 0) //Right
                _dashDirection = 1;
            if (Input.GetButtonDown("Dash") & Input.GetAxisRaw("Horizontal") < 0) //Left
                _dashDirection = 2;
        }
        else
            isDashing = true;
    }

    private void FixedUpdate()
    {
        Move();

        if(isFacingRight == false & _moveInput > 0)
            Flip();
        else if(isFacingRight & _moveInput < 0)
            Flip();

        if (isDashing)
            Dash();
    }

    private void Move()
    {
        Vector3 playerFeet = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        _isGrounded = Physics2D.OverlapCircle(playerFeet, _radius, groundLayer);
        _moveInput = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(_moveInput * speed, _rb.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") & _jumps > 0)
        {
            _rb.velocity = Vector2.up * jumpForce;
            _jumps--;
        }
        else if (Input.GetButtonDown("Jump") & _jumps <= 0 & _isGrounded)
            _rb.velocity = Vector2.up * jumpForce;
    }

    private void Dash()
    {
        if (_dashTime <= 0)
        {
            _dashDirection = 0;
            _dashTime = dashStartTime;
        }
        else
        {
            _dashTime -= Time.deltaTime;

            switch (_dashDirection)
            {
                case 1:
                    StartCoroutine(_cameraShake.Shake(0.1f, 0.6f));
                    _rb.velocity = Vector2.right * dashSpeed;
                    break;
                case 2:
                     StartCoroutine(_cameraShake.Shake(0.1f, 0.6f));
                    _rb.velocity = Vector2.left * dashSpeed;
                    break;
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

}
