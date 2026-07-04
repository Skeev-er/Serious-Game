using UnityEngine;

[CreateAssetMenu(fileName = "SO_GameData", menuName = "Scriptable Objects/SO_GameData")]
public class SO_GameData : ScriptableObject
{
    public int score = 0;
    public int previousComboAmount = 0;
    public int secondPreviousComboAmount = 0;

    public bool upgrade1 = false;
    public bool upgrade2 = false;
    public bool upgrade3 = false;
    public bool upgrade4 = false;
    public bool upgrade5 = false;
    public bool upgrade6 = false;
    public bool upgrade7 = false;
    public bool upgrade8 = false;

    public void upgrades(int upgradeNumber)
    {
        switch (upgradeNumber)
        {
            case 1:
                upgrade1 = true; 
                break;
            case 2:
                upgrade2 = true; 
                break;
            case 3:
                upgrade3 = true;
                break;
            case 4:
                upgrade4 = true;
                break;
            case 5:
                upgrade5 = true;
                break;
            case 6:
                upgrade6 = true;
                break;
            case 7:
                upgrade7 = true;
                break;
            case 8:
                upgrade8 = true;
                break;
            default:
                break;
        }
    }

    public void resetUpgrades(int upgradeNumber)
    {
        switch (upgradeNumber)
        {
            case 1:
                upgrade1 = false;
                break;
            case 2:
                upgrade2 = false;
                break;
            case 3:
                upgrade3 = false;
                break;
            case 4:
                upgrade4 = false;
                break;
            case 5:
                upgrade5 = false;
                break;
            case 6:
                upgrade6 = false;
                break;
            case 7:
                upgrade7 = false;
                break;
            case 8:
                upgrade8 = false;
                break;
            default:
                break;
        }
    }
}
