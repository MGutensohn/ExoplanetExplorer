using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindPlanet : MonoBehaviour
{
    public InputField searchBar;
    public Button searchButton;
    private GameObject closest = null;
    // Start is called before the first frame update
    void Start()
    {
        searchBar = GameObject.Find("Search Bar").GetComponent<InputField>();
        searchButton = GameObject.Find("Search Button").GetComponent<Button>();
        searchButton.onClick.AddListener(delegate {FindStarByName();});
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindStarByName()
    {
        GameObject destination = GameObject.Find(searchBar.text);
        if(destination != null)
        {
            DrawPath(destination);
        }
    }

    public void DrawPath(GameObject destination)
    {
        if(closest != null)
            Destroy(closest.GetComponent<LineRenderer>());
        closest = null;

        float distance = Mathf.Infinity;
        Vector3 position = Camera.main.transform.position;

        Transform stars = GameObject.Find("starGenerator").transform;
        foreach (Transform t in stars)
        {
            Vector3 diff = t.position - position;
            float curDistance = diff.sqrMagnitude;
            if(Vector3.Dot(transform.forward, diff) < 0)
                continue;

            if (curDistance < distance)
            {
                closest = t.gameObject;
                distance = curDistance;
            }
        }

        closest.AddComponent<LineRenderer>();
        LineRenderer lr = closest.GetComponent<LineRenderer>();

        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lr.startColor = Color.red;
        lr.endColor = Color.red;
        lr.endWidth = 0.003f;
        lr.startWidth = 0.003f;

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, (destination.transform.position - closest.transform.position).normalized, Vector3.Distance(closest.transform.position, destination.transform.position));
        
        Vector3[] lrPos = new Vector3[hits.Length + 1];
        lrPos[0] = closest.transform.position;
        lr.positionCount = hits.Length + 1;
        System.Array.Sort(hits, (x,y) => x.distance.CompareTo(y.distance));


        for (int i = 1; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            lrPos[i] =  hit.transform.position;
        }
        lrPos[hits.Length] = destination.transform.position;
        lr.SetPositions(lrPos);
    }
}
