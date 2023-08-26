using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : RepeatSkill
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
        lifetime = SkillData.lifetime;
        cooltime = SkillData.cooltime;
        if (level == 1)
        {
            gameObject.SetActive(true);
            ActivateSkill();
        }
    }
    private void FixedUpdate()
    {
        SetParticles();
    }
    void SetParticles()
    {
        if (Managers.Game.Player == null)
            return;
        transform.localEulerAngles = Managers.Game.Player.Indicator.transform.eulerAngles;
        transform.position = Managers.Game.Player.transform.position;

        float radian = Mathf.Deg2Rad * transform.localEulerAngles.z * -1;

        var main = _particleSystem.main;
        main.startRotation = radian;
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
            _particleSystem.Play();
            yield return new WaitForSeconds(lifetime);
            _collider.enabled = false;
            yield return new WaitForSeconds(cooltime);
        }
    }
}
