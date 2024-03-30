
using Unity.Mathematics;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private readonly string Horizontal = "Horizontal";
    public readonly int isJump = Animator.StringToHash(nameof(isJump));
    public readonly int direction = Animator.StringToHash(nameof(direction));

    [SerializeField] private float _speed;
    [SerializeField] private float _JumpForce;
    [SerializeField] private float _checkGroundY;
    [SerializeField] private float _checkRadius;

    private Rigidbody2D _rigidbody;
    private float _direction;
    private bool _isRight = true;
    private int _rotateY = -1;
    private bool _isGrounded = false;
    private Animator _animator;
    private bool _isJump = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckGround();

        if(Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            _isJump = true;

        _direction = Input.GetAxis(Horizontal);
        Animate();

        if (_direction > 0 && _isRight == false)
            Flip();

        if (_direction < 0 && _isRight)
            Flip();
    }

    private void FixedUpdate()
    {
        Vector2 move = new Vector2(_direction * _speed, _rigidbody.velocity.y);
        _rigidbody.velocity = move;

        if(_isJump)
        {
            _rigidbody.AddForce(transform.up * _JumpForce, ForceMode2D.Impulse);
            _isJump = false;
        }
    }

    private void Flip()
    {
        _isRight=!_isRight;
        Vector3 directX = transform.localScale;
        directX.x *= _rotateY;
        transform.localScale = directX;
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y + _checkGroundY), _checkRadius);
        
        if (colliders.Length>1)
            _isGrounded = true;
        else
            _isGrounded = false;
    }
    private void Animate()
    {
        _animator.SetFloat(direction, math.abs(_direction));

        if (_isGrounded == false)
            _animator.SetBool(isJump, true);

        if (_isGrounded)
            _animator.SetBool(isJump, false);
    }
}
