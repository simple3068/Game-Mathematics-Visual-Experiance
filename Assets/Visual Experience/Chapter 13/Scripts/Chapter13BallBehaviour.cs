using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter13BallBehaviour : MonoBehaviour
{
    [SerializeField] bool bKeepMoving = false;

    Rigidbody rigidBody;

	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = new Vector3(Random.Range(-20.0f, 20.0f), 0.0f, Random.Range(-20.0f, 20.0f));
	}
	
	void Update ()
    {
		if (bKeepMoving && rigidBody.velocity.magnitude < 0.1f)
        {
            rigidBody.velocity = new Vector3(Random.Range(-20.0f, 20.0f), 0.0f, Random.Range(-20.0f, 20.0f));
        }
	}
}
