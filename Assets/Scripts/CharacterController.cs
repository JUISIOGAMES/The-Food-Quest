using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	void Start () 
	{
        //Initialise speed as baseSpeed Speed will be decreased when you are on air
        speed = baseSpeed;
	}

    Animator animator;

    void Awake()
    {
        //Animator component attached to the main character
        animator = GetComponent<Animator>();
    }

    //The input manager where input is checked and managed
	public InputManager inputManager;

    //Basic movement
    public float baseSpeed;
    public float jumpLenght;
	private float speed;
    public bool lateralCollision;
    public BoxCollider2D lateralCollider;

    //Things to make ground detection
    public Transform groundChecker;
    public bool isGrounded;
    public LayerMask Ground;

    private bool facingRight = true;

    void FixedUpdate()
    {
        //Animator variables
        if (GetComponent<Rigidbody2D>().velocity.x == 0)
            animator.SetBool("running", false);
        else
            animator.SetBool("running", true);

        //Checking if the character is on the ground
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, 0.45f, Ground);

        if (lateralCollider.IsTouchingLayers(Ground))
            lateralCollision = true;
        else
            lateralCollision = false;
    }
   
    void Update () 
	{
        if (lateralCollision)
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -4);

        if (inputManager.LeftPressed())
        {
            if (lateralCollision)
            {
                if (isGrounded)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);

                    //If there is a change in direction, the character rotates 180 degrees in y axis
                    if (facingRight)
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        facingRight = false;
                    }
                }
                else
                {
                    if (facingRight)
                        GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, jumpLenght);
                }
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);

                //If there is a change in direction, the character rotates 180 degrees in y axis
                if (facingRight)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    facingRight = false;
                }
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);

            if (inputManager.RightPressed())
            {
                if (lateralCollision)
                {
                    if (isGrounded)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);

                        //If there is a change in direction, the character rotates 180 degrees in y axis
                        if (!facingRight)
                        {
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            facingRight = true;
                        }
                    }
                    else
                    {
                        if (!facingRight)
                            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, jumpLenght);
                    }
                }
                else
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);

                    //If there is a change in direction, the character rotates 180 degrees in y axis
                    if (!facingRight)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        facingRight = true;
                    }
                }
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        //Jumping
		if (inputManager.UpPressed ())
		{
            if (isGrounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpLenght);
            }
		}
	}
}
