using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CountdownTimer : MonoBehaviour {

    [SerializeField]
    public float MaxTime;

    public Sprite S1;
    public Sprite S2;
    public Sprite S3;
    public Sprite S4;
    public Sprite S5;
    public Sprite S6;
    public Sprite S7;
    public Sprite S8;
    public Sprite S9;
    public Sprite S0;


    public Image MOne;
    public Image MTwo;
    public Image SOne;
    public Image STwo;

    private bool isPaused = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (!isPaused)
        {
            MaxTime -= Time.deltaTime;
            if (MaxTime <= 0)
            {
                MaxTime = 0;
                GameOver();
            }
        }
        PrintTime();
	}

    void PrintTime()
    {
        string minutes = Mathf.Floor(MaxTime / 60).ToString("00");
        string seconds = Mathf.Floor(MaxTime % 60).ToString("00");

        string whatever = minutes + ":" + seconds;

        int index = 0;
        foreach(var letter in minutes)
        {
            if (index == 2)
                index = 0;
            if (index == 0)
            {
                MOne.sprite = SpriteSelection(letter.ToString());
            }
            else if(index == 1)
            {
                MTwo.sprite = SpriteSelection(letter.ToString());
            }
            index++;
        }
        index = 0;
        foreach (var letter in seconds)
        {
            if (index == 2)
                index = 0;
            if (index == 0)
            {
                SOne.sprite = SpriteSelection(letter.ToString());
            }
            else if (index == 1)
            {
                STwo.sprite = SpriteSelection(letter.ToString());
            }
            index++;
        }

       
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Resume()
    {
        isPaused = false;
    }
    Sprite SpriteSelection(string a)
    {
        switch (a)
        {
            case "1":
                return S1; 
            case "2":
                return S2;
            case "3":
                return S3;
            case "4":
                return S4;
            case "5":
                return S5;
            case "6":
                return S6;
            case "7":
                return S7;
            case "8":
                return S8;
            case "9":
                return S9;
            case "0":
                return S0;
            default:
                return S0;
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene(2);
        //Debug.Log("GAMEOVER");
    }
}
