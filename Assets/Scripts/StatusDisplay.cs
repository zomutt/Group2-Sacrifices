using System.Collections.Generic;
using UnityEngine;

public class StatusDisplay : MonoBehaviour
{
    private ObjectPooler pool;
    
    private CharacterBehavior character;
    
    private List<StatusEffect> statuses;
    private List<StatusIcon> icons;
    
    private readonly float iconSpacing = 0.6f;

    void Awake()
    {
        pool = GetComponent<ObjectPooler>();
        
        character = transform.parent.GetComponent<CharacterBehavior>();
        character.statusDisplay = this;
        
        statuses = new();
        icons = new();
    }

    void Update()
    {
        UpdateDisplays();
    }
    
    public void AddStatus(StatusEffect status)
    {
        if (status.hidden) return;
        
        statuses.Add(status);
        icons.Add(GetNewIcon(status));
        
        UpdateIconPositions();
    }
    
    private StatusIcon GetNewIcon(StatusEffect status)
    {
        GameObject newIconObject = pool.GetPooledObject();
        newIconObject.transform.parent = transform;
        //newIconObject.transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(0, 90, 0);
        
        StatusIcon newIcon = newIconObject.GetComponent<StatusIcon>();
        newIcon.SetIcon(status);
        newIconObject.SetActive(true);
        
        return newIcon;
    }
    
    public void RemoveStatus(StatusEffect status)
    {
        if (!statuses.Contains(status)) return;
        
        int i = statuses.IndexOf(status);
        
        statuses.RemoveAt(i);
        icons.RemoveAt(i);
        
        UpdateIconPositions();
    }
    
    private void UpdateDisplays()
    {
        for (int i = 0; i < icons.Count; i++)
        {
            icons[i].UpdateValue(statuses[i]);
        }
    }
    
    private void UpdateIconPositions()
    {
        for (int i = 0; i < icons.Count; i++)
        {
            icons[i].UpdatePosition(i * iconSpacing);
            
            Debug.Log(icons[i].transform.position);
        }
    }
}
