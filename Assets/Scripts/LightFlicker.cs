using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour
{
    private Light _light;
    private float _baseIntensity;

    public float FlickerAmount = 2;
    public float FlickerSpeed = 0.2f;

    void Start()
    {
        _light = GetComponent<Light>();

        _baseIntensity = _light.intensity;

        var coroutine = Flicker();
        StartCoroutine(coroutine);
    }

    void Update()
    {
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            FlickerSpeed = Random.Range(0f, 0.1f);
            _light.intensity = _baseIntensity + Random.Range(0, FlickerAmount);
            yield return new WaitForSeconds(FlickerSpeed);
        }
    }
}
