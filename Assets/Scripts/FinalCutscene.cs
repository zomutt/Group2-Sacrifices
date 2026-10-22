using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class FinalCutscene : MonoBehaviour
{
    public static FinalCutscene Instance;
    
    [SerializeField]
    private GameObject sceneObject;
    [SerializeField]
    private VideoPlayer player;
    
    private bool playingVid;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (playingVid && player.frame + 1 >= (long)player.frameCount)
            OnEnd();
            
            
    }
    
    public void PlayScene()
    {
        UIController.Instance.gameObject.SetActive(false);
        sceneObject.SetActive(true);
        player.Play();
        
        playingVid = true;
        
        //Invoke("OnEnd", 2f);
    }
    
    public void OnEnd()
    {
        Debug.Log("hey");
        UIController.Instance.gameObject.SetActive(true);
        sceneObject.SetActive(false);
        player.gameObject.SetActive(false);
        
        UIController.Instance.battleTransitioner.gameObject.SetActive(false);
    }
}
