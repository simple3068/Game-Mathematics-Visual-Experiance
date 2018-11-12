using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter09BulletBehaviour : MonoBehaviour
{
    [SerializeField] int nBulletPower;

    public string strStartName;

    Chapter09Controller ctrl;


    void Start ()
    {
        ctrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<Chapter09Controller>();
	}
	
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            ctrl.BulletHit();
            Destroy(gameObject);
        }

        if (other.CompareTag("Player 1") && other.tag != strStartName)
        {
            ctrl.nPlayer1HP -= nBulletPower;
            ctrl.BulletHit();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player 2") && other.tag != strStartName)
        {
            ctrl.nPlayer2HP -= nBulletPower;
            ctrl.BulletHit();
            Destroy(gameObject);
        }
    }
}
