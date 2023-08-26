using Data;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.Notifications.iOS;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillCard : UI_Base
{
    Data.SkillData _skilldata = null;

    enum Images
    {
        SkillImage,
    }
    enum Texts
    {
        SkillLevelText,
        SkillReferenceText,
    }
    enum Buttons
    {
        UI_SkillCardButton,
    }
    private void Awake()
    {
        BindImage(typeof(Images));
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
    }
    public void SetInfo(int skillID)
    {
        switch (skillID)
        {
            case 0:
                _skilldata = Managers.Game.Player.Skills.GetSkillData(Define.FIREBALL_NAME);
                GetImage((int)Images.SkillImage).sprite = Managers.Resource.Load<Sprite>(Define.FIREBALL_ICON);
                GetText((int)Texts.SkillLevelText).text = $"Lv.{_skilldata.level}";
                GetButton((int)Buttons.UI_SkillCardButton).gameObject.BindEvent(OnClickItem);
                break;
            case 1:
                _skilldata = Managers.Game.Player.Skills.GetSkillData(Define.SAW_NAME);
                GetImage((int)Images.SkillImage).sprite = Managers.Resource.Load<Sprite>(Define.SAW_ICON);
                GetText((int)Texts.SkillLevelText).text = $"Lv.{_skilldata.level}";
                GetButton((int)Buttons.UI_SkillCardButton).gameObject.BindEvent(OnClickItem);
                break;
            case 2:
                _skilldata = Managers.Game.Player.Skills.GetSkillData(Define.SWORD_NAME);
                GetImage((int)Images.SkillImage).sprite = Managers.Resource.Load<Sprite>(Define.SWORD_ICON);
                GetText((int)Texts.SkillLevelText).text = $"Lv.{_skilldata.level}";
                GetButton((int)Buttons.UI_SkillCardButton).gameObject.BindEvent(OnClickItem);
                break;
            case 3:
                _skilldata = Managers.Game.Player.Skills.GetSkillData(Define.BOMB_NAME);
                GetImage((int)Images.SkillImage).sprite = Managers.Resource.Load<Sprite>(Define.BOMB_ICON);
                GetText((int)Texts.SkillLevelText).text = $"Lv.{_skilldata.level}";
                GetButton((int)Buttons.UI_SkillCardButton).gameObject.BindEvent(OnClickItem);
                break;
        }
    }

    public void OnClickItem()
    {
        if (_skilldata.level == Define.SKILL_MAX_LEVEL)
            return;
        Managers.Sound.Play(Define.SELECT_SOUND, Define.Sound.Effect);
        Managers.Game.Player.Skills.SetSkillData(_skilldata.skillname,_skilldata.next);
        Managers.UI.ClosePopup();
    }
   
}
