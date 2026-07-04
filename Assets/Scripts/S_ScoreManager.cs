using System.Collections;
using TMPro;
using UnityEngine;

// This class is the parent to each bar in the canvas
// It takes the number of targets hit and scores them, dispaying this on the scoreText 

public class S_ScoreManager : MonoBehaviour
{
    [SerializeField] private int maxCombo = 0;
    public TextMeshProUGUI scoreText;
    public SO_GameData gameData;
    public S_Target[] targets;
    private bool scored = false;
    private bool scoreComboInvoked = false;
    private string comboText;
    private int scoreToAdd = 0;
    private Color greenDisplay = new Color32(117, 231, 97, 255);

    private void Start()
    {
        gameData.score = 0;
        gameData.previousComboAmount = 0;
        gameData.secondPreviousComboAmount = 0;
    }

    // Receive the total number of targets hit.
    // This will be called 3 times from S_Bar (one for every bar)
    public void Combo(int targetHit)
    {
        maxCombo += targetHit;
        
        // Wait until the function gets called all 3 times for the maxCombo to be fully counted
        Invoke("ScoreCombo", 0.1f);
    }

    private void ScoreCombo()
    {
        // Only allow this part to get ran once
        if (!scoreComboInvoked)
        {
            switch (maxCombo)
            {
                case 0:
                    comboText = "MISS";
                    scoreToAdd = 0;

                    gameData.previousComboAmount = 0;
                    scored = false;
                    break;

                case 1:
                    if (gameData.previousComboAmount == 1)
                    {
                        if (gameData.secondPreviousComboAmount == 1)
                        {
                            comboText = "TRIPLE SINGLE COMBO";
                            scoreToAdd = 5;
                        }
                        else
                        {
                            comboText = "DOUBLE SINGLE COMBO";
                            scoreToAdd = 3;
                        }
                    }
                    else
                    {
                        comboText = "SINGLE COMBO";
                        scoreToAdd = 1;
                    }

                    gameData.secondPreviousComboAmount = gameData.previousComboAmount;
                    gameData.previousComboAmount = 1;
                    scored = true;
                    break;

                case 2:
                    // If the last combo was also 2
                    if (gameData.previousComboAmount == 2)
                    {
                        // If the combo before the last combo was also 2
                        if (gameData.secondPreviousComboAmount == 2)
                        {
                            comboText = "TRIPLE DOUBLE COMBO";
                            scoreToAdd = 8;
                        }
                        else
                        {
                            comboText = "DOUBLE DOUBLE COMBO";
                            scoreToAdd = 6;
                        }
                    }
                    else
                    {
                        comboText = "DOUBLE COMBO";
                        scoreToAdd = 4;
                    }

                    gameData.secondPreviousComboAmount = gameData.previousComboAmount;
                    gameData.previousComboAmount = 2;
                    scored = true;
                    break;

                case 3:
                    // If the last combo was also 3
                    if (gameData.previousComboAmount == 3)
                    {
                        // If the combo before the last combo was also 3
                        if (gameData.secondPreviousComboAmount == 3)
                        {
                            comboText = "TRIPLE PERFECT COMBO";
                            scoreToAdd = 12;
                        }
                        else
                        {
                            comboText = "DOUBLE PERFECT COMBO";
                            scoreToAdd = 10;
                        }
                    }
                    else
                    {
                        comboText = "PERFECT COMBO";
                        scoreToAdd = 8;
                    }

                    gameData.secondPreviousComboAmount = gameData.previousComboAmount;
                    gameData.previousComboAmount = 3;
                    scored = true;
                    break;

                default:
                    break;
            }

            gameData.score += scoreToAdd;
            scoreText.text = "SCORE:+" + scoreToAdd + "\n" + comboText;

            // Only randomise the position of the targets if the player didn't miss
            if (scored)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    targets[i].RandomPos();
                }
                scored = false;
            }

            // Reset this method so that it can run again
            maxCombo = 0;
            scoreComboInvoked = true;
            Invoke("AllowNextCombo", 0.1f);

            // Cause the scoreText to blink a few times
            StartCoroutine(DelayScreenBlinking(0.5f));

            // Set the scoreText back to usual
            Invoke("ClearComboText", 3.5f);
        }
    }

    // This is just to prevent the main scoring code from running multiple times at once
    private void AllowNextCombo()
    {
        scoreComboInvoked = false;
    }

    // Remove any flavour text for combos and just show the player their score
    public void ClearComboText()
    {
        comboText = "";
        scoreText.text = "SCORE: " + gameData.score;
    }

    // Cause the scoreText to blink a few times
    IEnumerator DelayScreenBlinking(float timeBetweenBlinks)
    {
        yield return new WaitForSeconds(timeBetweenBlinks);
        scoreText.color = Color.clear;
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(timeBetweenBlinks);
            if (i % 2 == 0)
            {
                scoreText.color = greenDisplay;
            }
            else
            {
                scoreText.color = Color.clear;
            }    
        }
    }
}
