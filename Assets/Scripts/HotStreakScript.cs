using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotStreakScript : MonoBehaviour
{
    public HotStreakVariable HotStreak;

    void Update()
    {
        string multiplierText = string.Concat(HotStreak.Multiplier.ToString(), "x");
        this.GetComponent<TextMesh>().text = multiplierText;
    }
}
