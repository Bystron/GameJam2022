using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
	private GameObject pausePanel;	


	// Start is called before the first frame update
    void Start()
    {
		pausePanel = GameObject.FindGameObjectWithTag("pausePanel");
		pausePanel.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			showPauseMenu();
		}	
	}

	public void showPauseMenu()
	{
		pausePanel.SetActive(true);	
	}

	public void hidePauseMenu()
	{
		pausePanel.SetActive(false);
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
