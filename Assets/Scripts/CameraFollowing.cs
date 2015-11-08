using UnityEngine;
using System.Collections;

public class CameraFollowing : MonoBehaviour {

    public Transform personaje;

	void Update ()
    {
        transform.position = new Vector3(personaje.position.x, personaje.position.y, -10);
	}
}
