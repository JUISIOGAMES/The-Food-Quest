using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		speed = baseSpeed;
	}

	public InputManager inputManager;
	public float baseSpeed;
	public float jumpLenght;

	private float speed;

	// Update is called once per frame
	void Update () 
	{
		if (inputManager.LeftPressed ()) 
		{
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-speed, GetComponent<Rigidbody2D> ().velocity.y);
		}
		else 
		{
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);

			if (inputManager.RightPressed ()) 
			{
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed, GetComponent<Rigidbody2D> ().velocity.y);
			}
			else 
			{
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
			}
		}

		if (inputManager.UpPressed ())
		{
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jumpLenght);
		}
	}
}
