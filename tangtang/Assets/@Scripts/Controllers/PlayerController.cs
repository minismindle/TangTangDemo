using Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static Define;

public class PlayerController : CreatureController
{
    Vector2 _moveDir = Vector2.zero;
    
    float EnvCollectDist { get; set; } = 1.0f;

	[SerializeField]
	Transform _indicator;
    [SerializeField]
    Transform _fireSocket;

    public Transform Indicator {  get { return _indicator; } }
    public Vector3 FireSocket { get { return _fireSocket.position; } }
    public Vector3 ShootDir { get {  return (_fireSocket.position - _indicator.position).normalized; } }
    public Data.LevelData LevelData { get; set; }

    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set { _moveDir = value.normalized; }
    }

	public override bool Init()
	{
        if (base.Init() == false)
            return false;

        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
            
        SetInfoInit(Define.PLAYER_ID);
        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;
        CreatureState = Define.CreatureState.Idle;
        return true;
	} 

	void OnDestroy()
	{
        if (Managers.Game != null)
	    	Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
	}

	void HandleOnMoveDirChanged(Vector2 dir)
    {
        _moveDir = dir;
    }

    public override void UpdateAnimation()
    {
        switch (CreatureState)
        {
            case Define.CreatureState.Moving:
                _animator.Play("Walk");
                break;
            case Define.CreatureState.Idle:
                _animator.Play("Idle");
                break;
            case Define.CreatureState.Dead:
                _animator.Play("Dead");
                break;
        }
    }

    public override void SetInfo(int templateID)
    {
        LevelData = Managers.Data.LevelDic[templateID];
        level = LevelData.level;
        maxHp = LevelData.maxHp;
        Hp = maxHp;
        maxExp = LevelData.maxExp;  
    }

    public override void SetInfoInit(int templateID)
    {
        CreatureData = Managers.Data.CreatureDic[templateID];
        ObjectType = Define.ObjectType.Player;
        CreatureState = Define.CreatureState.Idle;
        type = CreatureData.type;
        prefab = CreatureData.prefab;
        level = CreatureData.level;
        speed = CreatureData.speed;
        maxHp = CreatureData.maxHp;
        Hp = maxHp;
        maxExp = CreatureData.maxExp;
    }

    void Update()
    {
        if (CreatureState == Define.CreatureState.Dead)
            return;

        MovePlayer();
        CollectEnv();
	}
    
    void MovePlayer()
    {
        if (CreatureState == Define.CreatureState.Dead)
            return;

        Vector3 dir = _moveDir * speed *Time.deltaTime;

        if (dir.x < 0) _sprite.flipX = true;
        else if (dir.x > 0) _sprite.flipX = false;  

        transform.position += dir; 
        
        if (_moveDir != Vector2.zero) 
        {
            _indicator.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-dir.x, dir.y) * 180 / Mathf.PI);
        }

        _rigid.velocity = Vector3.zero;
    }

    void CollectEnv()
    {
        float sqrCollectDist = EnvCollectDist * EnvCollectDist;
       
        var findGems = GameObject.Find("@Grid").GetComponent<GridController>().GatherObjects(transform.position, EnvCollectDist + 0.5f);

		foreach (var go in findGems)
		{
            GemController gem = go.GetComponent<GemController>();

			Vector3 dir = gem.transform.position - transform.position;
			if (dir.sqrMagnitude <= sqrCollectDist)
			{
				Managers.Game.Gem += gem.exp;

				Managers.Object.Despawn(gem);
			}
		}
    }
   
    public override void OnDamaged(BaseController attacker, int damage)
	{
        if (CreatureState == Define.CreatureState.Dead)
            return;

        CreatureState = Define.CreatureState.Hit;

        base.OnDamaged(attacker, damage);
	}
    protected override void OnDead()
    {
        if (CreatureState == Define.CreatureState.Dead)
            return;

        CreatureState = Define.CreatureState.Dead;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        MonsterController attacker = collision.GetComponent<MonsterController>();
        if (attacker.IsValid() == false) 
            return;
        if (this.IsValid() == false) 
            return;
        if (attacker.CreatureState == CreatureState.Dead)
            return;
        if (_coDotDamage != null)
            return;
        _coDotDamage = StartCoroutine(CoStartDotDamage(attacker));
    }

    #region DotDamage
    Coroutine _coDotDamage;
    public IEnumerator CoStartDotDamage(MonsterController attacker)
    {
        OnDamaged(attacker, attacker.attack);
        Managers.UI.GetSceneUI<UI_GameScene>().SetHpBar((float)Hp / maxHp);
        yield return new WaitForSeconds(Define.DOTDAMAGE_TIME);
        _coDotDamage = null;
    }
    #endregion

    
}
