using UnityEngine;

// This ScriptableObject holds variables that belong to the level itself

[CreateAssetMenu(fileName = "SO_GameData", menuName = "Scriptable Objects/SO_GameData")]
public class SO_GameData : ScriptableObject
{
    public int score = 0;
    public int previousComboAmount = 0;
    public int secondPreviousComboAmount = 0;
}
