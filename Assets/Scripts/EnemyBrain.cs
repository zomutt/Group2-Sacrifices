using System;
using UnityEngine;

[Serializable]
public class EnemyBrain
{
    [Serializable]
    public class UsableMove
    {
        [Serializable]
        public class MoveCondition
        {
            [Serializable]
            public enum ConditionType
            {
                AOutOfB,
                EnemyCountAToB
            }
            
            public ConditionType conditionType;
            
            public int a;
            public int b;
            
            public bool TestCondition()
            {
                switch (conditionType)
                {
                    case ConditionType.AOutOfB:
                        return UnityEngine.Random.Range(0, a) < b;
                        
                    case ConditionType.EnemyCountAToB:
                        return CombatManager.Instance.EnemyCount >= a
                            && CombatManager.Instance.EnemyCount <= b;
                    
                    default:
                        return true;
                }
            }
        }
        
        public MoveCondition[] moveConditions;
        
        public bool TestConditions()
        {
            foreach (MoveCondition condition in moveConditions)
            {
                if (!condition.TestCondition())
                    return false;
            }
            
            return true;
        }
    }
    
    public UsableMove[] moves;
    
    public int GetUsableMoveIndex()
    {
        for (int i = 0; i < moves.Length; i++)
        {
            if (moves[i].TestConditions())
                return i;
        }
        
        return moves.Length - 1;
    }
}
