using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMove : MonoBehaviour
{

    public float speed;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * speed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += this.transform.forward * speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed;
        }
    }
}
