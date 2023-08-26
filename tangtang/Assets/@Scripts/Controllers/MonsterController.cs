using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MonsterController : CreatureController
{
	public override void UpdateAnimation()
	{
        switch (CreatureState)
        {
            case Define.CreatureState.Moving:
                _animator.Play("Walk");
                break;
			case Define.CreatureState.Hit:
                _animator.Play("Hit");
				break;
            case Define.CreatureState.Dead:
                _animator.Play("Dead");
                break;
        }
    }

    protected override void UpdateMoving() 
	{
        if (CreatureState != Define.CreatureState.Moving)
            return;
        PlayerController pc = Managers.Object.Player;
        if (pc == null)
            return;

        Vector3 dir = pc.transform.position - transform.position;
        Vector3 newPos = transform.position + dir.normalized * Time.deltaTime * speed;
        GetComponent<Rigidbody2D>().MovePosition(newPos);
        GetComponent<SpriteRenderer>().flipX = dir.x < 0;
    }
	
	public override bool Init()
	{
		base.Init();
        MakeDead = false;
		_rigid = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
        GetComponent<Collider2D>().isTrigger = false;
        _rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
		ObjectType = Define.ObjectType.Monster;
		CreatureState = Define.CreatureState.Moving;
		return true;
	}
    public override void SetInfo(int templateID)
    {
        CreatureData = Managers.Data.CreatureDic[templateID];
        this.templateID = CreatureData.templateID;
        type = CreatureData.type;
        prefab = CreatureData.prefab;
        level = CreatureData.level;
        attack = CreatureData.attack;
        speed = CreatureData.speed;
        maxHp = CreatureData.maxHp;
        Hp = maxHp;
    }

    void FixedUpdate()
    {
        switch (CreatureState)
        {
            case Define.CreatureState.Moving:
                UpdateMoving();
                break;
        }
    }

    public override void OnDamaged(BaseController attacker, int damage)
    {
        if (CreatureState == Define.CreatureState.Dead)
            return;
        CreatureState = Define.CreatureState.Hit;

        Managers.Sound.Play(Define.HIT_SOUND, Define.Sound.SubBgm,0.5f);

        base.OnDamaged(attacker, damage);
    }

    protected override void OnDead()
    {
        CreatureState = Define.CreatureState.Dead;

        base.OnDead();

        Managers.Game.KillCount++;

        if (_knockback != null)
            StopCoroutine(_knockback);
        _knockback = null;

        Dead(1.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CreatureState == Define.CreatureState.Dead)
            return;
        if(this.IsValid() == false) 
            return;
        if (_knockback != null)
            StopCoroutine(_knockback);
        _knockback = StartCoroutine(CoKnockBack());
       
    }

    #region KnockBack
    Coroutine _knockback;
    IEnumerator CoKnockBack()
    {
        float elapsed = 0;
        while (true)
        {
            elapsed += Time.deltaTime;
            if (elapsed > 0.1f)
                break;

            Vector3 dir = (Managers.Game.Player.transform.position - transform.position) * -1.0f;
            Vector2 nextVec = dir.normalized * 5 * Time.fixedDeltaTime;
            _rigid.MovePosition(_rigid.position + nextVec);

            yield return null;
        }
        CreatureState = Define.CreatureState.Moving;
        yield return new WaitForSeconds(0.5f);
        _knockback = null;
        yield break;
    }
    #endregion

    #region Dead
    Coroutine _coDead;

    void Dead(float waitSeconds)
    {
        if (_coDead != null)
            StopCoroutine(_coDead);
        _coDead = StartCoroutine(CoStartWait(waitSeconds));
    }

    IEnumerator CoStartWait(float waitSeconds)
    {
        GetComponent<Collider2D>().isTrigger = true;
        _rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(waitSeconds);
        Managers.Object.Spawn<GemController>(transform.position);
        Managers.Object.Despawn(this);
        _coDead = null;
    }

    #endregion
}


