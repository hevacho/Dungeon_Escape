using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    
    [SerializeField]
    private float _jumpForce = 5.0f;

    [SerializeField]
    private float _speed = 1.0f;

    [SerializeField]
    private LayerMask _groundLayer;

    private PlayerAnimation _playerAnimation;
    private SpriteRenderer _spriteRender;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _spriteRender = GetComponentInChildren<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    
    private void Movement()
    {
        //GetAxisRaw obtenemos -1 o 1 sin intermedios
        float move = Input.GetAxisRaw("Horizontal");

        //flipS¡
        if ((move < 0 && !_spriteRender.flipX) || (move>0 && _spriteRender.flipX))
        {
            _spriteRender.flipX = !_spriteRender.flipX;
        }

        _playerAnimation.Move(move);
        _rigidbody2D.velocity = new Vector2(move * _speed, _rigidbody2D.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
        }
    }

    
    private bool IsGrounded()
    {
       RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, _groundLayer);
        if (hitInfo.collider != null)
        {
            return true;
        }

        return false;
    }


}
