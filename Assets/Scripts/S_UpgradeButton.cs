using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// This class is attached to the upgrade buttons in the shop display
// It detects mouse interactions and communicates to the UpgradeManager if the player can buy the clicked upgrade

public class S_UpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button upgradeButton;
    private Image highlight;
    public SO_GameData gameData;
    public int cost = 1;
    private S_ScoreManager scoreManager;
    private TextMeshProUGUI upgradeText;
    private Color greenDisplay = new Color32(117, 231, 97, 255);
    private Color selected = new Color32(34, 69, 29, 255);
    private S_UpgradeManager upgradeManager;
    private string upgradeName;

    private S_SoundManager soundManager;
    public AudioClip hoverSound, selectSound, incorrectSound;


    // Get components on start
    void Start()
    {
        upgradeButton = GetComponent<Button>();
        upgradeButton.onClick.AddListener(Clicked);

        highlight = upgradeButton.GetComponent<Image>();

        scoreManager = GameObject.Find("ScoreManager").GetComponent<S_ScoreManager>();
        upgradeManager = GameObject.Find("S_UpgradeManager").GetComponent<S_UpgradeManager>();
        upgradeText = GetComponentInChildren<TextMeshProUGUI>();
        upgradeName = upgradeText.name;

        soundManager = GameObject.Find("SoundManager").GetComponent<S_SoundManager>();
    }

    private void Clicked()
    {
        // If the player can afford the upgrade
        if (gameData.score >= cost)
        {
            // Tell the scriptableObject to buy the upgrade
            gameData.score -= cost;
            upgradeManager.UpgradeSwitch(upgradeName);

            // Play sound effect to give feedback to player
            soundManager.PlaySound(selectSound);

            // Do not allow the upgrade to be interacted with afterwards
            upgradeText.color = selected;
            upgradeButton.interactable = false;
        }
        else
        {
            // Play sound effect to give feedback to player
            soundManager.PlaySound(incorrectSound);
        }

            // Reset the score text to reflect the change in score
            scoreManager.ClearComboText();
    }

    // Show/hide the outline depending on if the mouse hovers over the upgrade or not
    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.color = greenDisplay;

        // Play sound effect to give feedback to player
        soundManager.PlaySound(hoverSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.color = selected;
    }
}
