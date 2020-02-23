using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muzzleFlash : MonoBehaviour
{

    public float lifetime;


   
    void FixedUpdate()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            DestroyFlash();
        }
    }


    private void DestroyFlash()
    {
        Destroy(gameObject);
    }
}
