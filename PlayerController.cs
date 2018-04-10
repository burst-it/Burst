using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed = 1.0f;

    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;

    void Start () {
		
	}

	void Update () {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * walkSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * walkSpeed;

        transform.Translate(x, 0, z);

        if (Input.GetMouseButton(1))
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }

        //transform.Rotate();
    }
}
