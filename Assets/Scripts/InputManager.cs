using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public KeyCode Left;
	public KeyCode Right;
	public KeyCode Up;

	public bool LeftPressed()
	{
		if (Input.GetKey(Left)) 
		{
			return true;
		}
		else return false;
	}

	public bool RightPressed()
	{
		if (Input.GetKey(Right)) 
		{
			return true;
		}
		else return false;
	}

	public bool UpPressed()
	{
		if (Input.GetKeyDown(Up)) 
		{
			return true;
		}
		else return false;
	}
}
