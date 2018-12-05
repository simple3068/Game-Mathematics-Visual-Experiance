using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ch03BB_GameController : MonoBehaviour
{
    [Header("Game Options")]
    [SerializeField] GameObject objBox;
    [Space]
    [SerializeField] float fMinSpawnInterval = 0.5f;
    [SerializeField] float fMaxSpawnInterval = 2.0f;
    [Space]
    [Header("Settings for UI")]
    [SerializeField] Text txtStage;
    [SerializeField] Text txtNumOfBoxes;

    int nCurrentStage = 1;

	IEnumerator Start ()
    {
		while (true)
        {
            for (int i = 0; i < nCurrentStage * nCurrentStage; i++)
            {
                Instantiate(objBox);

                yield return new WaitForSeconds(Random.Range(fMinSpawnInterval, fMaxSpawnInterval));
            }

            yield return new WaitForSeconds(3.0f);
            nCurrentStage++;
        }
	}
	
	void Update ()
    {
        txtStage.text = "Stage: " + nCurrentStage;
        txtNumOfBoxes.text = "# of Boxes: " + (nCurrentStage * nCurrentStage);
    }
}
