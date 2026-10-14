using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovePanel : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> buttons;
    [SerializeField]
    private List<TextMeshProUGUI> buttonTexts;
    
    [SerializeField]
    private AudioClip clickSound;

    private CharacterBehavior ownerBehavior;

    public static MovePanel Instance;

    void Awake()
    {
        if (Instance != null) return;
        
        Instance = this;
        gameObject.SetActive(false);
    }

    public void OpenPanel(List<AttackBehavior> attacks, CharacterBehavior owner)
    {
        gameObject.SetActive(true);
        ownerBehavior = owner;
        
        for (int i = 0; i < buttons.Count; i++)
        {
            if (attacks.Count > i && attacks[i] != null)
            {
                buttons[i].SetActive(true);
                buttonTexts[i].text = attacks[i].attackName;
            }
            else
            {
                buttons[i].SetActive(false);
            }
        }
    }
    
    public void ChooseMove(int index)
    {
        if (AudioManager.Instance) AudioManager.Instance.PlaySFX(clickSound);

        ownerBehavior.DoMove(index);
        gameObject.SetActive(false);
    }
}
