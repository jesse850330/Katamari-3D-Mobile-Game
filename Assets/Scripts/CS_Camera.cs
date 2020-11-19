using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Camera : MonoBehaviour
{
    public GameObject ball;
    Vector3 LookAtOffSet;

    void Start()
    {
        LookAtOffSet = new Vector3(0, 1.5f, 0);
    }

    void Update()
    {
        transform.LookAt(ball.transform.position + LookAtOffSet);
    }
}
