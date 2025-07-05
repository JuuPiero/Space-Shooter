using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    [SerializeField] private Sprite[] _sprites;
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];

        float pushX = Random.Range(-1f, 0f);
        float pushY = Random.Range(-1f, 1f);
        _rb.linearVelocity = new Vector2(pushX, pushY);

    }

    void Update()
    {
        float moveX = GameManager.Instance.worldSpeed * Player.Instance.boost * Time.deltaTime;

        transform.Translate(new Vector2(-moveX, 0));

        if (transform.position.x < -11f)
        {
            Destroy(gameObject);
        }
    }
}