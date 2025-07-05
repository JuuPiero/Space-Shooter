using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private float _backgroundImageWidth;

    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        _backgroundImageWidth = sprite.bounds.size.x;
    }

    void Update()
    {
        float moveX = _moveSpeed * Time.deltaTime;
        if (Player.Instance != null)
        {
            moveX = (_moveSpeed * Player.Instance.boost) * Time.deltaTime;
        }
     
        transform.position += new Vector3(moveX, 0);
        if (Mathf.Abs(transform.position.x) - _backgroundImageWidth > 0){
            transform.position = new Vector3(0f, transform.position.y);
        }
    }
}