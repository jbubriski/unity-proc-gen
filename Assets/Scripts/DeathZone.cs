using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            var playerHealth = collider.gameObject.GetComponentInParent<PlayerHealth>();

            playerHealth.Kill();
        }

        if (collider.tag == "Enemy")
        {
            var enemyHealth = collider.gameObject.GetComponentInParent<EnemyHealth>();

            enemyHealth.Kill();
        }
    }
}
