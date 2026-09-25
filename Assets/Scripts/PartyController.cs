using System.Collections.Generic;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    [SerializeField]
    private List<CharacterBehavior> characters;
    
    private List<Vector3> trail = new();
    private float pullAmount;
    private readonly float trailStep = 0.25f;

    void Start()
    {
        ChangeTrailLength(Mathf.CeilToInt(characters.Count / trailStep) + 1);
    }

    void Update()
    {
        PullTrail();
    }

    private void PullTrail()
    {
        while (VectTrailToPrevTrail(0).magnitude > trailStep)
        {
            trail.Insert(0, trail[0] + VectTrailToPrevTrail(0).normalized * trailStep);
            trail.RemoveAt(trail.Count - 1);
        }
        
        pullAmount = VectTrailToPrevTrail(0).magnitude;
        
        for (int i = 0; i < characters.Count; i++)
        {
            float temp = i / trailStep - pullAmount;
            
            characters[i].transform.position =
                    trail[Mathf.CeilToInt(temp)]
                    + VectTrailToPrevTrail(Mathf.CeilToInt(temp)).normalized * (temp % 1) / trailStep;
        }
    }
    
    private Vector3 VectTrailToPrevTrail(int index)
    {
        if (index <= 0)
            return transform.position - trail[0];
        else
            return trail[index - 1] - trail[index];
    }

    private void ChangeTrailLength(int length)
    {
        if (length > trail.Count)
        {
            for (int i = trail.Count; i < length; i++)
            {
                if (i == 0)
                    trail.Add(transform.position);
                else
                    trail.Add(trail[i - 1]);
            }
        }
        else
        {
            for (int i = trail.Count; i > length; i--)
            {
                trail.RemoveAt(i - 1);
            }
        }
    }
}
