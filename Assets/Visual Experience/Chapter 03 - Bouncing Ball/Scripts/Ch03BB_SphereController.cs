using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch03BB_SphereController : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float fMaxHeight = 5.0f;
    [SerializeField] float fMaxMoveSpeed = 5.0f;

    Rigidbody rigidbody;

    float fHorizontalMove;

	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
	}

	void Update ()
    {
        float fHeight = Mathf.Abs(Mathf.Sin(Time.time * Mathf.PI) * fMaxHeight);
        float fMoveSpeed = rigidbody.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * fMaxMoveSpeed;

        rigidbody.position = new Vector3(fMoveSpeed, fHeight, rigidbody.position.z);
    }
}
