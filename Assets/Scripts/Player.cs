using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float _speed;
    private bool _flipX;
    private float _direction;

    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _checkRadius;
    private bool _isGround;

    private bool _isDie;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!_isDie)
        {
            GroundCheck();
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (!_isDie)
        {
            Move();
        }
        
    }

    private void Move()
    {
        _direction = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(transform.right.x * _direction * _speed, rb.velocity.y, transform.right.z * _direction * _speed);
        animator.SetFloat("directionX", MathF.Abs(_direction));
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (!_flipX && _direction < 0)
        {
            spriteRenderer.flipX = true;
            _flipX = true;
        }
        if (_flipX && _direction > 0)
        {
            spriteRenderer.flipX = false;
            _flipX = false;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGround) rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void GroundCheck()
    {
        if (Physics.OverlapSphere(_groundCheck.position, _checkRadius, _groundLayer).Length == 0) _isGround = false;
        else _isGround = true;

        animator.SetBool("ground", _isGround);
        animator.SetFloat("directionY", rb.velocity.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == null) return;
        if (collision.collider.GetComponent<BoxCollider>()) WorldManager.Instance.PlayerEnterOnBox(collision.transform);

    }

    public void Die()
    {
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        _isDie = true;
        animator.SetTrigger("die");
    }

    public void Respawn()
    {
        rb.velocity = Vector3.zero;
        rb.isKinematic = false;
        _isDie = false;
        WorldManager.Instance.RestartPlayer();
    }
}
