using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;
using System.IO;

public class CameraMove : MonoBehaviour
{
    // the input file for reading
    public TextAsset inputFile;

    // The list of all of the ship positions on the voyage.
    List<ShipPosition> theDays = new List<ShipPosition>();

    // the framecounter for movement
    private int frameCounter;

    // The total number of timesteps
    private int numFrames;


    // Struct for holding a particular ship position in xyz, with the timestamp
    struct ShipPosition
    {
        public double x, y, z;
        public string date;         // do something with this later probably

        // constructor
        public ShipPosition(double inx, double iny, double inz, string indate)
        {
            this.x = inx;
            this.y = iny;
            this.z = inz;
            this.date = indate;
        }

        // constructor for strings
        public ShipPosition(string inx, string iny, string inz, string indate)
        {
            this.x = System.Convert.ToDouble(inx);
            this.y = System.Convert.ToDouble(iny);
            this.z = System.Convert.ToDouble(inz);
            this.date = indate;
        }
    }



   /*
    * function for reading in the input file.
    * Splits up the input file into ShipPosition structs,
    * and returns the amount of ShipPositions read in.
    */
    private int loadTextFile(TextAsset theFile)
    {
        // split  up the file into lines
        string[] allLines = theFile.text.Split('\n');

        foreach (string currentLine in allLines)
        {
            if (currentLine != null)
            {
                string[] lineArray = currentLine.Split(';');            // split on semicolons
                for (int i = 0; i < lineArray.Length; i++)
                    lineArray[i] = lineArray[i].Trim();                 // trim whitespace

                // create a new ShipPosition object and add it to the list
                theDays.Add(new ShipPosition(
                    lineArray[1],
                    lineArray[2],
                    lineArray[3],
                    lineArray[0])
                    );
            }

            else
            {
                Debug.Log("Error in input file line");
            }
        }

        // Done reading the file.
        return theDays.Count;
    }


    // Use this for initialization
    void Start()
    {
        int numLoaded = loadTextFile(inputFile);
        if (numLoaded >= 0)
        {
            Debug.Log("Position file read successfully! Lines read: " + numLoaded);
            numFrames = numLoaded;
        }

        /*foreach (ShipPosition currentPos in theDays)
        {
            Instantiate(Sphe)
        }*/

        frameCounter = 0;
    }


    // Update is called once per frame
    void Update ()
    {
		
	}


    // FixedUpdate is called once every physics step.
    // Therefore, the intervals are consistent
    void FixedUpdate ()
    {
        // restart if we're at the end of the loop
        if (frameCounter > numFrames-1)
            frameCounter = 0;

        // divide by 1,000,000 for scaling
        double currentx = theDays[frameCounter].x / 10000000;
        double currenty = theDays[frameCounter].y / 10000000;
        double currentz = theDays[frameCounter].z / 10000000;

        double nextx = theDays[frameCounter+1].x / 10000000;
        double nexty = theDays[frameCounter+1].y / 10000000;
        double nextz = theDays[frameCounter+1].z / 10000000;

        float x = (float)currentx;
        float y = (float)currenty;
        float z = (float)currentz;

        float newx = (float)nextx;
        float newy = (float)nexty;
        float newz = (float)nextz;

        transform.position = new Vector3(x,y,z);
        //transform.LookAt(new Vector3(newx, newy, newz), Vector3.zero);
        transform.LookAt(Vector3.zero, Vector3.zero);

        frameCounter++;
    }
}
