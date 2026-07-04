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
    public int upgradeNumber = 1;
    private TextMeshProUGUI upgradeText;
    private Color selected = new Color32(34, 69, 29, 255);

    // Get components on start
    void Start()
    {
        upgradeButton = GetComponent<Button>();
        upgradeButton.onClick.AddListener(Clicked);

        highlight = upgradeButton.GetComponent<Image>();

        scoreManager = GameObject.Find("BarManager").GetComponent<S_ScoreManager>();

        // Make sure the player has no upgrades left over from last session
        gameData.resetUpgrades(upgradeNumber);

        upgradeText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Clicked()
    {
        // If the player can afford the upgrade
        if (gameData.score >= cost)
        {
            // Tell the scriptableObject to buy the upgrade
            gameData.score -= cost;
            gameData.upgrades(upgradeNumber);

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
        highlight.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.enabled = false;
    }
}
