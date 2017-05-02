using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    /// <summary>
    /// Player score
    /// </summary>
    public static int Score = 0;

    /// <summary>
    /// Score UI text
    /// </summary>
    private Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
        Reset();
    } 


    public void ScorePoints(int points)
    {
        Score += points;
        scoreText.text = Score.ToString();
    }

    public static void Reset()
    {
        Score = 0;
    }
}
