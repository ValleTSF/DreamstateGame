using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningParent : MonoBehaviour
{

    // These will house my Lightning Bolt prefabs created in Unity.
    public GameObject lightningBolt1;
    public GameObject lightningBolt2;
    public GameObject lightningBolt3;
    public GameObject lightningBolt4;
    public GameObject lightningBolt5;

    public float timeBetweenBolts;  // This float variable will hold the value of how many seconds I want between each lightning bolt.
    private float startTime;        // I will give this float the same value of the timeBetweenBolts variable but use it as a countdown.

    public float destructTime;        // This float variable will hold the value of how long I want the LightningParent to stay in the game.
    private float startDestructTime;  //  I will give this float the same value of the destructTime variable but use it as a countdown.

    int whatToSpawn; // This integer will hold the case number that will be randomly generated in the code further down. This will let the script know which prefab to instantiate.

    private void Start()
    {
        startTime = timeBetweenBolts;      // Here I set the startTime to equal the value I give timeBetweenBolts inside of Unity.
        startDestructTime = destructTime;  // Here I set the startDestructTime to equal the value I give destructTime inside of Unity.

    }



    private void FixedUpdate()
    {
        if (startTime <= 0)   // If startTime is less or equal to 0 the code will run.
        {
            whatToSpawn = Random.Range(1, 5);  // This is where whatToSpawn gets it's random case number.

            switch (whatToSpawn)  // These are all the different cases or lightning bolts in my case.
            {
                case 1:
                    Instantiate(lightningBolt1, this.transform.position, this.transform.rotation);
                    break;
                case 2:
                    Instantiate(lightningBolt2, this.transform.position, this.transform.rotation);
                    break;
                case 3:
                    Instantiate(lightningBolt3, this.transform.position, this.transform.rotation);
                    break;
                case 4:
                    Instantiate(lightningBolt4, this.transform.position, this.transform.rotation);
                    break;
                case 5:
                    Instantiate(lightningBolt5, this.transform.position, this.transform.rotation);
                    break;
            }

            startTime = timeBetweenBolts; // After a random lightning bolt has been chosen the timer resets by setting the startTime back to the timeBetweenBolts.

        }
        else   // This code is running if the startTime is not less or equal to 0.
        {
            startTime -= Time.deltaTime;  // This takes the value of startTime and subtracts it by how much time has passed.
        }

        if (startDestructTime <= 0)   // If startDestructTime is less or equal to 0 then run the code.
        {
            DestroyLightningParent(); // Run the DestroyLightningParent function.
        }
        else  // This code is running if the startDestructTime is not less or equal to 0.
        {
            startDestructTime -= Time.deltaTime;  // This takes the value of startDestructTime and subtracts it by how much time has passed.
        }
    }
       

   
    private void DestroyLightningParent()
    {
        Destroy(gameObject);  // When this is run the game object that holds this script will be destroyed.
    }
}
