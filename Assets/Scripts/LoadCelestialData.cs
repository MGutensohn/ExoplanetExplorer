﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Celestial;

public class LoadCelestialData : MonoBehaviour
{
    private Observed celestialData;

    // Start is called before the first frame update
    void Awake()
    {
        Systems.stars = new List<Star>();
        StartCoroutine(GetStars());
    }

    IEnumerator GetStars() {
        UnityWebRequest www = UnityWebRequest.Get("https://exoplanetarchive.ipac.caltech.edu/cgi-bin/nstedAPI/nph-nstedAPI?table=exoplanets&select=pl_hostname,st_rad,st_teff,ra,dec,st_dist&order=pl_disc&format=json");
        yield return www.SendWebRequest();
        if(www.isDone)
        {
            if(www.isNetworkError) 
            {
                Debug.Log("Network Error: " + www.error);
            }

            if(www.isHttpError)
            {
                Debug.Log("HTTP Error: " + www.error);
            }
            else {
                Debug.Log("download size: " + www.downloadHandler.data.Length);

                celestialData = JsonUtility.FromJson<Observed>("{\"rawData\":" + www.downloadHandler.text + "}");
                var rawData = new List<RawData>(celestialData.rawData);
                Debug.Log("Total number of planets: " + rawData.Count);
                
                foreach (RawData r in rawData)
                {
                    
                    Exoplanet ep = new Exoplanet();
                    ep.name = r.pl_name;
                    ep.inclination = r.pl_orbincl;
                    ep.period = r.pl_orbper;
                    ep.eccentricity = r.pl_orbseccen;
                    ep.smAxis = r.pl_orbsmax;

                    if(Systems.stars.FindIndex(star => star.name == r.pl_hostname) < 0)
                    {
                        Star s = new Star();
                        s.name = r.pl_hostname;
                        s.distance = r.st_dist;
                        s.temperature = r.st_teff;

                        if(r.st_rad > 0)
                        {
                            var log = Mathf.Log(r.st_rad, 10);
                            s.size =  log >= 0.5f ? log : 0.5f;
                        }
                        else
                        {
                            s.size = 1;
                        }

                        float x = Mathf.Cos(r.ra) * Mathf.Cos(r.dec);
                        float y = Mathf.Sin(r.ra) * Mathf.Cos(r.dec);
                        float z = Mathf.Sin(r.dec);

                        s.startingPosition = new Vector3(x,y,z);
                        if(s.distance > 0)
                        {
                            s.location = Mathf.Log(s.distance, 10) * s.startingPosition;
                        }
                        else
                        {
                            s.location = s.startingPosition;
                        }
                        s.planets = new List<Exoplanet>();
                        Systems.stars.Add(s);
                    }
                    Systems.stars.Find(star => star.name == r.pl_hostname).planets.Add(ep);
                }
                Debug.Log("Total number of stars: " + Systems.stars.Count);
            }
        }
    }
}
