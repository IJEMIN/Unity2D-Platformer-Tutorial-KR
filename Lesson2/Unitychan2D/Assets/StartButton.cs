using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

    public void GameStart(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}