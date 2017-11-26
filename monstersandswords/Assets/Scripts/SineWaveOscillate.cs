using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveOscillate : MonoBehaviour
{
    public float Wavelengh = 1.0f;
    public float Frequency = 1.0f;

    private float _timer = 0.0f;
    Vector3 _originalPos;

    private void Awake()
    {
        _originalPos = transform.position;
    }

    void Update ()
    {
        _timer += Time.deltaTime;
        transform.position = _originalPos + new Vector3( 0.0f, Wavelengh * Mathf.Sin(_timer * Frequency ), 0.0f );
	}
}
