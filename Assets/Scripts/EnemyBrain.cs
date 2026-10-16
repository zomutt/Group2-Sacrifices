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
                RandomAOutOfB,
                EnemyCountAToB,
                MoveLastTurn
            }
            
            public ConditionType conditionType;
            
            public int a;
            public int b;
            
            public bool TestCondition(int previousMoveIndex)
            {
                switch (conditionType)
                {
                    case ConditionType.RandomAOutOfB:
                        return UnityEngine.Random.Range(0, b) < a;
                        
                    case ConditionType.EnemyCountAToB:
                        return CombatManager.Instance.EnemyCount >= a
                            && CombatManager.Instance.EnemyCount <= b;
                        
                    case ConditionType.MoveLastTurn:
                        return previousMoveIndex == a;
                    
                    default:
                        return true;
                }
            }
        }
        
        public MoveCondition[] moveConditions;
        
        public bool TestConditions(int previousMoveIndex)
        {
            foreach (MoveCondition condition in moveConditions)
            {
                if (!condition.TestCondition(previousMoveIndex))
                    return false;
            }
            
            return true;
        }
    }
    
    public UsableMove[] moves;
    
    private int previousMoveIndex = -1;
    
    public int GetUsableMoveIndex()
    {
        for (int i = 0; i < moves.Length; i++)
        {
            if (moves[i].TestConditions(previousMoveIndex))
            {
                previousMoveIndex = i;
                return i;
            }
        }
        
        previousMoveIndex = moves.Length - 1;
        return moves.Length - 1;
    }
}
