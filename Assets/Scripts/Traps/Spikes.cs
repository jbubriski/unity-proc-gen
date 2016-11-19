using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        var playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth)
        {
            playerHealth.Kill();
        }
    }
}
