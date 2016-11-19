using UnityEngine;
using System.Collections;

public class LightBall : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        transform.eulerAngles = new Vector3(Mathf.Sin(Time.time)  * 180, Mathf.Sin(Time.time)  * 180, Mathf.Sin(Time.time) * 180);
    }
}
