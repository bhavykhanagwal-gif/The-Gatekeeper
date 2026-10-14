using UnityEngine;
using TMPro;                      // needed for TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;   // drag ScoreText here
    public TextMeshProUGUI boomText;
    public TextMeshProUGUI highScoreText;
    int kills = 0;
    public int booms = 0;
    public int boomcounter = 0;
     int highScore = 0;
    public void AddKill()
    {
        kills++;
        scoreText.text = "Kills: " + kills;   // replaces Debug.Log
         if (kills > highScore)
        {
            highScore = kills;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "High Score: " + highScore;
        }
    }

    public void AddBoom()
    {
        boomcounter++;
        if(boomcounter>=5)
        {
            booms++;
            boomText.text = "Booms: " + booms;   // replaces Debug.Log
            boomcounter=0;
        }
    
    }
    public void GoBoom()
    {
        booms--;
        boomText.text = "Booms: " + booms;   // replaces Debug.Log
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
    }
}