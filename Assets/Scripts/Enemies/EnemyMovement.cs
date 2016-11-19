using UnityEngine;

public class EnemyMovement : Entity
{
    private Rigidbody _rigidBody;
    private EnemyHealth _enemyHealth;
    private Weapon _weapon;

    private GameObject _target;

    public float TurnSpeed = 5;
    public float MoveSpeed = 6;
    public float ViewDistance = 5;
    public float AttackDistance = 3;
    public float SpotDistance = 16;

    private bool _isAttacking = false;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _weapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        if (_enemyHealth.IsDead)
            return;

        if (_target == null)
        {
            var newTarget = FindObjectOfType<PlayerHealth>();

            if (newTarget != null)
            {
                _target = newTarget.gameObject;
            }
        }

        if (_target == null)
        {
            return;
        }

        if (_isAttacking)
        {
            if (_weapon.IsReadyToAttack())
            {
                _isAttacking = false;
            }

            return;
        }

        var distanceToTarget = Vector3.Distance(_target.transform.position, transform.position);
        var isLookingTowardsTarget = IsLookingTowardsTarget(_target.transform);

        if (isLookingTowardsTarget && distanceToTarget < AttackDistance)
        {
            if (_weapon != null)
            {
                _weapon.Attack();

                _isAttacking = true;
            }
        }
        else if (distanceToTarget < SpotDistance)
        {
            TurnTowardsTarget(_target.transform, TurnSpeed);

            if (isLookingTowardsTarget)
            {
                MoveTowardsTarget(_rigidBody, _target.transform, 1, MoveSpeed);
            }
        }
    }
}
