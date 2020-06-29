using Assets.Scripts;

using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
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
    private GameObject _swordEffectGO;

    private Vector3 _offset = new Vector3(0.3f, 0, 0);

    [SerializeField]
    private int diamonds { get; set; }

    
    enum CharacterState
    {
        IDLE = 1,
        MOVING = 2,
        ATTACKIG
    }

    private CharacterState _state = CharacterState.IDLE;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _spriteRender = GetComponentInChildren<SpriteRenderer>();
        _swordEffectGO = transform.GetChild(1).gameObject;


    }

    bool launchedCoroutine = false;

    public float Health { get; set; } = 4;

    // Update is called once per frame
    void Update()
    {
                
        bool isGrounded = IsGrounded();
        if (Health > 0)
        {
            Movement(isGrounded);
            Attack(isGrounded);
        }

    }


    private void Attack(bool isGrounded)
    {
        bool attack = CrossPlatformInputManager.GetButtonDown("A_Button") ||Input.GetMouseButtonDown(1);
        if (attack)
        {
            if (isGrounded)
            {
                _playerAnimation.Attack();

            }
        }
    }
    
    private void Movement(bool isGrounded)
    {

        float move = Input.GetAxisRaw("Horizontal");

        if (move == 0)
        {
            move = CrossPlatformInputManager.GetAxis("Horizontal");
        }

        //flip
        if ((move < 0 && !_spriteRender.flipX) || (move>0 && _spriteRender.flipX))
        {
            _spriteRender.flipX = !_spriteRender.flipX;
            _swordEffectGO.transform.localPosition = new Vector3(-_swordEffectGO.transform.localPosition.x, _swordEffectGO.transform.localPosition.y, _swordEffectGO.transform.localPosition.z);
            _swordEffectGO.GetComponent<SpriteRenderer>().flipY = _spriteRender.flipX;
            
        }

        
        _rigidbody2D.velocity = new Vector2(move * _speed, _rigidbody2D.velocity.y);

        
        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("B_Button")) && isGrounded)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
        }

        _playerAnimation.Move(move, isGrounded);
        
    }

    
    private bool IsGrounded()
    {
       RaycastHit2D hitInfo = Physics2D.Raycast(transform.position + _offset, Vector2.down, 0.8f, _groundLayer);
        if (hitInfo.collider != null)
        {
            return true;
        }
        else
        {
            hitInfo = Physics2D.Raycast(transform.position - _offset, Vector2.down, 0.8f, _groundLayer);
            if (hitInfo.collider != null)
            {
                return true;
            }
        }

        return false;
    }

    public void Damage()
    {
        if (Health > 0)
        {
            Health--;
            UiManager.IUinstance.UpdateLives(Health);
            if (Health == 0)
            {
                _playerAnimation.Dead();
            }
        }
                
    }

    public void addDiamond(int diamond)
    {
        this.diamonds += diamond;
        UiManager.IUinstance.UpdateGemCount(this.diamonds);
    }

    public int Diamonds()
    {
        return diamonds;
    }

    public bool isLife()
    {
        return Health > 0;
    }
}
