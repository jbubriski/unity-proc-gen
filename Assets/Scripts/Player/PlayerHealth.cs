using System.Collections;
using UnityEngine;

public class PlayerHealth : Entity
{
    private Camera _camera;
    private Transform _torch;
    private Transform _weapon;
    private Rigidbody _rigidBody;
    private LevelDirectorBase _levelDirector;
    private Transform _levelDirectorExtras;

    public int Health = 100;

    void Start()
    {
        _camera = Camera.main;
        _torch = transform.FindChild("Torch");
        _weapon = transform.FindChild("Weapon");
        _rigidBody = GetComponent<Rigidbody>();
        _levelDirector = FindObjectOfType<LevelDirectorBase>();
        _levelDirectorExtras = _levelDirector.transform.FindChild("Extras");

        _camera.GetComponent<FollowPlayer>().Player = transform;
    }

    void Update()
    {

    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "EnemyWeapon")
        {
            var weapon = collider.gameObject.GetComponentInParent<Weapon>();

            TakeDamage(weapon.Damage);
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
            Kill();
    }

    public void Kill()
    {
        if (IsDead)
            return;

        if (Health > 0)
            Health = 0;

        IsDead = true;

        _camera.GetComponent<FollowPlayer>().Player = null;
        _torch.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        _torch.GetComponent<Rigidbody>().freezeRotation = false;
        _torch.GetComponent<CapsuleCollider>().enabled = true;
        _torch.transform.parent = _levelDirectorExtras;

        _weapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        _weapon.GetComponent<Rigidbody>().freezeRotation = false;
        _weapon.GetComponent<BoxCollider>().enabled = true;
        _weapon.transform.parent = _levelDirectorExtras;

        _rigidBody.constraints = RigidbodyConstraints.None;

        _rigidBody.AddForce(30, 100, 30);

        ResetGame();
    }

    protected void ResetGame()
    {
        var coroutine = ResetGameInternal();
        StartCoroutine(coroutine);
    }

    private IEnumerator ResetGameInternal()
    {
        yield return new WaitForSeconds(5);

        Destroy(gameObject);
    }
}
