using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private Rigidbody2D _rb;
    private Animator _anim;
    private Vector2 _inputDirection;
    [SerializeField] private float _speed;


    [Header("Boost energy")]
    public float boost = 1f;
    [SerializeField] private float _boostPower = 5f;
    private bool _boosting = false;

    [SerializeField] private float _maxEnergy;
    public float MaxEnergy => _maxEnergy;

    public event Action OnEnergyChanged;
    [SerializeField] private float _energy;
    public float Energy
    {
        get { return _energy; }
        set
        {
            _energy = value;
            OnEnergyChanged?.Invoke();
        }
    }
    [SerializeField] private float _energyRegen;
    [SerializeField] private float _energyDrain = 0.2f;


    [Header("Health")]
    [SerializeField] private int _health;
    public event Action OnHealthChanged;
    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            OnHealthChanged?.Invoke();
        }
    }
    [SerializeField] private int _maxHealth = 100;
    public float MaxHealth => _maxHealth;



    [SerializeField] private GameObject _destroyEffect;

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;

        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        Health = _maxHealth;
        Energy = _maxEnergy;
    }


    void Update()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");
        _inputDirection = new Vector2(directionX, directionY).normalized;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2"))
        {
            EnterBoost();
        }
        else if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire2"))
        {
            ExitBoost();
        }

        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        _anim.SetFloat("moveX", _inputDirection.x);
        _anim.SetFloat("moveY", _inputDirection.y);
        _anim.SetBool("boosting", _boosting);
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_inputDirection.x, _inputDirection.y) * _speed;

        if (_boosting)
        {
            if (Energy > _energyDrain) Energy -= _energyDrain;
            else ExitBoost();
        }
        else
        {
            Energy = Mathf.Min(Energy + _energyRegen, _maxEnergy);
        }
    }

    private void EnterBoost()
    {
        boost = _boostPower;
        _boosting = true;
    }

    private void ExitBoost()
    {
        boost = 1f;
        _boosting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        boost = 0f;
        gameObject.SetActive(false);
        Instantiate(_destroyEffect, transform.position, Quaternion.identity);
    }
}