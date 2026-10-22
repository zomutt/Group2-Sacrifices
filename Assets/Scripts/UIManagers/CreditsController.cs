using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;

    public void OnClickMainMenu()
    {
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(clickSound);
        SceneManager.LoadScene("MainMenuScene");        
    }
}
