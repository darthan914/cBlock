using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventButton : MonoBehaviour {

    public Button dynamicStartButton;

    public void Awake()
    {
        dynamicStartButton.GetComponentInChildren<Text>().text = "Start";
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void StartPause()
    {
        if(Time.timeScale > 0f)
        {
            Time.timeScale = 0f;
            dynamicStartButton.GetComponentInChildren<Text>().text = "Resume";
            GameObject.FindObjectOfType<MainController>().messageText = "Press Resume to Continue";
        }
        else
        {
            Time.timeScale = 1f;
            dynamicStartButton.GetComponentInChildren<Text>().text = "Pause";
        }
    }
}
