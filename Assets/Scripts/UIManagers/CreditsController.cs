using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This is just here for testing. It'll get removed when there's a DDOL UIController.
/// </summary>
public class CreditsController : MonoBehaviour
{
    public void OnClickMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");        
    }
}
