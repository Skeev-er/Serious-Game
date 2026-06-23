using UnityEngine;

// This class is part of the scene, but only for windows builds
// It allows the player to quit the game upon pressing escape

public class S_Escape : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
