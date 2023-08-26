using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : BaseController
{
	public CreatureController Owner { get; set; }
	public Define.SkillType SkillType { get; set; } = Define.SkillType.None;
    public Data.SkillData SkillData { get; protected set; }
	
	public virtual int templateID { get; set; }
	public virtual int prev { get; set; }	
	public virtual int next { get; set; }	
	public virtual string type { get; set; }
	public virtual string skillname { get; set; }
	public virtual string prefab { get; set; }
	public virtual int level { get; set; }
	public virtual int damage { get; set; }
	public virtual float attackrange { get; set; }
	public virtual float cooltime { get; set; }	
	public virtual float lifetime { get; set; }
	public virtual float speed { get; set; }
    
    public SkillBase(Define.SkillType skillType)
	{
		SkillType = skillType;
	}
    
    public virtual void ActivateSkill()
	{

	}
	public virtual void DeactivateSkill() 
	{ 

	}	
	public override void SetInfo(int templateID)
	{

	}

    protected virtual void GenerateProjectile(SkillData skillData, CreatureController owner, Vector3 startPos, Vector3 dir, Vector3 targetPos)
	{
		ProjectileController pc = Managers.Object.Spawn<ProjectileController>(startPos, templateID);
		pc.SetInfo(skillData, owner, dir);
	}

	#region Destroy
	Coroutine _coDestroy;

	public void StartDestroy(float delaySeconds)
	{
		StopDestroy();
		_coDestroy = StartCoroutine(CoDestroy(delaySeconds));
	}

	public void StopDestroy()
	{
		if (_coDestroy != null)
		{
			StopCoroutine(_coDestroy);
			_coDestroy = null;
		}
	}

	IEnumerator CoDestroy(float delaySeconds)
	{
		yield return new WaitForSeconds(delaySeconds);
		if (this.IsValid())
		{
			Managers.Object.Despawn(this);
		}
	}

	#endregion
}
