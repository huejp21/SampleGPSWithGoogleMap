using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleMapAPI : MonoBehaviour
{

    public enum mapType
    {
        roadmap,
        satellite,
        hybrid,
        terrain
    }

    string apiKey = "UserKey";
    public bool RefreshMap(RawImage img, float lat, float lon, int zoom, int scale, mapType style)
    {
        try
        {
            int mapWidth = (int)img.GetComponent<RectTransform>().rect.width;
            int mapHeight = (int)img.GetComponent<RectTransform>().rect.height;
            string url =
             "https://maps.googleapis.com/maps/api/staticmap?" +
             "center=" + lat + "," + lon +
             "&zoom=" + zoom +
             "&size=" + mapWidth + "x" + mapHeight +
             "&scale=" + scale +
             "&maptype=" + style +
             "&markers=color:blue%7Clabel:S%7C" + lat + "," + lon +
             "&key=" + apiKey;

            //"https://maps.googleapis.com/maps/api/staticmap?center=\" + lat + \",\" + lon +\n" +
            //  " \"&zoom=\" + zoom + \"&size=\" + mapWidth + \"x\" + mapHeight + \"&scale=\" + scale \n" +
            //  " + \"maptype=\" + mapSelected +\n \"&markers=color:blue%7Clabel:S%7C40.702147,-74.015794&markers=color:green%7Clabel:G7C40.711614,-74.012318&markers=color:red%77C40.718217,-73.998284&key= + apiKey + \"";
            WWW www = new WWW(url);
            int delay = 1000;
            int timer = 0;
            bool done = false;

            while (delay > timer)
            {
                System.Threading.Thread.Sleep(1);
                timer++;
                if (www.isDone)
                {
                    done = true;
                    break;
                }
            }

            if (done == false)
            {
                return false;
            }
            if (img != null)
            {
                img.texture = www.texture;
                img.SetNativeSize();
            }
        }
        catch (System.Exception)
        {

            return false;
        }
        return true;
    }

    //   // Use this for initialization
    //   void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
}
