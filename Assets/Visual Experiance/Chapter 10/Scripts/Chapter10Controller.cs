using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter10Controller : MonoBehaviour
{
    [SerializeField] Transform trsPlayer1;
    [SerializeField] Transform trsPlayer2;
    [Space]
    [SerializeField] GameObject objBullet;
    [Space]
    [SerializeField] float fMaxStartSpeed = 20.0f;
    [Space]
    [SerializeField] Text txtHPP1;
    [SerializeField] Text txtHPP2;
    [Space]
    [SerializeField] Text txtAngleP1;
    [SerializeField] Text txtAngleP2;
    [Space]
    [SerializeField] Text txtStartSpeed;
    [Space]
    [SerializeField] Text txtDesc;
    [Space]

    public int nPlayer1HP = 100;
    public int nPlayer2HP = 100;

    public enum Ch10Status { None, FlyBy, Player1, Player2 };
    public Ch10Status status = Ch10Status.None;
    Ch10Status lastTurn = Ch10Status.Player1;

    float fStartSpeed;

    bool bGameOver = false;

    bool bDoneSetAngle = false;
    bool bDoneSetPower = false;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		if (!bGameOver)
        {
            txtHPP1.text = "HP: " + nPlayer1HP;
            txtHPP2.text = "HP: " + nPlayer2HP;

            switch(status)
            {
                case Ch10Status.None:
                    break;

                case Ch10Status.Player1:
                    // Set Player 2's Rotation to zero
                    if (trsPlayer2.rotation != Quaternion.identity)
                        trsPlayer2.rotation = Quaternion.identity;

                    if (!bDoneSetAngle)
                    {
                        txtDesc.text = "Player 1의 차례입니다. 각도를 조절한 후 스페이스바를 눌러주세요. ";

                        // First, set the angle of player's cannon
                        trsPlayer1.eulerAngles += new Vector3(0.0f, 0.0f, -Input.GetAxis("Horizontal"));
                        txtAngleP1.text = (trsPlayer1.eulerAngles.z - 270.0f) + "˚";

                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            bDoneSetAngle = true;
                            StartCoroutine("StartSpeedSelection");
                        }
                    }
                    else if (!bDoneSetPower)
                    {
                        txtDesc.text = "Player 1의 차례입니다. 적절한 초기속도 지정 후 스페이스바를 눌러주세요. ";

                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            StopCoroutine("StartSpeedSelection");

                            bDoneSetAngle = bDoneSetPower = false;
                            status = Ch10Status.FlyBy;

                            GameObject obj = Instantiate(objBullet, trsPlayer1.position + new Vector3(0.0f, 0.505f, 0.0f), trsPlayer1.rotation, null);
                            obj.GetComponent<Chapter10BulletBehaviour>().strStartName = trsPlayer1.tag;
                            obj.GetComponent<Rigidbody>().velocity = obj.transform.up * fStartSpeed;
                        }
                    }
                    if (lastTurn != Ch10Status.Player1)
                        lastTurn = Ch10Status.Player1;
                    break;

                case Ch10Status.Player2:
                    // Set Player 1's Rotation to zero
                    if (trsPlayer1.rotation != Quaternion.identity)
                        trsPlayer1.rotation = Quaternion.identity;

                    if (!bDoneSetAngle)
                    {
                        txtDesc.text = "Player 2의 차례입니다. 각도를 조절한 후 스페이스바를 눌러주세요. ";
                        txtAngleP2.text = (90.0f - trsPlayer2.eulerAngles.z) + "˚";

                        // First, set the angle of player's cannon
                        trsPlayer2.eulerAngles += new Vector3(0.0f, 0.0f, -Input.GetAxis("Horizontal"));

                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            bDoneSetAngle = true;
                            StartCoroutine("StartSpeedSelection");
                        }
                    }
                    else if (!bDoneSetPower)
                    {
                        txtDesc.text = "Player 2의 차례입니다. 적절한 초기속도 지정 후 스페이스바를 눌러주세요. ";

                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            StopCoroutine("StartSpeedSelection");

                            bDoneSetAngle = bDoneSetPower = false;
                            status = Ch10Status.FlyBy;

                            GameObject obj = Instantiate(objBullet, trsPlayer2.position + new Vector3(0.0f, 0.505f, 0.0f), trsPlayer2.rotation, null);
                            obj.GetComponent<Chapter10BulletBehaviour>().strStartName = trsPlayer2.tag;
                            obj.GetComponent<Rigidbody>().velocity = obj.transform.up * fStartSpeed;
                        }
                    }
                    if (lastTurn != Ch10Status.Player2)
                        lastTurn = Ch10Status.Player2;
                    break;

                case Ch10Status.FlyBy:
                    break;
            }

            if (nPlayer1HP <= 0 || nPlayer2HP <= 0)
            {
                bGameOver = true;
                status = Ch10Status.None;
            }
        }
        else
        {
            if (nPlayer1HP <= 0)
            {
                txtDesc.text = "Player 2가 이겼습니다!";
            }
            else if (nPlayer2HP <= 0)
            {
                txtDesc.text = "Player 1이 이겼습니다!";
            }
        }
	}

    IEnumerator StartSpeedSelection()
    {
        fStartSpeed = 0.0f;

        while (true)
        {
            while (fStartSpeed < fMaxStartSpeed)
            {
                fStartSpeed += 0.125f;
                txtStartSpeed.text = "초기속도: " + fStartSpeed;
                yield return new WaitForEndOfFrame();
            }

            while (fStartSpeed > 0.0f)
            {
                fStartSpeed -= 0.125f;
                txtStartSpeed.text = "초기속도: " + fStartSpeed;
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void BulletHit()
    {
        if (lastTurn == Ch10Status.Player1)
        {
            status = Ch10Status.Player2;
        }
        else if (lastTurn == Ch10Status.Player2)
        {
            status = Ch10Status.Player1;
        }
    }
}
