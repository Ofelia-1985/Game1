using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private Canvas canvas;
    public GameObject GameMenu;
    public bool isPaused;
    private GameObject ObjGear;

    
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        GameMenu = canvas.transform.GetChild(0).gameObject; 
        ObjGear = canvas.transform.GetChild(1).gameObject;  
        GameMenu.SetActive(false);
        isPaused = false; 
    }

    void Update()
    {
        if(Input.GetKeyDown("p") &&!isPaused)
        {
            GameMenu.SetActive(true);
            ObjGear.SetActive(false);
            isPaused = true;
        }
        else if (Input.GetKeyDown("p")&&isPaused)
        {
            GameMenu.SetActive(false);
            ObjGear.SetActive(true);
            isPaused = false;
        }

         if(isPaused)
        {
            Time.timeScale = 0;
            Debug.Log(Time.timeScale);
        }
        else
        {
            Time.timeScale = 1;
        }
    }

     public void Gear()
    {
        ObjGear.SetActive(false);
        GameMenu.SetActive(true);
        isPaused= true;
    }

     public void Resume()
    {
        ObjGear.SetActive(true);
        GameMenu.SetActive(false);
        isPaused= false;
    }

    public void Exit()
    {
        Application.Quit();

    }
}