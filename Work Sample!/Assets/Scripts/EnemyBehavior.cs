using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    public float health;               // This variable will store the health value of the boss that I can change in Unity.
    public float speed;                // I'll be able to put a value in this variable from Unity.
    public float stoppingDistance;     // This will hold the distance in where I want the boss to stop following the player.
    public float retreatDistance;      // This will hold the distance in where I want the boss to retreat.

    public Slider hpBar;          // This will store information of the slider that I will use as a health bar in Unity.

    private Transform player;     // Here i will store the players transform attributes.
    private Vector2 target;       // This is where we want the spell to travel to.
    public Transform edgePoint;   // This will store the positional information of the game object edgePoint. Then be used later so that the boss doesn't run off the stage.

    public float lightningCooldown;  // This variable will store the value of how many seconds I want between the lightning bolt phases.
    private float lightningTiming;   // This variable will be given the value of lightningCooldown and then be used as a countdown timer.

    public float spellCooldown;        // I'll be able to put a value in this variable from Unity.
    private float spellTiming;         // Here I will store the value of spellCooldown.

    public GameObject lightningFlash;   // This will be used to instantiate an effect prefab.
    public GameObject lightningParent;  // I'll be able to insert a game object in here from Unity. The game object will then be referred to as "lightningParent". It will hold the script for actually shooting the lightning bolts.
    public GameObject fireBall;         // I'll be able to insert a game object in here from Unity. The game object will then be referred to as "fireBall". 

    public Transform spellHand;        // Here I'll be able to store another game object's transform attributes.

    public GameObject skullParent;     // I'll be able to insert a game object in here from Unity. The game object will then be referred to as "skullParents". It will hold the script for actually spawning the skull minions.
    public float skullCooldown;        // This variable will store the value of how many seconds I wan't between the skull minions phase.
    private float skullTiming;         // This variable will be given the value of skullCooldown and then be used as a countdown timer.

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();   // With this code I set the player transform attributes inside of the player variable.

        spellTiming = spellCooldown;          // Here the spellTiming variable will receive the value we put in the spellCooldown field inside of Unity.
        lightningTiming = lightningCooldown;  // Here the lightningTiming variable will receive the value we put in the lightningCooldown field inside of Unity.
        skullTiming = skullCooldown;          // Here the skullTiming variable will receive the value we put in the skullCooldown field inside of Unity.
    } 

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)   // This code means that if the current position of this object and the position of the player is greater than the "stoppingDistance", this will execute.

        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);   // This will move the object towards the target which is set to the player position.
        }

        // If the position between this object and the player is less than the "stoppingDistance" and this objects position and the player is greater than the "retreatDistance" this will execute.
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)  
        {
            transform.position = this.transform.position;   // Stay at this position.
        }
        // If the distance between this object and the player is less than the "retreatDistance" and the distance between this object and the game object "edgePoint" is greater than 10 this will execute.
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance && Vector2.Distance(transform.position, edgePoint.position) > 10 )  
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);   // The object will move towards the player's position at a negative speed value, making the object move away from it instead.
        }

        if(spellTiming <= 0)   // If the variable "spellTiming" has a value less or equals to 0, this will execute.
            
        {
              Instantiate(fireBall, spellHand.position, spellHand.rotation);   // This will spawn game object "fireBall" at the "spellHand" position with the "spellHand" rotation.
              spellTiming = spellCooldown;   // the "spellTiming" variable then receives the value of "spellCooldown" again.
        }

        else
        {
              spellTiming -= Time.deltaTime; // If "spellTiming" is not less or equals to zero take the "spellTiming" value and subtract it with time that has passed. 
        }

        if (lightningTiming <= 0) // If the variable "lightningTiming" has a value less or equals to 0, this will execute.

        {
            transform.localScale = new Vector3(0, 0, 0);                             // Give the game object the Vector3 dimensions of X = 0, Y = 0 and Z = 0. The goal is to make it look like the boss has teleported.
            Instantiate(lightningParent, transform.position, transform.rotation);    // Instantiate the game object "lightningParent"
            Instantiate(lightningFlash, transform.position, transform.rotation);     // Instantiate the game object "lightningFlash"
            lightningTiming = lightningCooldown;                                     // Here the value of lightningTiming is set back to the value of lightningCooldown, essentially resetting the timer.
            Invoke("Teleport", 10f);                                                 // This will execute the function "Teleport" after 10 seconds.
        }
 
        else

        {
            lightningTiming -= Time.deltaTime;  // If "lightningTiming" is not less or equals to zero take the "lightningTiming" value and subtract it with time that has passed. 
        }


        if (skullTiming <= 0)  // If the variable "skullTiming" has a value less or equals to 0, this will execute.
        {
            Instantiate(skullParent, transform.position, transform.rotation);  // Instantiate the game object "skullParent"
            skullTiming = skullCooldown;                                       // Here the value of skullTiming is set back to the value of skullCooldown, essentially resetting the timer.
        }

        else
        {
            skullTiming -= Time.deltaTime;   // If "skullTiming" is not less or equals to zero take the "skullTiming" value and subtract it with time that has passed. 
        }

        hpBar.value = health;     // Here I set the value of the slider "hpBar" to that of the game object's health.

        if(health <= 0)           // If the game object's health reaches a value of 0 or less than 0 this will execute.
        {
            Destroy(gameObject);  // This game object is destroyed.
        }
    }

    void Teleport()
    {
        transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);                // Give the game object the Vector3 dimensions of X = 0.75, Y = 0.75 and Z = 0.75. The goal is to make it look like the boss has teleported back.
        transform.localPosition = new Vector3(14, 20, 0);                       // Give the game object the Vector3 position of X = 14, Y = 20 and Z = 0. The boss will teleport back to this position.
        Instantiate(lightningFlash, transform.position, transform.rotation);    // Instantiate the game object "lightningFlash"
    }

    public void Damage(int damage)  // Here I created a public function so that the game object can receive a value from damage integers.
    {
        health -= damage;           // I take the health value of the game object and subtract it with the damage value that the game object has received.
    }

}

