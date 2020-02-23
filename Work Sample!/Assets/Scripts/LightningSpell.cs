using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpell : MonoBehaviour
{

    public int damage;       // This integer will store the damage which can be changed in Unity.
    public float speed;      // This will be used determine the speed of the spell.

    public Transform lightningPoint;   // This variable will store the position of the spawn point of the lightning bolt. I will set it in Unity.
    public Transform lightningTarget;  // This variable will store the position of the target point of the lightning bolt. I will set it in Unity.
    private Vector2 target;            // This variable will store the information from lightningTarget.

    private Transform player;          // This variable will store the position of the player.

    // These will be used for spell effects.
    public GameObject lightningExplosion;
    public GameObject lightningFlash;
    public GameObject lightningRain;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;                 // The player variable is now storing the transform information of the game object "Player"
        target = new Vector2(lightningTarget.position.x, lightningTarget.position.y);  // The target variable is now storting the X and Y position of our target.
        this.transform.position = lightningPoint.position;                             // The position of this game object(lightning bolt) is now set to the position of the lightningPoint game object.
        GameObject.Find("LightningHand").transform.localScale = new Vector3(1, 1, 1);  // Find the game object "LightningHand" and give it the Vector3 dimensions X = 1, Y = 1 and Z = 1.
        GameObject.Find("LightningHand").transform.position = new Vector2(transform.position.x, transform.position.y -65);  // Find the game object "LightningHand" and give it this game object's X and Y position.
        Instantiate(lightningFlash, transform.position, transform.rotation);  // Spawn the lightningFlash game object at this game objects position and rotation.
        Instantiate(lightningRain, transform.position, transform.rotation);   // Spawn the lightningRain game object at this game objects position and rotation.
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);   // Move this game object towards the position of the target at the speed variable value set in Unity.

        if (transform.position.x == target.x && transform.position.y == target.y)   // If the spell reaches the target position run the code.
        {
            DestroySpell();  // run the DestroySpell function.
        }

    }

    void OnTriggerEnter2D(Collider2D other)   // This will trigger when the spell touches another object with a Collider2D attached to it.
    {
        if (other.CompareTag("Player"))   // If the object the spell collides with is a player, run the code.
        {   
            DestroySpell();  // run the DestroySpell function.
            player.gameObject.SendMessage("Damage", damage);  // This sends the damage value to the player object.
        }
    }

    void DestroySpell()   
    {
        GameObject.Find("LightningHand").transform.localScale = new Vector3(0, 0, 0);   // Find the game object "LightningHand" and give it the Vector3 dimensions X = 0, Y = 0 and Z = 0.
        Instantiate(lightningFlash, transform.position, transform.rotation);   // Spawn the lightningFlash game object at this game objects position and rotation.
        Instantiate(lightningExplosion, transform.position, transform.rotation);  // Spawn the lightningRain game object at this game objects position and rotation.
        Destroy(gameObject);  // Destroy this game object.
    }
}
