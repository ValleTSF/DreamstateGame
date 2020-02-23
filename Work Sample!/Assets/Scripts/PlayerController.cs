using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    private Animator animator;  // I want to use the animator to play animations during movement inputs and attacks.
    private Rigidbody2D rb2d;   // I need the rigidbody2D to add velocity to my character.

    public float speed;     // I will use this variable to store a value that will act as my speed.
    public float jump;      // I will use this variable to store a value that will act as my jump force.
    public float health;    // This variable will store the player character's health.

    public Transform groundCheck;  // This variable stores the positional information of the game object "ground check"

    private bool facingRight;    // I want to use this bool to determine if I'm facing right or not.
    private bool sliding;        // I want to use this bool to determine if I'm sliding or not.
    private bool jumping;        // I want to use this bool to determine if I'm jumping or not.


    private bool isGrounded;    // I want to use this bool to determine if I'm grounded or not.
    public bool isAlive;        // I want to use this bool to determine if I'm alive or not.

    public GameObject DeathExplosion;   // I'll use an explosion prefab to get a visual effect when I die.

    void Start()
    {
        isAlive = true;        // When the game starts isAlive is set to true
        facingRight = true;    // When the game starts facingRight is set to true
        rb2d = GetComponent<Rigidbody2D>();  // I'm going to use rigidbody2d in my script.
        animator = GetComponent<Animator>(); // I'm going to use the animator in my script.
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {

        if(Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) // I'm using linecast to check what is directly below my character if its a layer with the name "Ground" execute code.
        {
            isGrounded = true;  // My character is grounded.
        }
        else
        {
            isGrounded = false;  // Otherwise my character is not grounded.
        }


        float horizontal = Input.GetAxis("Horizontal");  // I'll use a float variable called horizontal and store the "Horizontal" axis from Unity in it.


        Movement(horizontal);  

        Flip(horizontal);

        Reset();
        
        if(health <= 0)  // If heakth is equal or lower than 0 execute code.
        {
            transform.localScale = new Vector3(0, 0, 0);           // This will transform the object's scale to X=0 Y=0 and Z=0.
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;   // Freeze all dimensions of Rigidbody2D on the object.
            Instantiate(DeathExplosion, transform.position, transform.rotation);   // Instantiate the visual death effect at the objects position and rotation.
            FindObjectOfType<GameManager>().EndGame();      // Find GameManager and run the function "EndGame".
            isAlive = false;  // The player is no longer alive.
        }
        health = 20;    // I put this here as a fast fix to not have the death effect instantiate over and over until the game is restarted upon death.
    }

    private void Movement(float horizontal)
    {

        rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);   // This adds to the object velocity in the X axis while still retaining the current Y velocity. 

        animator.SetFloat("speed", Mathf.Abs(horizontal));  // This sends the float value of "speed" to my animator in Unity. This will trigger a transition in the animator.

        if (jumping && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))  // Ff jumping is true and the animator is not in the "Jump" state, execute code.
        {
            animator.SetBool("jumping", true); // Set the bool "jumping" in the animator to true.
        }
        else if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))  // Else if the animator is in the "Jump" state, execute code.
        {
            animator.SetBool("jumping", false);  // Set the bool "jumping" in the animator to false.
        }
    }


    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)   // If the key Space is down and object is grounded execute code.
        {
            jumping = true;  
            rb2d.velocity = new Vector2(rb2d.velocity.x, jump);  // Set the X velocity to current velocity and the Y velocity to the value of jump.
        }
    } 

    private void Flip(float horizontal)
    {
        if (horizontal > 0  && !facingRight || horizontal < 0 && facingRight)  // If the value of horizontal is grater than 0 and facingRight is false or if the horizontal is less than 0 and facingRight is true, execute code.
        {
            facingRight = !facingRight;  // FacingRight is now not facingRight.
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);  // Transform the objects scale to X = -1 Y = Object's Y and Z = Object's Z.
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeathZone"))  // If the collider of this object collides with another collider with the tag "DeathZone", execute code.
        {
            transform.localScale = new Vector3(0, 0, 0);           // This will transform the object's scale to X=0 Y=0 and Z=0.
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;   // Freeze all dimensions of Rigidbody2D on the object.
            Instantiate(DeathExplosion, transform.position, transform.rotation);   // Instantiate the visual death effect at the objects position and rotation.
            FindObjectOfType<GameManager>().EndGame();      // Find GameManager and run the function "EndGame".
            isAlive = false;  // The player is no longer alive.
        }

    }

    public void Damage(int damage)  // Here I created a public function so that the game object can receive a value from damage integers.
    {
        health -= damage;           // I take the health value of the game object and subtract it with the damage value that the game object has received.
    }

    private void Reset()
    {
        jumping = false;  // Set jumping to false.
    }



}
