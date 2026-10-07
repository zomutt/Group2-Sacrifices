using UnityEngine;
using System.Collections.Generic;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] List<ObjectPooler> particles = new List<ObjectPooler>();
    
    public GameObject ParticleList()
    {
        return particles[0].GetPooledObject();
    }
}
