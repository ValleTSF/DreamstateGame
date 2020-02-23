using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public int damage;         // This integer will hold the damage which can be changed in Unity.
    public float speed;        // This will be used determine the speed of the spell.

    private Transform player;  // I'm using this to acquire the player's position.
    private Vector2 target;    // This is where we want the spell to travel to.

    public GameObject explosion;   // This will be used for spell effects.
    public GameObject spellFlash;  // This will be used for spell effects.

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  // This code will locate the players position.

        target = new Vector2(player.position.x, player.position.y);     // The target variable will now hold the current position of the player.
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed *Time.deltaTime);   /* I want my spell to travel to the players location and not the actual player. So this is where I use the target variable.
                                                                                                        If I would've used the player instead, the spell would just follow the player.  */

       if(transform.position.x == target.x && transform.position.y == target.y)   // If the spell reaches the target position run "DestroySpell()".
        {
            DestroySpell();
        } 
    }

    void OnTriggerEnter2D(Collider2D other)   // This will trigger when the spell touches another object with a Collider2D attached to it.
    {
        if(other.CompareTag("Player"))   // If the object the spell collides with has a "Player" tag, execute the code.
        {
           DestroySpell();  // This runs the DestroySpell function. 
           player.gameObject.SendMessage("Damage", damage); // This sends the damage value to the player object.
        }
    }


    void DestroySpell()   // When this is run the object this script is attached to will be destroyed and effects will be instantiated.
    {
        Instantiate(spellFlash, transform.position, transform.rotation);
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
