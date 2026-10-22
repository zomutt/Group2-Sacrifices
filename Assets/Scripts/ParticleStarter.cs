using UnityEngine;

public class ParticleStarter : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particle;

    void OnEnable()
    {
        particle.Play();
    }
}
