using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Celestial;

public class StarGenerator : MonoBehaviour
{
    public GameObject starPrefab;
    public Button toggleModeButton;
    private List<Star> stars;
    private bool mode = false;
    private bool ToggleMode
    {
        get {return mode;}
        set {
            if (mode == value) return;
            mode = value;
            
            OnModeChange(mode);
        }
    }

    public void ToggleModeOnClick()
    {
        ToggleMode = !ToggleMode;
    }

    public delegate void OnVariableChangeDelegate(bool newMode);
    public event OnVariableChangeDelegate OnModeChange;
    public float scale = 0.2f;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
        if(GameObject.Find("Toggle Mode") != null)
        {
            toggleModeButton = GameObject.Find("Toggle Mode").GetComponent<Button>();
            toggleModeButton.onClick.AddListener(delegate {ToggleModeOnClick();});
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (stars != null)
        {
            for(int i = 0; i < 10; i++)
            {
                if(stars.Count > 0)
                {
                    Star s = stars[stars.Count - 1];
                    stars.RemoveAt(stars.Count - 1);
                    var newStar = Instantiate(starPrefab);
                    newStar.GetComponent<StarController>().setStarData(s, scale);
                    newStar.transform.SetParent(gameObject.transform, false);
                }
                else{
                    break;
                }
            }
            if(Input.GetKeyDown(KeyCode.C))
            {
                ToggleMode = true;
            }
        }
        else
        {
            stars = Systems.stars;
        }
    }
}
