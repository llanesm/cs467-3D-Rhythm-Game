using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public ScoreVariable Score;

    void Update()
    {
        string scoreText = Score.Value.ToString();
        while (scoreText.Length < 5)
        {
            scoreText = string.Concat(" ", scoreText);
        }
        this.GetComponent<TextMesh>().text = scoreText;
    }
}
