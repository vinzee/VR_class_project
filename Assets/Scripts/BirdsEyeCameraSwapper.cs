using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsEyeCameraSwapper : MonoBehaviour
{
    public Camera defaultCamera;                    // The default, ship view camera
    public Camera[] birdCameraList;                 // The list of cameras to switch between
    //public GameObject viewCornerLines;              // The viewing rays representing FoV of the ship
    private int currentCameraIndex;                 // The current camera in use

	// Use this for initialization
	void Start ()
    {
        // Initialize the camera index
        currentCameraIndex = 0;

        // Turn off all bird's eye cameras, because we start with the default cam
        for (int i = 0; i < birdCameraList.Length; i++)
        {
            birdCameraList[i].gameObject.SetActive(false);
        }

        // Turn off the viewing lines
        //viewCornerLines.SetActive(false);

        // Turn on the default cam
        defaultCamera.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update ()
    {
		// If the C button (temporary) is pressed, switch to the next bird's eye camera
        if (Input.GetKeyDown(KeyCode.C))
        {
            // turn off the default camera, in case it was on
            defaultCamera.gameObject.SetActive(false);

            // turn on the viewing lines
            //viewCornerLines.SetActive(true);

            // turn off the current bird camera
            birdCameraList[currentCameraIndex].gameObject.SetActive(false);

            //update the bird camera index
            if (currentCameraIndex >= birdCameraList.Length - 1)
                currentCameraIndex = 0;
            else
                currentCameraIndex++;

            // Turn on the next camera
            birdCameraList[currentCameraIndex].gameObject.SetActive(true);
        }


        // If the D key (temporary) is pressed, switch back to the default camera
        if (Input.GetKeyDown(KeyCode.D))
        {
            // turn off all bird's eye cameras
            for (int i = 0; i < birdCameraList.Length; i++)
            {
                birdCameraList[i].gameObject.SetActive(false);
            }

            // Turn off the viewing lines
            //viewCornerLines.SetActive(false);

            // Turn on the default camera
            defaultCamera.gameObject.SetActive(true);
        }
	}
}
