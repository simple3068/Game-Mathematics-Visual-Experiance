using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter06Controller : MonoBehaviour
{
    [SerializeField] Dropdown ddTransformation;
    [Space]
    [Header("Option: Translate")]
    [SerializeField] GameObject objPanelTranslate;
    [SerializeField] InputField[] ifTranslate;
    [SerializeField] Button btnTranslateApply;
    [SerializeField] Button btnTranslateReset;
    [SerializeField] Button btnTranslateRandom;
    [Space]
    [Header("Option: Rotate")]
    [SerializeField] GameObject objPanelRotate;
    [SerializeField] InputField[] ifRotate;
    [SerializeField] Button btnRotateApply;
    [SerializeField] Button btnRotateReset;
    [SerializeField] Button btnRotateRandom;
    [SerializeField] Toggle tglRoll;
    [SerializeField] Toggle tglPitch;
    [SerializeField] Toggle tglYaw;
    [SerializeField] InputField ifAngle;
    [Space]
    [Header("Option: Scale")]
    [SerializeField] GameObject objPanelScale;
    [SerializeField] InputField[] ifScale;
    [SerializeField] Button btnScaleApply;
    [SerializeField] Button btnScaleReset;
    [SerializeField] Button btnScaleRandom;
    [SerializeField] Toggle tglUniformScale;
    [Space]
    [SerializeField] Transform trsCube;
    [SerializeField] Text txtCubePosition;
    [SerializeField] Text txtCubeRotation;
    [SerializeField] Text txtCubeScale;
    [SerializeField] Button btnResetCube;

    void Start ()
    {
        ddTransformation.onValueChanged.AddListener(delegate
        {
            switch (ddTransformation.value)
            {
                case 0: // Translate
                    objPanelTranslate.SetActive(true);
                    objPanelRotate.SetActive(false);
                    objPanelScale.SetActive(false);
                    break;
                case 1: // Rotation
                    objPanelTranslate.SetActive(false);
                    objPanelRotate.SetActive(true);
                    objPanelScale.SetActive(false);
                    break;
                case 2: // Scale
                    objPanelTranslate.SetActive(false);
                    objPanelRotate.SetActive(false);
                    objPanelScale.SetActive(true);
                    break;
            }
        });

        // button settings for Translate
        btnTranslateApply.onClick.AddListener(delegate
        {
            VaildateMatrix(ifTranslate, ifTranslate.Length);
            trsCube.Translate(
                float.Parse(ifTranslate[3].text), // r14
                float.Parse(ifTranslate[7].text), // r24
                float.Parse(ifTranslate[11].text) // r34
            );
        });
        btnTranslateReset.onClick.AddListener(delegate
        {
            ResetMatrix(ifTranslate, ifTranslate.Length);
        });
        btnTranslateRandom.onClick.AddListener(delegate
        {
            RandomMatrix(ifTranslate, ifTranslate.Length);
        });

        // button settings for Rotation
        ifAngle.onEndEdit.AddListener(delegate
        {
            ApplyAngle();
        });
        btnRotateApply.onClick.AddListener(delegate
        {
            VaildateMatrix(ifRotate, ifRotate.Length);

            Matrix4x4 mat = new Matrix4x4(
                new Vector4(float.Parse(ifRotate[0].text), float.Parse(ifRotate[1].text), float.Parse(ifRotate[2].text), float.Parse(ifRotate[3].text)),
                new Vector4(float.Parse(ifRotate[4].text), float.Parse(ifRotate[5].text), float.Parse(ifRotate[6].text), float.Parse(ifRotate[7].text)),
                new Vector4(float.Parse(ifRotate[8].text), float.Parse(ifRotate[9].text), float.Parse(ifRotate[10].text), float.Parse(ifRotate[11].text)),
                new Vector4(float.Parse(ifRotate[12].text), float.Parse(ifRotate[13].text), float.Parse(ifRotate[14].text), float.Parse(ifRotate[15].text))
            );

            Quaternion q = new Quaternion();
            q.w = Mathf.Sqrt(1.0f + mat.m00 + mat.m11 + mat.m22) / 2.0f;
            float w4 = (4.0f * q.w);
            q.x = (mat.m21 - mat.m12) / w4;
            q.y = (mat.m02 - mat.m20) / w4;
            q.z = (mat.m10 - mat.m01) / w4;

            trsCube.Rotate(q.eulerAngles);
        });
        btnRotateReset.onClick.AddListener(delegate
        {
            ResetMatrix(ifRotate, ifRotate.Length);
            ifAngle.text = "0.0";
        });
        btnRotateRandom.onClick.AddListener(delegate
        {
            ifAngle.text = Random.Range(-360.0f, 360.0f).ToString("0.00");
            ApplyAngle();
        });

        // button settings for Scale
        btnScaleApply.onClick.AddListener(delegate
        {
            VaildateMatrix(ifScale, ifScale.Length);

            if (tglUniformScale.isOn)
            {
                ifScale[5].readOnly = ifScale[10].readOnly = true;
                ifScale[10].text = ifScale[5].text = ifScale[0].text;
            }
            else
            {
                ifScale[5].readOnly = ifScale[10].readOnly = false;
            }

            trsCube.localScale = new Vector3(
                float.Parse(ifScale[0].text),
                float.Parse(ifScale[5].text),
                float.Parse(ifScale[10].text)
            );
        });
        btnScaleReset.onClick.AddListener(delegate
        {
            ResetMatrix(ifScale, ifScale.Length);
        });
        btnScaleRandom.onClick.AddListener(delegate
        {
            if (tglUniformScale.isOn)
            {
                float fScale = Random.Range(0.01f, 10.0f);
                ifScale[0].text = ifScale[5].text = ifScale[10].text = fScale.ToString("0.00");
            }
            else
            {
                RandomMatrix(ifScale, ifScale.Length);
            }
        });

        btnResetCube.onClick.AddListener(delegate
        {
            trsCube.position = Vector3.zero;
            trsCube.rotation = Quaternion.identity;
            trsCube.localScale = Vector3.one;
        });
    }
	
	void Update ()
    {
        txtCubePosition.text = "Position: (" + trsCube.position.x.ToString("0.0000") + ", " + 
            trsCube.position.y.ToString("0.0000") + ", " + 
            trsCube.position.z.ToString("0.0000") + ")";
        txtCubeRotation.text = "Rotation: (" + trsCube.eulerAngles.x.ToString("0.0000") + ", " +
            trsCube.eulerAngles.y.ToString("0.0000") + ", " +
            trsCube.eulerAngles.z.ToString("0.0000") + ") in degrees";
        txtCubeScale.text = "Scale: (" + trsCube.localScale.x.ToString("0.0000") + ", " +
            trsCube.localScale.y.ToString("0.0000") + ", " +
            trsCube.localScale.z.ToString("0.0000") + ")";
}

    void ResetMatrix(InputField[] ifs, int nLength)
    {
        for (int i = 0; i < nLength; i++)
        {
            if (i % 5 == 0)
                ifs[i].text = "1.0";
            else
                ifs[i].text = "0.0";
        }
    }

    void VaildateMatrix(InputField[] ifs, int nLength)
    {
        float fTemp = 0.0f;

        for (int i = 0; i < nLength; i++)
        {
            if (!float.TryParse(ifs[i].text, out fTemp))
            {
                Debug.LogError("Check the value at Row: " + ((i / 4) + 1) + ", Col: " + ((i % 4) + 1) + "! we assumed as 0!");
                ifs[i].text = "0.0";
            }
        }
    }

    void RandomMatrix(InputField[] ifs, int nLength)
    {
        for (int i = 0; i < nLength; i++)
        {
            if (!ifs[i].readOnly)
                ifs[i].text = Random.Range(-5.0f, 5.0f).ToString("0.00");
        }
    }

    void ApplyAngle()
    {
        float fAngle = 0.0f;

        if (!float.TryParse(ifAngle.text, out fAngle))
        {
            Debug.LogError("Error on Angle[in Degree]! Check the value and we assumed as 0!");
            return;
        }

        if (tglRoll.isOn) // Roll
        {
            ifRotate[0].text = Mathf.Cos(fAngle * Mathf.Deg2Rad).ToString("0.00");
            ifRotate[1].text = (-Mathf.Sin(fAngle * Mathf.Deg2Rad)).ToString("0.00");
            ifRotate[4].text = Mathf.Sin(fAngle * Mathf.Deg2Rad).ToString("0.00");
            ifRotate[5].text = Mathf.Cos(fAngle * Mathf.Deg2Rad).ToString("0.00");
            ifRotate[2].text = ifRotate[6].text = ifRotate[8].text = ifRotate[9].text = "0.0";
            ifRotate[10].text = "1.0";
        }
        else if (tglPitch.isOn) // Pitch
        {
            ifRotate[5].text = Mathf.Cos(fAngle * Mathf.Deg2Rad).ToString("0.00");
            ifRotate[6].text = (-Mathf.Sin(fAngle * Mathf.Deg2Rad)).ToString("0.00");
            ifRotate[9].text = Mathf.Sin(fAngle * Mathf.Deg2Rad).ToString("0.00");
            ifRotate[10].text = Mathf.Cos(fAngle * Mathf.Deg2Rad).ToString("0.00");
            ifRotate[1].text = ifRotate[2].text = ifRotate[4].text = ifRotate[8].text = "0.0";
            ifRotate[0].text = "1.0";
        }
        else if (tglYaw.isOn) // Yaw
        {
            ifRotate[0].text = Mathf.Cos(fAngle * Mathf.Deg2Rad).ToString("0.00");
            ifRotate[2].text = Mathf.Sin(fAngle * Mathf.Deg2Rad).ToString("0.00");
            ifRotate[8].text = (-Mathf.Sin(fAngle * Mathf.Deg2Rad)).ToString("0.00");
            ifRotate[10].text = Mathf.Cos(fAngle * Mathf.Deg2Rad).ToString("0.00");
            ifRotate[1].text = ifRotate[4].text = ifRotate[6].text = ifRotate[9].text = "0.0";
            ifRotate[5].text = "1.0";
        }
    }
}
