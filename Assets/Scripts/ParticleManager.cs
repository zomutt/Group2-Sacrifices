using UnityEngine;
using System.Collections.Generic;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] List<ObjectPooler> particles = new List<ObjectPooler>();
    
    public static ParticleManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public GameObject GetParticleObject(int index)
    {
        if (index >= particles.Count)
        {
            Debug.LogError("Particle index out of range, failsafe activated");
            return particles[0].GetPooledObject();
        }
        
        return particles[index].GetPooledObject();
    }
}
