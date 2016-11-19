using UnityEngine;

public class PlayerMovement : Entity
{
    private Rigidbody _rigidBody;
    private PlayerHealth _playerHealth;
    private VirtualJoystick _virtualJoystick;
    private Transform _mesh;
    private Weapon _weapon;

    private bool _isAttacking;

    public float Speed = 8;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _playerHealth = GetComponent<PlayerHealth>();
        _mesh = transform.FindChild("Model");
        _weapon = GetComponentInChildren<Weapon>();

        _virtualJoystick = FindObjectOfType<VirtualJoystick>();
    }

    void Update()
    {
        if (_playerHealth.IsDead)
            return;

        var x = _virtualJoystick.Horizontal();
        var y = _virtualJoystick.Vertical();

        if (x != 0 || y != 0)
        {
            _rigidBody.velocity = new Vector3(x * Speed, _rigidBody.velocity.y, y * Speed);

            TurnTowardsVector(transform, new Vector3(x, 0, y));
        }

        var fire2 = Input.GetButton("Fire2");


        if (_isAttacking)
        {
            if (_weapon.IsReadyToAttack())
            {
                _isAttacking = false;
            }
        }
        else if (fire2)
        {
            if (_weapon != null)
            {
                _weapon.Attack();

                _isAttacking = true;
            }
        }
    }
}
