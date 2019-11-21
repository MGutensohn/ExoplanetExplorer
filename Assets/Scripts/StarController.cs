using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Celestial;

public class StarController : MonoBehaviour
{
    public float speed = 1.0f;
    private Star info;
    private float scale = 1f;
    public void setStarData(Star s, float scale)
    {
        this.scale = scale;
        info = s;
        gameObject.name = info.name;
        transform.position = info.startingPosition * scale;
        
        //transform.localScale *= info.size;

        // Color starCol = Mathf.CorrelatedColorTemperatureToRGB(s.temperature);
        // gameObject.GetComponent<SpriteRenderer>().color = starCol;
        
    }

    private void ModeChangeHandler(bool newMode)
    {
        if(newMode)
        {
            transform.localPosition = info.location * scale;
        }
        else
        {
            transform.localPosition = info.startingPosition * scale;
        }
    }
    void Start()
    {
        transform.parent.gameObject.GetComponent<StarGenerator>().OnModeChange += ModeChangeHandler;
    }

    void OnDisable()
    {
        transform.parent.gameObject.GetComponent<StarGenerator>().OnModeChange -= ModeChangeHandler;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
