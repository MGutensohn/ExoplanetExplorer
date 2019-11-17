using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindPlanet : MonoBehaviour
{
    public InputField searchBar;
    private GameObject closest = null;
    // Start is called before the first frame update
    void Start()
    {
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
        lr.endWidth = 0.002f;
        lr.startWidth = 0.002f;
        lr.SetPosition(0, closest.transform.position);
        lr.SetPosition(1, destination.transform.position);
    }
}
