using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    // all over buttons, load scenes
	public void Btn(string gameLevel)
    {
        SceneManager.LoadScene(gameLevel);
    }

    // quit button
    public void QuitBtn()
    {
        Application.Quit();
    }

    // for control menu, game over menu, and win menu
    public void BackBtn(string gameLevel)
    {
        SceneManager.LoadScene(gameLevel);
    }
}
