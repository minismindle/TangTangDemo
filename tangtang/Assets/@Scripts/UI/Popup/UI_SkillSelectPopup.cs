using Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillSelectPopup : UI_Base
{
    List<UI_SkillCard> _items = new List<UI_SkillCard>();

    enum GameObjects
    {
        SkillCardSelectListObject,
        ExpSliderObject,
    }
    enum Texts
    {
        BeforeLevelValueText,
        AfterLevelValueText,
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
    public void SetInfo(float ratio,int level)
    {
        PopulateGrid();
        SetLevel(ratio,level);
    }
    
    public void PopulateGrid()
    {
        Transform _grid = GetObject((int)GameObjects.SkillCardSelectListObject).transform;
        
        foreach (Transform t in _grid.transform)
            Managers.Resource.Destroy(t.gameObject);
       
        for(int i = 0; i < 4;i++)
        {
            var go = Managers.Resource.Instantiate("UI_SkillCard.prefab", pooling: false);
            UI_SkillCard item = go.GetOrAddComponent<UI_SkillCard>();

            item.SetInfo(i);
            item.transform.SetParent(_grid.transform);

            _items.Add(item);
        }
    }
    public void SetLevel(float ratio, int level)
    {
        GetObject((int)GameObjects.ExpSliderObject).GetComponent<Slider>().value = ratio;
        GetText((int)Texts.BeforeLevelValueText).text = $"Lv.{level - 1}";
        GetText((int)Texts.AfterLevelValueText).text = $"Lv.{level}";
        GetText((int)Texts.CharacterLevelValueText).text = $"{level}";
    }
}
