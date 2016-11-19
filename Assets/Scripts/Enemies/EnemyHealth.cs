using System.Collections;
using UnityEngine;

public class EnemyHealth : Entity
{
    private Rigidbody _rigidBody;
    private Transform _weapon;
    private LevelDirectorBase _levelDirector;

    public int Health = 100;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _weapon = transform.FindChild("Weapon");
        _levelDirector = FindObjectOfType<LevelDirectorBase>();
    }

    void Update()
    {

    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "PlayerWeapon")
        {
            var weapon = collider.gameObject.GetComponentInParent<Weapon>();

            TakeDamage(weapon.Damage);
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0 && !IsDead)
            Kill();
    }

    public void Kill()
    {
        if (Health > 0)
            Health = 0;

        IsDead = true;

        if (_weapon != null)
        {
            _weapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            _weapon.GetComponent<Rigidbody>().freezeRotation = false;
            var animator = _weapon.GetComponentInChildren<Animator>();
            
            if (animator != null)
                animator.enabled = false;

            var colliders = _weapon.GetComponentsInChildren<Collider>();

            foreach (var collider in colliders)
            {
                collider.enabled = !collider.isTrigger;
            }

            _weapon.transform.parent = _levelDirector.transform;
        }

        _rigidBody.constraints = RigidbodyConstraints.None;

        _rigidBody.AddForce(30, 100, 30);

        DelayDestroy();
    }
}
