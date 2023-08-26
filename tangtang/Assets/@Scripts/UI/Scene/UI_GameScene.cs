using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UI_GameScene : UI_Base
{
    enum GameObjects
    {
        ExpSliderObject,
        HpSliderObject,
    }
    enum Texts
    {
        WaveValueText,
        TimeLimitValueText,
        KillValueText,
        CharacterLevelValueText,
    }
    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));
        return true;
    }

    public void SetGemCountRatio(float ratio)
    {
        GetObject((int)GameObjects.ExpSliderObject).GetComponent<Slider>().value = ratio;
    }

    public void SetKillCount(int killCount)
    {
        GetText((int)Texts.KillValueText).text = $"{killCount}";
    }

    public void SetHpBar(float ratio)
    {
        GetObject((int)GameObjects.HpSliderObject).GetComponent<Slider>().value = ratio;
    }

    public void SetStageInfo(int stage)
    {
        GetText((int)Texts.WaveValueText).text = $"{stage}"; 
    }

    public void SetTimeInfo(string time)
    {
        GetText((int)Texts.TimeLimitValueText).text = time;
    }
}
