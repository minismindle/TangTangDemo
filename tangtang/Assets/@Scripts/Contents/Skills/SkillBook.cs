using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
	// 일종의 스킬 매니저
	public Dictionary<string,SkillBase> Skills { get; } = new Dictionary<string,SkillBase>();
	public List<SkillBase> RepeatedSkills { get; } = new List<SkillBase>();

	public SkillData GetSkillData(string skillname) 
	{
		SkillBase skillData = null;
		Skills.TryGetValue(skillname, out skillData );	

		if (skillData == null) 
			return null;

		return skillData.SkillData;
	}
	public void SetSkillData(string skillname,int templateID)
	{
		Skills[skillname].SetInfo(templateID);
	}
    public T AddSkill<T>(Vector3 position, string skillname,Transform parent = null) where T : SkillBase
    {
        System.Type type = typeof(T);
		
        if (type == typeof(FireballSkill))
        {
			var fireball = Managers.Object.Spawn<FireballSkill>(position, Define.FIREBALL_ID_LEVEL0);
            fireball.transform.SetParent(parent);
			fireball.ActivateSkill();

			Skills.Add(skillname,fireball);
			RepeatedSkills.Add(fireball);
			SetSkillData(Define.FIREBALL_NAME, Define.FIREBALL_ID_LEVEL0);
			return fireball as T;
		}
        else if(type == typeof(Saw))
		{
            var saw = Managers.Object.Spawn<Saw>(position, Define.SAW_ID_LEVEL0);
            saw.transform.SetParent(parent);
            saw.ActivateSkill();

            Skills.Add(skillname, saw);
            RepeatedSkills.Add(saw);
            SetSkillData(Define.SAW_NAME, Define.SAW_ID_LEVEL0);

            return saw as T;
        }
		else if(type == typeof(Sword))
		{
			var sword = Managers.Object.Spawn<Sword>(position,Define.SWORD_ID_LEVEL0);
			sword.transform.SetParent(parent);

			Skills.Add(skillname,sword);
			SetSkillData(Define.SWORD_NAME,Define.SWORD_ID_LEVEL0);

			return sword as T;
		}
		else if(type == typeof(Bomb))
		{
			var bomb = Managers.Object.Spawn<Bomb>(position, Define.BOMB_ID_LEVEL0);
			bomb.transform.SetParent(parent);

			Skills.Add(skillname, bomb);
			SetSkillData(Define.BOMB_NAME,Define.BOMB_ID_LEVEL0); 
			
			return bomb as T;
		}

		return null;
    }

	int _sequenceIndex = 0;


    bool _stopped = false;

	public void StopSkills()
	{
		_stopped = true;

		foreach (var skill in RepeatedSkills)
		{
			skill.StopAllCoroutines();
		}
	}
}
