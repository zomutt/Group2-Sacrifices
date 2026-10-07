using TMPro;
using UnityEngine;

public class StatusIcon : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private TextMeshPro text;

    public void SetIcon(StatusEffect status)
    {
        spriteRenderer.sprite = status.icon;
        UpdateValue(status);
    }

    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    public void UpdateValue(StatusEffect status)
    {
        text.text = Mathf.FloorToInt(status.Duration).ToString();
    }
    
    public void UpdatePosition(float yValue)
    {
        transform.localPosition = Vector3.up * yValue;
    }
}
