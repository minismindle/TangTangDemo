using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : RepeatSkill
{
    ParticleSystem _particleSystem;
    Collider2D _collider;

    public override bool Init()
    {
        gameObject.SetActive(false);
        _particleSystem = GetComponent<ParticleSystem>();
        _collider = GetComponent<Collider2D>();
        return true;
    }
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
        damage = SkillData.damage;
        attackrange = SkillData.attackrange;
        lifetime  = SkillData.lifetime;
        cooltime = SkillData.cooltime;
        if (level == 1)
        {
            gameObject.SetActive(true);
            ActivateSkill();
        }
    }
    void SetParticles()
    {
        if (Managers.Game.Player == null)
            return;

        float angle = Random.Range(0,360) * Mathf.Deg2Rad;
        float xDistance = Random.Range(-attackrange, attackrange);
        float yDistance = Random.Range(-attackrange, attackrange);

        float xDist = Mathf.Cos(angle) * xDistance;
        float yDist = Mathf.Sin(angle) * yDistance;

        transform.position = Managers.Game.Player.transform.position + new Vector3(xDist,yDist,0);
        _particleSystem.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterController target = collision.transform.GetComponent<MonsterController>();
        if (target.IsValid() == false)
            return;
        if (target.CreatureState == Define.CreatureState.Dead)
            return;
        target.OnDamaged(Owner, damage);
    }
    protected override void DoSkillJob()
    {

    }
    protected override IEnumerator CoStartSkill()
    {
        while (true)
        {
            _collider.enabled = true;
            SetParticles();
            yield return new WaitForSeconds(lifetime);
            _collider.enabled = false;
            yield return new WaitForSeconds(cooltime);
        }
    }
}

