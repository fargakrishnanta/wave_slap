using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour{

    public static GameManager instance = null;
    void Awake()
    {
        
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void StartWaverCutScene()
    {
        SceneManager.LoadScene(3);
    }

    public void StartSlapperCutScene()
    {
        SceneManager.LoadScene(4);
    }
    public void InGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadJellyBean()
    {
        SceneManager.LoadScene(5);
    }

    public void LoadWaveWin()
    {
        SceneManager.LoadScene(6);
    }

    public void SlapperWinScene()
    {
        SceneManager.LoadScene(7);
    }
	// Use this for initialization
	
}
