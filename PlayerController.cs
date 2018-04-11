using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed = 1.0f;

    //private Vector3 mousePosition;
    //public float moveSpeed = 0.1f;

    void Start () {
		
	}

	void Update () {
        var x = Input.GetAxis("Horizontal") * walkSpeed;
        var z = Input.GetAxis("Vertical") * walkSpeed;

        //transform.Translate(x, 0, z);
        var move = new Vector3(x, 0f, z);
        transform.position += Vector3.ClampMagnitude(move, walkSpeed) * Time.deltaTime;

        /*if (Input.GetMouseButton(1))
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }*/

        //transform.Rotate();
    }
}
