using UnityEngine;

public class DumbKeepRotation : MonoBehaviour
{
    [SerializeField]
    private Quaternion rotation;
    
    [SerializeField]
    private bool logging;
    
    void Update()
    {
        if (logging) Debug.Log(transform.localRotation);
        transform.localRotation = rotation;
    }
}
