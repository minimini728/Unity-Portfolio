using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnum 
{
    public enum eEventType
    {
        None = -1,
        AvoidGameOver,
        HardestGameOver,
        HardestGoal,
        StartMainGame,
        StartMiniGame,
        BackMainGame,
        ClearStage,
        AvoidUpScore,
        AvoidGameClear,
        AvoidChangeFace,
        ShakeBlock,
        ObstacleTrigger,
        NewGame,
        DestroyTotem

    }
}
