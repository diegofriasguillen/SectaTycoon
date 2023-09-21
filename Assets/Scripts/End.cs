using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject exitButton;
    public GameObject soundEnd;

    private void Start()
    {
        soundEnd.SetActive(true);
    }
    public void Restart()
    {   
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
