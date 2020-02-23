using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullParent : MonoBehaviour
{
    public GameObject skullEnemy1;
    public GameObject skullEnemy2;
    public GameObject skullEnemy3;

    public float timeBetweenSkulls;
    private float startTime;

    private float startDestructTime;
    public float destructTime;

    int whatToSpawn;

    private void Start()
    {
        startTime = timeBetweenSkulls;
        startDestructTime = destructTime;

    }



    private void FixedUpdate()
    {
        if (startTime <= 0)
        {
            whatToSpawn = Random.Range(1, 4);

            switch (whatToSpawn)
            {
                case 1:
                    Instantiate(skullEnemy1, this.transform.position, this.transform.rotation);
                    break;
                case 2:
                    Instantiate(skullEnemy2, this.transform.position, this.transform.rotation);
                    break;
                case 3:
                    Instantiate(skullEnemy3, this.transform.position, this.transform.rotation);
                    break;

            }

            startTime = timeBetweenSkulls;

        }
        else
        {
            startTime -= Time.deltaTime;
        }

        if (startDestructTime <= 0)
        {
            DestroySkullParent();
        }
        else
        {
            startDestructTime -= Time.deltaTime;
        }
    }



    private void DestroySkullParent()
    {
        Destroy(gameObject);
    }
}
