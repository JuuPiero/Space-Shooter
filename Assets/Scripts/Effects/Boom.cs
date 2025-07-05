using UnityEngine;

public class Boom : MonoBehaviour
{
    private Animator _anim;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        Destroy(gameObject, GetAnimationLength());
    }

    private float GetAnimationLength()
    {
        return _anim.GetCurrentAnimatorStateInfo(0).length;
    }
}