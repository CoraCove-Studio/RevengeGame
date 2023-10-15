using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public float power = .2f;
    public float duration = .2f;
    public float slowDownAmount = 1.0f;
    private bool shouldShake;
    private float initialDuration;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
        initialDuration = duration;
    }

    // Update is called once per frame
    void Update()
    {
        Shake();
    }

    void Shake()
    {
        //if we should shake the camera
        if (shouldShake)
        {
            if (duration > 0f)
            {
                transform.position = startPosition + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                transform.localPosition = startPosition;
            }
        }
    }

    //accessor
    public bool ShouldShake
    {
        get { return shouldShake; }
        set { shouldShake = value; }
    }
}
