using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deckbuilder
{
    public class SkillEvent : UnityEvent<BaseSkill> { }

    [CreateAssetMenu(
        fileName = "SkillVariable.asset",
        menuName = "Variables/New Variable_"+ "Skill",
        order = 5)]
    public class SkillVariable : BaseVariable<BaseSkill, SkillEvent>
    {
    }
}
