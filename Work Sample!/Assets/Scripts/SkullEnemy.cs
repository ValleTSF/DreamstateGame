using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullEnemy : MonoBehaviour
{

    public float health;
    public int damage;
    public float distance;
    public LayerMask whatIsSolid;

    public float speed;

    private Vector2 target;
    private Transform player;

    public Transform skullPoint;

    public GameObject skullExplosion;
    public GameObject skullFlash;





    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        this.transform.position = skullPoint.position;
    }

    void FixedUpdate()
    {


        target = new Vector2(player.position.x, player.position.y);


        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);   /* I want my spell to travel to the players location and not the actual player. So this is where I use the target variable.
                                                                                                        If I would've used the player instead, the spell would just follow the player.  */ 

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (player.position.x > transform.position.x)
        {
            // Face right.
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (player.position.x < transform.position.x)
        {
            // Face left.
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    void OnTriggerEnter2D(Collider2D other)   // This will trigger when the spell touches another object with a Collider2D attached to it.
    {
        if (other.CompareTag("Player"))   // If the object the spell collides with is a player, run "DestroySpell()".
        {
            player.gameObject.SendMessage("Damage", damage);
            DestroySpell();
        }


    }

  

    public void Damage(int damage)
    {
        health -= damage;
    }


    void DestroySpell()   // When this is run the object this script is attached to will be destroyed.
    {
        Instantiate(skullFlash, transform.position, transform.rotation);
        Instantiate(skullExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
