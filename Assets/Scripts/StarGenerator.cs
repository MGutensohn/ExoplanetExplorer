using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Celestial;

public class StarGenerator : MonoBehaviour
{
    public GameObject star_prefab;
    private Observed celestialData;
    private List<Star> stars;
    public bool distance = false;
    public float scale = 1f;

    public void ToggleDistance()
    {
        distance = !distance;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetStars());
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 10; i++)
        {
            if(stars.Count > 0)
            {
                Star s = stars[stars.Count - 1];
                stars.RemoveAt(stars.Count - 1);
                if (!GameObject.Find(s.pl_hostname))
                {
                    var newStar = Instantiate(star_prefab);
                    newStar.GetComponent<StarController>().setStarData(s, scale);
                    newStar.transform.parent = gameObject.transform;
                }
                else
                {
                    Debug.Log(s.pl_hostname + " Exists.");
                }
            }
            else{
                break;
            }
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            ToggleDistance();
        }
    }

    IEnumerator GetStars() {
        UnityWebRequest www = UnityWebRequest.Get("https://exoplanetarchive.ipac.caltech.edu/cgi-bin/nstedAPI/nph-nstedAPI?table=exoplanets&select=pl_hostname,ra,dec,st_dist&order=pl_disc&format=json");
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
                // Show results as text
                Debug.Log("Success: " + www.downloadHandler.text);
                celestialData = JsonUtility.FromJson<Observed>("{\"stars\":" + www.downloadHandler.text + "}");
                stars = new List<Star>(celestialData.stars);
                Debug.Log("Total number of stars: " + stars.Count);
                
                foreach (Star s in stars)
                {
                    float x = Mathf.Cos(s.ra) * Mathf.Cos(s.dec);
                    float y = Mathf.Sin(s.ra) * Mathf.Cos(s.dec);
                    float z = Mathf.Sin(s.dec);
                    s.startingPosition = new Vector3(x,y,z);
                    if(s.st_dist > 0)
                    {
                        s.location = Mathf.Log(s.st_dist) * s.startingPosition;
                    }
                    else
                    {
                        s.location = Mathf.Log(Mathf.Epsilon) * s.startingPosition;
                    }
                }

            }
        }
    }
}
