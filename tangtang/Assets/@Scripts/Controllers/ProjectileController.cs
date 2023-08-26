using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectileController : SkillBase
{
    CreatureController _owner;
    Vector3 _moveDir;
    
	public ProjectileController() : base(Define.SkillType.None)
	{

	}

	public override bool Init()
	{
		base.Init();
		StartDestroy(lifetime);
		return true;
	}
	public void SetSkillData(SkillData skillData)
	{
		speed = skillData.speed;
		damage = skillData.damage;
		lifetime = skillData.lifetime;
		skillname = skillData.skillname;
	}
	public void SetInfo(SkillData skillData, CreatureController owner, Vector3 moveDir)
	{
		SetSkillData(skillData);
		_owner = owner;
		_moveDir = moveDir;
	}

	public override void UpdateController()
	{
		switch(skillname)
		{
			case Define.FIREBALL_NAME:
                transform.position += _moveDir * speed * Time.deltaTime;
                break;
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		MonsterController target = collision.gameObject.GetComponent<MonsterController>();

        if (target.IsValid() == false)
			return;
		if (this.IsValid() == false)
			return;
        if (target.CreatureState == Define.CreatureState.Dead)
            return;

        if (target.MakeDead == true)
		{
            StopDestroy();

            Managers.Object.Despawn(this);

            target.MakeDead = false;
        }

        target.OnDamaged(_owner, damage);

		StopDestroy();

		Managers.Object.Despawn(this);
	}
}
