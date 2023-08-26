using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Saw : RepeatSkill
{
    List<Saw> _saws = new List<Saw>();
    int SawSize = Define.MAX_SAW_SIZE;

    float deg = 0;
    
    public override void UpdateController()
    {
        DoSkillJob();
    }
    public override bool Init()
    {
        for(int i = 0; i < SawSize; i++)
            transform.GetChild(i).gameObject.SetActive(false);
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
        speed = SkillData.speed;
        if (level == 1)
        {
            for (int i = 0; i < SawSize; i++)
                transform.GetChild(i).gameObject.SetActive(true);
        }
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
        transform.position = Managers.Object.Player.transform.position;
        deg += Time.deltaTime * speed;
        if (deg < 360)
        {
            for (int i = 0; i < SawSize; i++)
            {
                var rad = Mathf.Deg2Rad * (deg + (i * (360 / SawSize)));
                var x = attackrange * Mathf.Sin(rad);
                var y = attackrange * Mathf.Cos(rad);
                transform.GetChild(i).gameObject.transform.position = transform.position + new Vector3(x, y);
                transform.GetChild(i).gameObject.transform.rotation = Quaternion.Euler(0, 0, (deg + (i * (360 / SawSize))) * -1);
            }
        }
        else
        {
            deg = 0;
        }
    }
}
