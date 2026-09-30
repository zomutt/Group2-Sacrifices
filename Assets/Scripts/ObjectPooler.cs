using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

// Nathan Wilson : ObjectPooler class
// 
// ObjectPooler creates a pool of objects to draw from.
// These objects remain inactive until called upon by another script.
public class ObjectPooler : MonoBehaviour
{
    [SerializeField]
    private GameObject currentPrefab;

    [SerializeField]
    private int startingSize;

    private List<GameObject> pooledObjects = new();

    public List<GameObject> biomeVariants = new();

    private void Awake()
    {
        // Add objects to fill starting size of pool
        for (int i = 0; i < startingSize; i++)
        {
            CreatePooledObject();
        }
    }

    public GameObject GetPooledObject()
    {
        // Check pool for available objects
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                // Return available object
                return pooledObjects[i];
            }
        }
        
        // Return new member of pool
        return CreatePooledObject();
    }

    // Create new member of pool; return object
    private GameObject CreatePooledObject()
    {
        GameObject newGameObject = Instantiate(currentPrefab);
        newGameObject.SetActive(false);
        pooledObjects.Add(newGameObject);
        return newGameObject;
    }

    public void ChangeBiome(string changingTo)
    {
        currentPrefab = ReturnBiomeVariant(changingTo);
    }

    public GameObject ReturnBiomeVariant(string biome)
    {
        GameObject newPrefab = currentPrefab;

        switch (biome)
        {
            case "Plains":
                newPrefab = biomeVariants[0];
                break;
            case "Graveyard":
                newPrefab = biomeVariants[1];
                break;
            case "Castle":
                newPrefab = biomeVariants[2];
                break;
        }

        return newPrefab;
    }
}
