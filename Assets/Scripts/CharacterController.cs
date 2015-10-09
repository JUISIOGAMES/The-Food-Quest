using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	// Use this for initialization
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
    private bool doubleJump;
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
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, 0.5f, Ground);
        if (isGrounded)
        {
            //If the character is touching the ground we make the doublejump avaliable
            doubleJump = true;
        }

        if (lateralCollider.IsTouchingLayers(Ground))
            lateralCollision = true;
        else
            lateralCollision = false;
    }
   
    void Update () 
	{
		if (inputManager.LeftPressed ()) 
		{
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-speed, GetComponent<Rigidbody2D> ().velocity.y);

            //If there is a change in direction, the character rotates 180 degrees in y axis
            if (facingRight)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                facingRight = false;
            }
		}
		else 
		{
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);

			if (inputManager.RightPressed ()) 
			{
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed, GetComponent<Rigidbody2D> ().velocity.y);

                //If there is a change in direction, the character rotates 180 degrees in y axis
                if (!facingRight)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    facingRight = true;
                }
			}
			else 
			{
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
			}
		}
        //Jumping
		if (inputManager.UpPressed ())
		{
            if (isGrounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpLenght);
            }
            else if (doubleJump)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpLenght);
                doubleJump = false;
            }
		}
	}
}
