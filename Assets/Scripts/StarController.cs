using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Celestial;

public class StarController : MonoBehaviour
{
    public float speed = 1.0f;
    private Star info;
    private bool move = false;
    private float scale = 1f;

    public void setStarData(Star s, float scale)
    {
        this.scale = scale;
        info = s;
        gameObject.name = info.name;
        transform.position = info.startingPosition * scale;
        transform.localScale *= info.size;

        Color starCol = Mathf.CorrelatedColorTemperatureToRGB(s.temperature);
        gameObject.GetComponent<SpriteRenderer>().color = starCol;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        if(transform.parent.gameObject.GetComponent<StarGenerator>().distance)
        {
            transform.position = info.location * scale;
        }
        else
        {
            transform.position = info.startingPosition * scale;
        }
    }
}
