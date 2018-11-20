using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter03Controller : MonoBehaviour
{
    [SerializeField] Transform trsCubeSin;
    [SerializeField] Transform trsCubeCos;
    [SerializeField] Transform trsCubeTan;
    [Space]
    [SerializeField] Slider sldAngle;
    [SerializeField] Text txtAngle;
    [SerializeField] Text txtSin;
    [SerializeField] Text txtCos;
    [SerializeField] Text txtTan;
    [Space]
    [SerializeField] Slider sldAmp;
    [SerializeField] Text txtAmp;
    [Space]
    [SerializeField] Transform trsAmpRange;
    [Space]
    [SerializeField] Button btnAutoRotation;

    bool bAutoRotation;

    float fSin;
    float fCos;
    float fTan;

    float fAmp = 1.0f;

    float fAngle;
    float fAngleInRad;

    void Start ()
    {
        btnAutoRotation.onClick.AddListener(delegate
        {
            bAutoRotation = !bAutoRotation;
        });
	}

    void Update()
    {
        // Code about Angle
        if (bAutoRotation)
        {
            btnAutoRotation.transform.localEulerAngles += new Vector3(0.0f, 0.0f, -1.0f);

            fAngle = Mathf.Repeat(fAngle + 1.0f, 360.0f);

            sldAngle.value = fAngle;
        }
        else
        {
            btnAutoRotation.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

            fAngle = sldAngle.value;
        }

        // Code about Angle
        fAngleInRad = fAngle * Mathf.Deg2Rad;
        txtAngle.text = fAngle.ToString("0.0000");

        // Code about Amplitude
        fAmp = sldAmp.value;
        txtAmp.text = fAmp.ToString("0.0000");

        trsAmpRange.localScale = new Vector3(trsAmpRange.localScale.x, fAmp * 2.0f, trsAmpRange.localScale.z);

        // Calculate Trigonometric Funcs
        fSin = fAmp * Mathf.Sin(fAngleInRad);
        fCos = fAmp * Mathf.Cos(fAngleInRad);
        fTan = fAmp * Mathf.Tan(fAngleInRad);

        // Apply calculation result to cube
        trsCubeSin.position = new Vector3(trsCubeSin.position.x, fSin, trsCubeSin.position.z);
        trsCubeCos.position = new Vector3(trsCubeCos.position.x, fCos, trsCubeCos.position.z);
        trsCubeTan.position = new Vector3(trsCubeTan.position.x, fTan, trsCubeTan.position.z);

        // print
        txtSin.text = fSin.ToString("0.0000");
        txtCos.text = fCos.ToString("0.0000");
        txtTan.text = fTan.ToString("0.0000");
    }
}
