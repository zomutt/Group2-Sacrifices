using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This is just here for testing. It'll get removed when there's a DDOL UIController.
/// </summary>
public class CreditsController : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;

    public void OnClickMainMenu()
    {
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(clickSound, 1f);
        SceneManager.LoadScene("MainMenuScene");        
    }
}
