﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningPoint : MonoBehaviour
{

    public Transform target;

    void Start()
    {

    }


    void Update()
    {
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
