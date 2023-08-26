using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RepeatSkill : SkillBase
{

	public RepeatSkill() : base(Define.SkillType.Repeat)
	{

	}

	public override bool Init()
	{
		base.Init();

		return true;
	}

	#region CoSkill
	Coroutine _coSkill;

	public override void ActivateSkill()
	{
		if (_coSkill != null)
			StopCoroutine(_coSkill);
		_coSkill = StartCoroutine(CoStartSkill());
	}
	public override void DeactivateSkill()
	{
		if (_coSkill != null)
			StopCoroutine(_coSkill);
		_coSkill = null;
	}
    protected abstract void DoSkillJob();

	protected virtual IEnumerator CoStartSkill()
	{
		WaitForSeconds wait = new WaitForSeconds(cooltime);

		while (true)
		{
			DoSkillJob();

			yield return wait;
		}
	}
	#endregion
}
