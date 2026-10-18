using UnityEngine;
using System.Collections;

public class CreditScroll : MonoBehaviour
{
    [Header("Scroll Settings")] 
    [SerializeField] private float scrollSpeed = 50f;
    [SerializeField] private float scrollDelay = 1.5f;
    [SerializeField] private float exitYPosition = 1200f;   // Where credits stop/reset
    
    private RectTransform rectTransform;
    private bool canStart;
    
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canStart = false;
        StartCoroutine(ScrollDelay());
    }

    private void Update()
    {
        if (!canStart) return;
        
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
    }

    private IEnumerator ScrollDelay()
    {
        yield return new WaitForSeconds(scrollDelay);
        canStart = true;
    }
    
}
