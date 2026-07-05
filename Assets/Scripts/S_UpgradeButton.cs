using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    }

    private void Clicked()
    {
        // If the player can afford the upgrade
        if (gameData.score >= cost)
        {
            // Tell the scriptableObject to buy the upgrade
            gameData.score -= cost;
            upgradeManager.UpgradeSwitch(upgradeName);

            // Do not allow the upgrade to be interacted with afterwards
            upgradeText.color = selected;
            upgradeButton.interactable = false;
        }

        // Reset the score text to reflect the change in score
        scoreManager.ClearComboText();
    }

    // Show/hide the outline depending on if the mouse hovers over the upgrade or not
    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.color = greenDisplay;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.color = selected;
    }
}
