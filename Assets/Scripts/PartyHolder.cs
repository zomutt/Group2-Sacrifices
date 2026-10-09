using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyHolder : MonoBehaviour
{
    public static PartyHolder Instance;
    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == UIController.Instance.mainMenuScene)
        {
            Instance = null;
            Destroy(gameObject);
        }
    }
}
