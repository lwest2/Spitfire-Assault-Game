using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public void OnClick(int index)
    {
        SceneManager.LoadScene(index);
    }


    // quit button
    public void QuitBtn()
    {
        Application.Quit();
    }
    
}
