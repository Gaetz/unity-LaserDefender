using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = ScoreKeeper.Score.ToString();
        ScoreKeeper.Reset();
    }
}
