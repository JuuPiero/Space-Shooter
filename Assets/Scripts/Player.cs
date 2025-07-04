using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private Rigidbody2D _rb;
    private Animator _anim;
    private Vector2 _inputDirection;
    [SerializeField] private float _speed;

    public float boost = 1f;
    [SerializeField] private float _boostPower = 5f;
    private bool _boosting = false;


    [SerializeField] private float _energy;
    [SerializeField] private float _maxEnergy;
    [SerializeField] private float _energyRegen;



    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        _energy = _maxEnergy;
    }


    void Update()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");
        _anim.SetFloat("moveX", directionX);
        _anim.SetFloat("moveY", directionY);
        _inputDirection = new Vector2(directionX, directionY).normalized;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2"))
        {
            EnterBoost();
        }
        else if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire2"))
        {
            ExitBoost();
        }
        _anim.SetBool("boosting", _boosting);
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_inputDirection.x, _inputDirection.y) * _speed;

        if (_boosting)
        {

        }
        else
        {
            
        }
    }

    private void EnterBoost()
    {
        // _anim.SetBool("boosting", true);
        boost = _boostPower;
        _boosting = true;
    }
    
    private void ExitBoost()
    {
        // _anim.SetBool("boosting", false);
        boost = 1f;
        _boosting = false;
    }
}
