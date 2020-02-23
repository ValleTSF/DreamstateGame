using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour
{
    public float life;
    public GameObject muzzleFlash;
    public GameObject bullet;
    public Transform bulletPoint;
    private SpriteRenderer spriteR;
    public PlayerController playerController;



    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();  // I'm going to need the sprite renderer to flip my gun when my character turns around.
    }
    

    void Update()
    { 
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // I want the gun to flip when our character does so I use the sprite renderer to achieve this goal.
        if (Input.GetKeyDown(KeyCode.A))
        {
            spriteR.flipX = true;   // Flip the X dimension
            spriteR.flipY = true;   // Flip the Y dimension
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            spriteR.flipX = false;  // Flip X back.
            spriteR.flipY = false;  // Flip Y back.
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && playerController.isAlive == true)  // If the left mouse button is pressed execute code. I added isAlive to fix a bug that I had, my character was able to shoot when he was dead.
        {
            shoot();  // Execute the function "shoot".
            
        }

    }

    private void shoot()  // When the function shoot is executed, the bullet and muzzleFlash game objects will be instantiated.
    {
        Instantiate(bullet, transform.position, transform.rotation);
        Instantiate(muzzleFlash, transform.position, transform.rotation);
    }

}
