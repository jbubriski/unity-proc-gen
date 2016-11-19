using UnityEngine;

public class FloorExit : MonoBehaviour
{
    public LevelDirectorBase LevelDirectorBase;

    void Start()
    {
        LevelDirectorBase = FindObjectOfType<LevelDirectorBase>();
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelDirectorBase.NextFloor();
        }
    }
}
