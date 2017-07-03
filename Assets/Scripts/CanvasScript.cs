using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour {

     Canvas canvas;
    private bool showMenu = true;
	
	void Start () {
        canvas = GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = showMenu;
            showMenu = !showMenu;
        }

	}

    public void ReturnFromMenu()
    {
        canvas.enabled = false;
        showMenu = !showMenu;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
