using UnityEngine;

public class PlayerManager
{
    private static PlayerHealth _playerHealth;

    public static PlayerHealth GetPlayer ()
    {
        if (_playerHealth == null)
        {
            _playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        }

        return _playerHealth;
    }
}
