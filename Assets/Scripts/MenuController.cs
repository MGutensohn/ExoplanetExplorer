using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menu;

    private Animator animator;

    public void OpenMenu()
    {
        animator = menu.GetComponent<Animator>();

        animator.SetBool("Open", true);
    }

    public void CloseMenu()
    {
        animator = menu.GetComponent<Animator>();

        animator.SetBool("Open", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
