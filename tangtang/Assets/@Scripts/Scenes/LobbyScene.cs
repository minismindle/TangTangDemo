using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class LobbyScene : BaseScene
{
    void Start()
    {
        Init();
    }
    void SetInfo()
    {
        Managers.UI.ShowSceneUI<UI_LobbyScene>();
        Managers.Sound.Play(Define.BGM_SOUND,Define.Sound.Bgm);
        SceneType = Define.Scene.LobbyScene;

        var eventsystem = Managers.Resource.Instantiate(Define.EVENTSYSTEM_PREFAB);
        eventsystem.name = "@EventSystem";

    }
    bool _init = false;
    protected override void Init()
    {
        if(_init == false)
        {
            Managers.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
            {
                Debug.Log($"{key} {count}/{totalCount}");

                if (count == totalCount)
                {
                    SetInfo();
                }
            });
            _init = true;
            return;
        }
        else if(_init == true) 
        {
            SetInfo();
            return;
        }
    }
    public override void Clear()
    {

    }
}
