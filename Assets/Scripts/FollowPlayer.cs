using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector3 _offset;

    public Transform Player;

    void Start()
    {
        _offset = transform.position;
    }

    void LateUpdate()
    {
        if (Player != null)
        {
            transform.position = Player.position + _offset;
        }
    }
}
