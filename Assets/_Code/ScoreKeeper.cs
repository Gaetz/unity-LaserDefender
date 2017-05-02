using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    /// <summary>
    /// Player score
    /// </summary>
    public int Score;

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

    public void Reset()
    {
        Score = 0;
        scoreText.text = Score.ToString();
    }
}
