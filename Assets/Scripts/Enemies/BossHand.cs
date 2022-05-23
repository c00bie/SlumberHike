using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : MonoBehaviour
{
    public float duration = 1f;
    public Vector3 end = Vector3.zero;
    private Vector3 start;
    private float time = 0;

    void Awake()
    {
        start = transform.position;
    }

    void Update()
    {
        if (time < duration)
        {
            transform.position = Vector3.Lerp(start, end, time / duration);
            time += Time.deltaTime;
        }
    }
}
