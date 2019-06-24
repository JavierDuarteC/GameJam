using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigator : MonoBehaviour
{
    [Header("Nombre de la escena siguiente:")]
    public String sceneName;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneName: sceneName);
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(sceneName: sceneName);
    }

    public void Close()
    {
        Application.Quit();
    }
}