using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter08Controller : MonoBehaviour
{
    [SerializeField] Transform trsCube;
    [SerializeField] Rigidbody rigidCube;
    [Space]
    [SerializeField] InputField ifStartVelocity;
    [SerializeField] InputField ifAcceleration;
    [Space]
    [SerializeField] Text txtCurrPosition;
    [SerializeField] Text txtCurrTime;
    [SerializeField] Text txtCurrVelocity;
    [Space]
    [SerializeField] Button btnStart;
    [SerializeField] Button btnPause;
    [SerializeField] Button btnStop;

    float fStartVelocity;
    float fAcceleration;

    float fStartedTime;

    bool bIsStarted = false;
    bool bIsPaused = false;
    
	void Start ()
    {
        rigidCube = trsCube.GetComponent<Rigidbody>();

        btnStart.onClick.AddListener(OnButtonStartClicked);
        btnPause.onClick.AddListener(OnButtonPauseClicked);
        btnStop.onClick.AddListener(OnButtonStopClicked);

        btnPause.enabled = false;
        btnStop.enabled = false;
	}
	
	void Update ()
    {
		if (bIsStarted)
        {
            txtCurrPosition.text = "X(현재 위치): " + trsCube.position.x;
            txtCurrTime.text = "t(현재 시간): " + (Time.time - fStartedTime);
            txtCurrVelocity.text = "V_f(현재속도): " + rigidCube.velocity.magnitude * (rigidCube.velocity.x / Mathf.Abs(rigidCube.velocity.x));
        }
	}

    //
    void OnButtonStartClicked()
    {
        if (!float.TryParse(ifStartVelocity.text, out fStartVelocity))
        {
            Debug.LogError("Invalid Value for Start Velocity!");
            ifStartVelocity.text = "";
            return;
        }
        if (!float.TryParse(ifAcceleration.text, out fAcceleration))
        {
            Debug.LogError("Invalid Value for Acceleration!");
            ifAcceleration.text = "";
            return;
        }

        fStartedTime = Time.time;

        rigidCube.velocity = new Vector3(fStartVelocity, 0.0f, 0.0f);
        StartCoroutine("CubeAcceleration");

        bIsStarted = true;
        btnStart.enabled = false;
        btnPause.enabled = true;
        btnStop.enabled = true;
    }
    void OnButtonPauseClicked()
    {
        if (!bIsPaused)
        {
            bIsPaused = true;
            btnStop.enabled = false;

            btnPause.GetComponentInChildren<Text>().text = "RESUME";
            Time.timeScale = 0.0f;
        }
        else
        {
            bIsPaused = false;
            btnStop.enabled = true;

            btnPause.GetComponentInChildren<Text>().text = "PAUSE";

            Time.timeScale = 1.0f;
        }
    }
    void OnButtonStopClicked()
    {
        bIsStarted = false;
        btnStart.enabled = true;
        btnPause.enabled = false;
        btnStop.enabled = false;

        StopAllCoroutines();

        rigidCube.velocity = Vector3.zero;
        trsCube.position = Vector3.zero;

        txtCurrPosition.text = "X(현재 위치): 0";
        txtCurrTime.text = "t(현재 시간): 0";
        txtCurrVelocity.text = "V_f(현재속도): 0";
    }

    //
    IEnumerator CubeAcceleration()
    {
        while (true)
        {
            rigidCube.AddForce(new Vector3(fAcceleration, 0.0f, 0.0f), ForceMode.Acceleration);
            yield return new WaitForEndOfFrame();
        }
    }
}
