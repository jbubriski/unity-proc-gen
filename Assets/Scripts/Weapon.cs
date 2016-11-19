using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Animator _animator;

    public int Damage = 10;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void Attack()
    {
        if (Random.Range(0f, 2f) > 1)
        {
            _animator.SetTrigger("Attack");
        }
        else
        {
            _animator.SetTrigger("Attack2");
        }
    }

    public bool IsReadyToAttack()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
    }
}
