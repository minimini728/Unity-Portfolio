using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnum
{
    public enum eEventType
    {
        None = -1,
        ChangeToOutPost,
        ChangeToOutPostTuTo,
        ChangeToBattleField,
        ChangeToBattleFieldTuto,
        IncreaseSkillValue,
        ChangeAnimation,
        GetItem,
        RefreshInventoryUI,
        HitMob,
        HitBoss,
        IncreasePlayerHp,
        DeclinePlayerHp,
        ResetPlayerSkill,
        IncreasePlayerHpBar,
        DeclinePlayerHpBar,
        IncreasePlayerSkillBar,
        ResetPlayerSkillBar,
        BuildOutPost,
        ShowUIBuildNotice,
        HideUIBuildNotice,
        EnterPark,
        WaveCut,
        BossCut,
        ResetPlayerHpBar,
        FullPlayerSkillValue,
        FullPlayerSkillBar,
        DoubleHitBoss,
        ResetPlayerHpValue
    }
}
