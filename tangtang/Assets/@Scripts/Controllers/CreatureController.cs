using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CreatureController : BaseController
{
    #region State Pattern
    protected float _speed = 1.0f;

    public bool MakeDead = false;
	public SkillBook Skills { get; protected set; }
    protected Animator _animator;
    protected Rigidbody2D _rigid;
    protected SpriteRenderer _sprite;

    Define.CreatureState _creatureState = Define.CreatureState.Idle;

    public virtual Define.CreatureState CreatureState
    {
        get { return _creatureState; }
        set
        {
            _creatureState = value;
            UpdateAnimation();
        }
    }

    public virtual void UpdateAnimation(){}
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateSkill() { }
    protected virtual void UpdateHit() { }
    protected virtual void UpdateMoving(){ }
    protected virtual void UpdateDead() { }
    #endregion
    public Data.CreatureData CreatureData;
    public virtual int templateID { get;set; }
    public virtual string type { get; set; }    
    public virtual string prefab { get; set; }
    public virtual int level { get; set; }
    public virtual int attack { get; set; } 
    public virtual float speed { get; set; }
    public virtual int maxHp { get; set;}
    public virtual int maxExp { get; set;}
    public int Hp { get; set; }

    public override bool Init()
	{
		base.Init();

		Skills = gameObject.GetOrAddComponent<SkillBook>();

		return true;
	}
    
	public virtual void OnDamaged(BaseController attacker, int damage)
	{
		if (Hp < 0)
			return;

		Hp -= damage;
		if (Hp <= 0)
		{
			Hp = 0;
            MakeDead = true;
			OnDead();
		}
	}

	protected virtual void OnDead()
	{

	}
    
}
