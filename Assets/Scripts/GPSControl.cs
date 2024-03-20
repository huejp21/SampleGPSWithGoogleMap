using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSControl : MonoBehaviour {

    public bool isOnGPS { get; private set; }

    public LocationInfo positionGPS { get; private set; }
    public float latitude { get; private set; }
    public float longitude { get; private set; }
    //int gps_connect = 0;

    void RetrieveGPSData()
    {
        positionGPS = Input.location.lastData; // Get GPS
        latitude = positionGPS.latitude;
        longitude = positionGPS.longitude;
        //gps_connect++;
    }

    // Use this for initialization
    void Start () {
        isOnGPS = false;
        Input.location.Start(0.5f);
        int wait = 1000; // Waiting Delay
        // Checks if the GPS is enabled by the user (-> Allow location ) 
        if (Input.location.isEnabledByUser)// GPS Enable
        {
            while (Input.location.status == LocationServiceStatus.Initializing && wait > 0) // Initial
            {
                wait--;
            }

            if (Input.location.status != LocationServiceStatus.Failed)// GPS On
            {
                isOnGPS = true;
                // We start the timer to check each tick (every 3 sec) the current gps position
                InvokeRepeating("RetrieveGPSData", 0.0001f, 1.0f);
            }
        }
        else // No GPS or GPS unable
        {
            isOnGPS = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
