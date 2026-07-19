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
    public float mult = 1;
    private int scoreToAdd = 0;
    private Color greenDisplay = new Color32(117, 231, 97, 255);
    private bool multiComboUpgrade = false;

    public int numberOfBlinks = 3;

    private S_SoundManager soundManager;
    public AudioClip bestSound, normalSound, badSound;
    private AudioClip scoreSound;

    private void Start()
    {
        gameData.score = 0;
        gameData.previousComboAmount = 0;
        gameData.secondPreviousComboAmount = 0;

        soundManager = GameObject.Find("SoundManager").GetComponent<S_SoundManager>();
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

                    scoreSound = badSound;

                    gameData.previousComboAmount = 0;
                    scored = false;
                    break;


                case 1:
                    // If the last combo was also 1 and the multi-combo upgrade has been unlocked
                    if (gameData.previousComboAmount == 1 && multiComboUpgrade)
                    {
                        if (gameData.secondPreviousComboAmount == 1)
                        {
                            comboText = "TRIPLE SINGLE COMBO";
                            scoreToAdd = 5;

                            scoreSound = bestSound;
                        }
                        else
                        {
                            comboText = "DOUBLE SINGLE COMBO";
                            scoreToAdd = 3;

                            scoreSound = normalSound;
                        }
                    }
                    else
                    {
                        comboText = "SINGLE COMBO";
                        scoreToAdd = 1;

                        scoreSound = badSound;
                    }

                    gameData.secondPreviousComboAmount = gameData.previousComboAmount;
                    gameData.previousComboAmount = 1;
                    scored = true;
                    break;


                case 2:
                    // If the last combo was also 2 and the multi-combo upgrade has been unlocked
                    if (gameData.previousComboAmount == 2 && multiComboUpgrade)
                    {
                        // If the combo before the last combo was also 2
                        if (gameData.secondPreviousComboAmount == 2)
                        {
                            comboText = "TRIPLE DOUBLE COMBO";
                            scoreToAdd = 8;

                            scoreSound = bestSound;
                        }
                        else
                        {
                            comboText = "DOUBLE DOUBLE COMBO";
                            scoreToAdd = 6;

                            scoreSound = normalSound;
                        }
                    }
                    else
                    {
                        comboText = "DOUBLE COMBO";
                        scoreToAdd = 4;

                        scoreSound = badSound;
                    }

                    gameData.secondPreviousComboAmount = gameData.previousComboAmount;
                    gameData.previousComboAmount = 2;
                    scored = true;
                    break;


                case 3:
                    // If the last combo was also 3 and the multi-combo upgrade has been unlocked
                    if (gameData.previousComboAmount == 3 && multiComboUpgrade)
                    {
                        // If the combo before the last combo was also 3
                        if (gameData.secondPreviousComboAmount == 3)
                        {
                            comboText = "TRIPLE PERFECT COMBO";
                            scoreToAdd = 12;

                            scoreSound = bestSound;
                        }
                        else
                        {
                            comboText = "DOUBLE PERFECT COMBO";
                            scoreToAdd = 10;

                            scoreSound = normalSound;
                        }
                    }
                    else
                    {
                        comboText = "PERFECT COMBO";
                        scoreToAdd = 8;

                        scoreSound = badSound;
                    }

                    gameData.secondPreviousComboAmount = gameData.previousComboAmount;
                    gameData.previousComboAmount = 3;
                    scored = true;
                    break;

                default:
                    break;
            }

            // Multiply score by mult
            scoreToAdd = Mathf.RoundToInt(scoreToAdd * mult);
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
        scoreText.color = greenDisplay;
        scoreText.text = "SCORE: " + gameData.score;
    }

    // Cause the scoreText to blink a few times
    IEnumerator DelayScreenBlinking(float timeBetweenBlinks)
    {
        scoreText.color = Color.clear;

        for (int i = 0; i < (numberOfBlinks * 2) + 1; i++)
        {
            yield return new WaitForSeconds(timeBetweenBlinks);
            if (i % 2 == 0)
            {
                scoreText.color = greenDisplay;
                // Play the sound every time except the last blink
                if (i != numberOfBlinks * 2)
                {
                    soundManager.PlaySound(scoreSound);
                }
            }
            else
            {
                scoreText.color = Color.clear;
            }    
        }
        // Set the scoreText back to usual
        ClearComboText();
    }

    // These are called by S_UpgradeManager
    public void MultiComboUnlock()
    {
        multiComboUpgrade = true;
        gameData.previousComboAmount = 0;
        gameData.secondPreviousComboAmount = 0;
    }

    public void SetMult(float multValue)
    {
        mult = multValue;
    }
}
