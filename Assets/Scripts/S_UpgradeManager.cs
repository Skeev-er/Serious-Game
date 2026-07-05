using UnityEngine;

// This class is attached to the shop screen panel's content
// It calls different scripts in order to trigger their upgrade methods

public class S_UpgradeManager : MonoBehaviour
{
    public SO_GameData gameData;
    private S_ScoreManager scoreManager;
    public S_CrankRotations[] crankRotations;
    public S_Bar[] bars;

    private void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<S_ScoreManager>();
    }

    // Switch on the upgrade name passed in
    public void UpgradeSwitch(string upgradeName)
    {
        switch (upgradeName)
        {
            case "MULTI COMBO":
                scoreManager.MultiComboUnlock();
                break;

            case "REVERSE":
                for (int i = 0; i < crankRotations.Length; i++)
                {
                    crankRotations[i].ReverseUnlock();
                }
                break;

            case "MULT":
                scoreManager.SetMult(1.5f);
                break;

            case "FILL RATE":
                for (int i = 0; i < bars.Length; i++)
                {
                    bars[i].FillRateUnlock(1.5f);
                }
                break;

            default:
                break;
        }
    }
}
