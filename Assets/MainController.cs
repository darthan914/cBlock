using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour {

    public enum PlayMode
    {
        TimeAttack, BrainStorm
    }

    public PlayMode playMode;
    public List<GameObject> blocks;
    public List<GameObject> specialBlocks;
    public int maxBlock = 4;
    public float rateSpecial = 10f;
    public float timerSet = 60f;

    public string[] rightMessages;
    public string[] wrongMessages;

    public Text[] digitalText;
    private int blockScore;

    private int combo;
    private int maxCombo;
    public string messageText;

    void Start()
    {
        Time.timeScale = 0f;

        foreach (Text text in digitalText)
        {
            text.text = "";
        }

        messageText = "Press Start to begin";
    }

    void Update()
    {
        maxBlock = Mathf.Clamp(maxBlock, 1, blocks.Count);

        if(timerSet > 0)
        {
            timerSet = timerSet - Time.deltaTime;
        }

        if(playMode == PlayMode.TimeAttack)
        {
            digitalText[0].text = TimerText(timerSet);
            digitalText[1].text = BlocksText(blockScore);
            digitalText[2].text = ComboText();
            digitalText[3].text = MessageText();
        }

        if (timerSet <= 0)
        {
            messageText = "Time Up! Press Restart to try again";
        }
        
    }

    string TimerText(float f)
    {
        string minutes = ((int)f / 60).ToString("d2");
        string seconds = (f % 60).ToString("f2");

        return "Timer\n" + minutes + ":" + seconds;
    }

    string BlocksText(float f)
    {
        return "Blocks\n" + blockScore.ToString("d4");
    }

    string ComboText()
    {
        maxCombo = Mathf.Max(combo, maxCombo);
        return "Combo/Max\n" + combo.ToString("d3") + "/" + maxCombo.ToString("d3");
    }

    string MessageText()
    {
        return messageText;
    }

    public void AddBlocks(int value)
    {
        blockScore = blockScore + value;
        combo++;
    }

    public void TimeBonus(float t)
    {
        timerSet = timerSet + t;
    }

    public void BreakCombo()
    {
        combo = 0;
    }

    public void GenerateMessages(bool valid)
    {
        if(timerSet > 0)
        {
            if (valid)
            {
                messageText = rightMessages[Random.Range(0, rightMessages.Length)];
            }
            else
            {
                messageText = wrongMessages[Random.Range(0, wrongMessages.Length)];
            }
        }
        else
        {
            messageText = "Time Up!";
        }
        
    }

}
