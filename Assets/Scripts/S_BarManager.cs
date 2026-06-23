using TMPro;
using Unity.VisualScripting;
using UnityEngine;

// This class is the parent to each bar in the canvas
// It takes the number of targets hit and scores them, dispaying this on the scoreText 

public class S_BarManager : MonoBehaviour
{
    [SerializeField] private int maxCombo = 0;
    public TextMeshProUGUI scoreText;
    public SO_GameData gameData;
    public S_Target[] targets;
    private bool scored = false;
    private bool scoreComboInvoked = false;

    private void Start()
    {
        gameData.score = 0;
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
                    scoreText.text = "SCORE: " + gameData.score + "\n\nMISS";
                    scored = false;
                    break;

                case 1:
                    gameData.score += 1;
                    scoreText.text = "SCORE: " + gameData.score + "\n\nSINGLE COMBO +1";
                    scored = true;
                    break;

                case 2:
                    gameData.score += 4;
                    scoreText.text = "SCORE: " + gameData.score + "\n\nDOUBLE COMBO +4";
                    scored = true;
                    break;
                case 3:
                    gameData.score += 8;
                    scoreText.text = "SCORE: " + gameData.score + "\n\nPERFECT COMBO +8";
                    scored = true;
                    break;

                default:
                    break;
            }

            // Only randomise the position of the targets if the player didn't miss
            if (scored)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    targets[i].RandomPos();
                }
                scored = false;
            }
        }

        maxCombo = 0;
        scoreComboInvoked = true;
        Invoke("AllowNextCombo", 0.1f);
        Invoke("ClearComboText", 3f);
    }

    private void AllowNextCombo()
    {
        scoreComboInvoked = false;
    }

    private void ClearComboText()
    {
        scoreText.text = "SCORE: " + gameData.score;
    }
}
