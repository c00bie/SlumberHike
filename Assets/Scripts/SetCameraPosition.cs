using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraPosition : MonoBehaviour
{
    public Vector3 cameraPosition;

    void Start()
    {
        gameObject.transform.position = cameraPosition;
    }
}
