using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveControllerInput : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;

    public int detailLevel = 0;

    private SteamVR_Controller.Device viveController
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }


    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update ()
    {
        // Finger touchpad
        if (viveController.GetAxis() != Vector2.zero)
        {
            Debug.Log(gameObject.name + viveController.GetAxis());
        }

        // Trigger pressed
        if (viveController.GetHairTriggerDown())
        {
            Debug.Log(gameObject.name + " Trigger Press");
        }

        // Trigger released
        if (viveController.GetHairTriggerUp())
        {
            Debug.Log(gameObject.name + " Trigger Release");
        }

        // Grip button pressed
        if (viveController.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Press");
            if (detailLevel == 0)
                detailLevel = 1;
            else if (detailLevel == 1)
                detailLevel = 2;
            else if (detailLevel == 2)
                detailLevel = 0;
        }

        // Grip button released
        if (viveController.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Release");
        }
    }
}
