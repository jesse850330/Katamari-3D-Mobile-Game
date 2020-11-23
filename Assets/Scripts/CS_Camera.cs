using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Camera : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    

    void Start()
    {
        offset = target.position - this.transform.position;
    }

    void Update()
    {
        this.transform.position = target.position - offset;
    }
}
