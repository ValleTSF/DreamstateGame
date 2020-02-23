using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float life;
    public int damage;
    public float distance;
    public LayerMask whatIsSolid;


    public GameObject hitFlash;
    public GameObject destroyEffect;

    void Start()
    {
        Invoke("DestroyProjectile", life);
    }

    
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("EnemyBoss"))
            {
                hitInfo.collider.GetComponent<EnemyBehavior>().Damage(damage);
            }
            else if(hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<SkullEnemy>().Damage(damage);
            }
            DestroyProjectile();
        }

       
    }

    void DestroyProjectile()
    {
        Instantiate(hitFlash, transform.position, transform.rotation);
        Instantiate(destroyEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
