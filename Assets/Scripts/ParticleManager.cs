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
        return particles[index].GetPooledObject();
    }
}
