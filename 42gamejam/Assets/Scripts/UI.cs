using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
	int viewPauseMenu = 0;    
	private GameObject pausePanel;	


	// Start is called before the first frame update
    void Start()
    {
		pausePanel = GameObject.FindGameObjectWithTag("pausePanel");
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			showPauseMenu();
		}	
	
		if (viewPauseMenu == 1)
		{
			pausePanel.SetActive(true);
		}		
		else if (viewPauseMenu == 0)
		{
			pausePanel.SetActive(false);
		}
    }

	public void showPauseMenu()
	{
		viewPauseMenu = 1;
	}

	public void hidePauseMenu()
	{
		viewPauseMenu = 0;
	}

	public void loadMainMenu()
	{
		SceneManager.LoadScene("mainMenu");
	}
	
	public void restartGame()
	{
		SceneManager.LoadScene("SampleScene");
	}
	
	public void quitGame()
	{
		Application.Quit();
	}
}
