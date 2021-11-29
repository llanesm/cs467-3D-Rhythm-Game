using UnityEngine;

public class GameOverStreakText : MonoBehaviour
{
    public HighestStreakVariable HighestStreak;

    // Start is called before the first frame update
    void Start()
    {
        string streakText = HighestStreak.Value.ToString();
        streakText = string.Concat("Longest\nStreak: ", streakText);
        this.GetComponent<TextMesh>().text = streakText;
    }
}