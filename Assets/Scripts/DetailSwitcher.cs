using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailSwitcher : MonoBehaviour
{

    public GameObject lowDetail, mediumDetail, highDetail;

    public GameObject viveController;

    private ViveControllerInput scriptFromController;

    private int currentDetail;

	// Use this for initialization
	void Start ()
    {
        lowDetail.SetActive(true);
        mediumDetail.SetActive(false);
        highDetail.SetActive(false);

        scriptFromController = viveController.GetComponent<ViveControllerInput>();
        currentDetail = scriptFromController.detailLevel;
	}

    void swapToLow()
    {
        mediumDetail.SetActive(false);
        highDetail.SetActive(false);
        lowDetail.SetActive(true);
    }

    void swapToMed()
    {
        highDetail.SetActive(false);
        lowDetail.SetActive(false);
        mediumDetail.SetActive(true);
    }

    void swapToHigh()
    {
        lowDetail.SetActive(false);
        mediumDetail.SetActive(false);
        highDetail.SetActive(true);
    }

    void swapToNext()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		if (currentDetail != scriptFromController.detailLevel)
        {
            if (scriptFromController.detailLevel == 0)
            {
                swapToLow();
                currentDetail = 0;
            }

            else if (scriptFromController.detailLevel == 1)
            {
                swapToMed();
                currentDetail = 1;
            }

            else if (scriptFromController.detailLevel == 2)
            {
                swapToHigh();
                currentDetail = 2;
            }

            else
                Debug.Log("Something went wrong when switching detail levels.");
        }
	}
}
