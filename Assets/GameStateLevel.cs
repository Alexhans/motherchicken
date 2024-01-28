using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateLevel : MonoBehaviour
{
    public static GameStateLevel i;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        ChickenManager.OnSurvive += RestartScene;
    }

        void OnDisable()
    {
        ChickenManager.OnSurvive -= RestartScene;
    }


    void CreateClassSingleton()
    {
        if (i == null)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void RestartScene()
    {
        Debug.Log("You won!");
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

    }
}
