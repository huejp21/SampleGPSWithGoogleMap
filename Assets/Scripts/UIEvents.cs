using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEvents : MonoBehaviour
{

    public GameObject googleMap;
    public GameObject GPS;

    public RawImage mapUser;
    public Text txt_zoom_user;
    public Text txt_lat_user;
    public Text txt_lon_user;
    public InputField ipf_lat_user;
    public InputField ipf_lon_user;
    public Slider sd_zoom_user;

    public RawImage mapCurrent;
    public Text txt_zoom_current;
    public Text txt_lat_current;
    public Text txt_lon_current;
    public Slider sd_zoom_current;

    bool refeshed = false;
    float userLat = 0.0f;
    float userLon = 0.0f;
    int userZoom = 0;
    float curLat = float.MaxValue;
    float curLon = float.MaxValue;
    int curZoom = 0;

    // Use this for initialization
    void Start()
    {
        userZoom = (int)sd_zoom_user.value;
        curZoom = (int)sd_zoom_current.value;
    }

    // Update is called once per frame
    void Update()
    {
        string strLat = "Current Latitude: ";
        string strLon = "Current Longitude: ";
        if (GPS.GetComponent<GPSControl>().isOnGPS)
        {
            float lat = GPS.GetComponent<GPSControl>().positionGPS.latitude;
            float lon = GPS.GetComponent<GPSControl>().positionGPS.longitude;

            strLat += lat.ToString("0.0000");
            strLon += lon.ToString("0.0000");
            if (curLat != lat || curLon != lon)
            {
                curLat = lat;
                curLon = lon;
                if (googleMap.GetComponent<GoogleMapAPI>().RefreshMap(mapCurrent, lat, lon, (int)sd_zoom_current.value, 1, GoogleMapAPI.mapType.roadmap))
                {
                    curZoom = (int)sd_zoom_current.value;
                }
            }
        }
        else
        {
            strLat = "Unable GPS";
            strLon = "Unable GPS";
        }
        txt_lat_current.text = strLat;
        txt_lon_current.text = strLon;

        txt_lat_user.text = "User Latitude: " + userLat.ToString("0.0000");
        txt_lon_user.text = "User Longitude: " + userLon.ToString("0.0000");

        txt_zoom_user.text = "Zoom: " + ((int)sd_zoom_user.value).ToString();
        txt_zoom_current.text = "Zoom: " + ((int)sd_zoom_current.value).ToString();
    }

    #region UI Events
    public void Click_Search()
    {
        float lat = 0.0f;
        float lon = 0.0f;
        if (float.TryParse(ipf_lat_user.text, out lat) == false
         || float.TryParse(ipf_lon_user.text, out lon) == false)
        {
            return;
        }
        if (googleMap.GetComponent<GoogleMapAPI>().RefreshMap(mapUser, lat, lon, (int)sd_zoom_user.value, 1, GoogleMapAPI.mapType.roadmap))
        {
            refeshed = true;
            userLat = lat;
            userLon = lon;
            userZoom = (int)sd_zoom_user.value;
            //userScale = (int)sd_scale_user.value;
        }
    }

    public void ChangedValue_Zoom_User()
    {
        userZoom = (int)sd_zoom_user.value;
        if (refeshed)
        {
            googleMap.GetComponent<GoogleMapAPI>().RefreshMap(mapUser, userLat, userLon, userZoom, 1, GoogleMapAPI.mapType.roadmap);
        }
    }

    public void ChangedValue_Zoom_Current()
    {
        curZoom = (int)sd_zoom_current.value;
        if (GPS.GetComponent<GPSControl>().isOnGPS)
        {
            float lat = GPS.GetComponent<GPSControl>().positionGPS.latitude;
            float lon = GPS.GetComponent<GPSControl>().positionGPS.longitude;
            if (googleMap.GetComponent<GoogleMapAPI>().RefreshMap(mapCurrent, lat, lon, curZoom, 1, GoogleMapAPI.mapType.roadmap))
            {

            }
        }
    }

    public void Click_Quit()
    {
        Application.Quit();
    }
    #endregion
}
