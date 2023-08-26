using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LobbyScene : UI_Base
{
    enum Buttons
    {
        StartButton,
    }
    enum Images
    {
        TitleImage,
    }
    enum Texts
    {
        StartButtonText,
    }
    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));
        BindText(typeof(Texts));
        SetInfo();
        return true;
    }
    public void SetInfo()
    {
        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnClickStartButton);
        GetImage((int)Images.TitleImage).sprite = Managers.Resource.Load<Sprite>(Define.TITLE_ICON);
        GetText((int)Texts.StartButtonText).text = "START";
    }
    public void OnClickStartButton()
    {
        Managers.Clear();
        Managers.Sound.Play(Define.SELECT_SOUND, Define.Sound.Effect);
        Managers.Scene.LoadScene(Define.Scene.GameScene);
    }

}
