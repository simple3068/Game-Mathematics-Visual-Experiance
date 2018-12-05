using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ch03BB_BoxBehaviour : MonoBehaviour
{
    [Header("Option")]
    [SerializeField] float fSpeed = 3.0f;

    Rigidbody rigidbody;

	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
	
	void Update ()
    {
        rigidbody.velocity = new Vector3(-fSpeed, 0.0f, 0.0f);

        if (transform.position.x < -14.0f)
            Destroy(gameObject);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
