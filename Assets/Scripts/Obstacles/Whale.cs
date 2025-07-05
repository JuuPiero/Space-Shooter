using UnityEngine;

public class Whale : MonoBehaviour
{
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