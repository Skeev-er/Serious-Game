using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class S_ShopButton : MonoBehaviour
{
    private Button shopButton;
    private TextMeshProUGUI shopButtonText;
    private Color originalShopTextColour;

    private Vector3 originalZoomPosition = new Vector3(0, 0, -10);
    private Vector3 newZoomPosition = new Vector3(-0.78f, 2.17f, -10f);
    private float t = 0f;
    private bool clicked = false;
    private bool openShop = true;
    public Camera cam;

    public GameObject scoreText;
    public GameObject shopScreenPanel;

    public S_SoundManager soundManager;
    public AudioClip selectSound;

    void Start()
    {
        shopButton = GetComponent<Button>();
        shopButton.onClick.AddListener(Clicked);

        shopButtonText = GetComponentInChildren<TextMeshProUGUI>();
        originalShopTextColour = shopButtonText.color;

        shopScreenPanel.SetActive(false);
    }

    private void Update()
    {
        if (clicked)
        {
            // If you are opening the shop, transition from original position and zoom to the new version
            if (openShop)
            {
                cam.transform.position = Vector3.Lerp(originalZoomPosition, newZoomPosition, t);
                cam.orthographicSize = Mathf.Lerp(5f, 2.35f, t);
            }
            // If closing the shop, do this in reverse
            else
            {
                cam.transform.position = Vector3.Lerp(newZoomPosition, originalZoomPosition, t);
                cam.orthographicSize = Mathf.Lerp(2.35f, 5f, t);
            }

            // If the lerp is done
            t += Time.deltaTime*1.4f;
            if (t >= 1)
            {
                if (openShop)
                {
                    scoreText.SetActive(false);
                    shopScreenPanel.SetActive(true);
                }
                else
                {
                    scoreText.SetActive(true);
                    shopScreenPanel.SetActive(false);
                }

                // Reset variables to allow this to happen next time
                clicked = false;
                openShop = !openShop;
                t = 0;
                shopButton.enabled = true;
            }
        }
    }

    private void Clicked()
    {
        // Make text invisible and wait before resetting it
        shopButtonText.color = Color.clear;
        StartCoroutine(ReturnButtonColour(0.85f));

        // Allow the update code to run
        clicked = true;
        // Do not allow the button to be pressed during the transition
        shopButton.enabled = false;

        // Play sound effect to give feedback to player
        soundManager.PlaySound(selectSound);
    }

    // Make the text visible again after the delay
    IEnumerator ReturnButtonColour(float timeBeforeReturn)
    {
        yield return new WaitForSeconds(timeBeforeReturn);
        shopButtonText.color = originalShopTextColour;

        // Set the text according to if you can now open or close the shop
        if (openShop)
        {
            shopButtonText.text = "OPEN SHOP";
        }
        else
        {
            shopButtonText.text = "CLOSE SHOP";
        } 
    }
}
