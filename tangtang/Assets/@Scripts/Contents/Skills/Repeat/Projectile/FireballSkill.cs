using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class FireballSkill : RepeatSkill
{
    public override void SetInfo(int templateID)
	{
		SkillData = Managers.Data.SkillDic[templateID];
		this.templateID = SkillData.templateID;
		prev = SkillData.prev;
		next = SkillData.next;
		type = SkillData.type;
		skillname = SkillData.skillname;
		prefab = SkillData.prefab;
		level = SkillData.level;	
		cooltime = SkillData.cooltime;
		attackrange = SkillData.attackrange;
		ActivateSkill();
	}
    protected override IEnumerator CoStartSkill()
	{
        WaitForSeconds wait = new WaitForSeconds(cooltime);
		while (level > 0) 
		{
			DoSkillJob();
			yield return wait;
		}
    }
	protected override void DoSkillJob()
	{
		MonsterController target = null;
		if (Managers.Game.Player == null)
			return;
		attackrange = SkillData.attackrange;
		foreach (var m in Managers.Object.Monsters)
		{
			if (m.CreatureState == Define.CreatureState.Dead)
				continue;
			float diff = Vector3.Distance(m.transform.position, Managers.Game.Player.transform.position);
			if (diff > attackrange)
				continue;
			if (diff < attackrange)
			{
				attackrange = diff;
				target = m;
			}
		}
		if (target.IsValid() == false)
			return;
		if (target.CreatureState == Define.CreatureState.Dead)
			return;
		Vector3 spawnPos = Managers.Game.Player.transform.position;
		Vector3 dir = (target.transform.position - Managers.Game.Player.transform.position).normalized;
		GenerateProjectile(SkillData, Owner, spawnPos, dir, Vector3.zero);
	}
}
