using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public GameObject menuCanvas;
    public GameObject gameCanvas;

    ChangeColor painter;
    public Spawner spawner;

    public GameObject gameOverPanel;
    public GameObject scorePanel;
    public GameObject nextGroupPanel;

    Text score;
    Text level;
    Scores scoreBoard;
    AudioSource melody;

    [SerializeField] Slider redSlider;
    [SerializeField] Slider greenSlider;
    [SerializeField] Slider blueSlider;

    public static GameObject nextGroup;
    // Use this for initialization
    void Start()
    {
        painter = GetComponent<ChangeColor>();
        melody = GetComponent<AudioSource>();
        scoreBoard = GetComponent<Scores>();
        Pause();
        SetColors();
    }
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();

        if(score)
        {
            score.text = DataBase.score.ToString();
            level.text = DataBase.level.ToString();
        }
        
	}

    public void Restart()
    {
        gameOverPanel.SetActive(false);
        scorePanel.SetActive(true);
        painter.Colorize();
        for (int i = 0; i < Grid.h; i++)
        {
            Grid.deleteRow(i);
        }

        DataBase.score = 0;
        DataBase.level = 1;
        DataBase.scoreToNextLvl = 0;

        GameObject[] groups = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < groups.Length; i++)
        {
            Destroy(groups[i].gameObject);
        }
        DataBase.gameOver = false;
        melody.mute = false;
        spawner.SpawnNext();
        spawner.MoveToGame();

    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverPanel.GetComponentInChildren<Text>().text = DataBase.score.ToString();
        scorePanel.SetActive(false);
        scorePanel.GetComponentInChildren<Text>().text = DataBase.score.ToString();
        nextGroup.SetActive(false);
        nextGroupPanel.SetActive(false);

        scoreBoard.UpdateScores(DataBase.score);
        melody.mute = true;
        painter.Colorize();
    }

    public void Music()
    {
        if (melody.mute)
            melody.mute = false;
        else
            melody.mute = true;
    }

    public void Pause()
    {
        if(DataBase.isPause)
        {
            Time.timeScale = 1;
            gameCanvas.SetActive(true);
            menuCanvas.SetActive(false);
            nextGroupPanel.SetActive(true);
            DataBase.isPause = false;
            melody.Play();
            if (!DataBase.inGame)
            {
                spawner.SpawnNext();
                spawner.MoveToGame();
                DataBase.inGame = true;
                score = GameObject.Find("Score").GetComponent<Text>();
                level = GameObject.Find("Level").GetComponent<Text>();
            }
            if(nextGroup)
                nextGroup.SetActive(true);
            painter.Colorize();
        }
        else
        {
            melody.Pause();
            if (nextGroup)
                nextGroup.SetActive(false);
            Time.timeScale = 0;
            gameCanvas.SetActive(false);
            menuCanvas.SetActive(true);
            nextGroupPanel.SetActive(false);
            DataBase.isPause = true;
            painter.Colorize();
        }
    }

    void SetColors()
    {
        float red = PlayerPrefs.GetFloat("ColorRed");
        float green = PlayerPrefs.GetFloat("ColorGreen");
        float blue = PlayerPrefs.GetFloat("ColorBlue");

        redSlider.value = red;
        greenSlider.value = green;
        blueSlider.value = blue;

        DataBase.gameColor = new Color(red, green, blue);
        painter.Colorize();
    }

    public void OnChangeColor()
    {
        float red = Mathf.Clamp(redSlider.value, 0.1f, 1);
        float green = Mathf.Clamp(greenSlider.value, 0.1f, 1);
        float blue = Mathf.Clamp(blueSlider.value, 0.1f, 1);

        PlayerPrefs.SetFloat("ColorRed", red);
        PlayerPrefs.SetFloat("ColorGreen", green);
        PlayerPrefs.SetFloat("ColorBlue", blue);
        DataBase.gameColor = new Color(red, green, blue);
        painter.Colorize();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
