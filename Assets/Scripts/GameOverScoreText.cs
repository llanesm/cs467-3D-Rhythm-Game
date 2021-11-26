using UnityEngine;

public class GameOverScoreText : MonoBehaviour
{
    public ScoreVariable Score;

    // Start is called before the first frame update
    private void Start()
    {
        string scoreText = Score.Value.ToString();
        scoreText = string.Concat("Score: ", scoreText);
        this.GetComponent<TextMesh>().text = scoreText;
    }
}