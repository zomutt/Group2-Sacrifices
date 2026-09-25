using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform partyTf;
    
    [SerializeField]
    private float moveSpeed;

    void Start()
    {
        partyTf = PartyController.Instance.transform;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, partyTf.position, moveSpeed * Time.deltaTime);
    }
}
