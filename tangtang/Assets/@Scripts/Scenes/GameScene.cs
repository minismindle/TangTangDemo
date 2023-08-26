using Data;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class GameScene : BaseScene
{
    void Start()
    {
        Managers.Data.Init();

        this.Init();

        SceneType = Define.Scene.GameScene;

        Managers.UI.ShowSceneUI<UI_GameScene>();

        _spawningPool = gameObject.AddComponent<SpawningPool>();
        _spawningPool.SetInfo(Define.STAGE_MIN_LEVEL);
        _timer = gameObject.AddComponent<Timer>();
        _timer.StartTimer();

        var player = Managers.Object.Spawn<PlayerController>(Vector3.zero);

        var joystick = Managers.Resource.Instantiate(Define.JOYSTICK_PREFAB);
        joystick.name = "@UI_Joystick";

        var eventsystem = Managers.Resource.Instantiate(Define.EVENTSYSTEM_PREFAB);
        eventsystem.name = "@EventSystem";

        var map = Managers.Resource.Instantiate(Define.MAP_PREFAB);
        map.name = "@Map";

        Camera.main.GetComponent<CameraController>().Target = player.gameObject;

        Managers.Game.Player.Skills.AddSkill<Saw>(Managers.Game.Player.transform.position, Define.SAW_NAME);
        Managers.Game.Player.Skills.AddSkill<FireballSkill>(Managers.Game.Player.transform.position, Define.FIREBALL_NAME);
        Managers.Game.Player.Skills.AddSkill<Sword>(Managers.Game.Player.transform.position, Define.SWORD_NAME);
        Managers.Game.Player.Skills.AddSkill<Bomb>(Managers.Game.Player.transform.position, Define.BOMB_NAME);

        Managers.Game.OnKillCountChanged -= HandleOnKillCountChanged;
        Managers.Game.OnKillCountChanged += HandleOnKillCountChanged;
        Managers.Game.OnGemCountChanged -= HandleOnGemCountChanged;
        Managers.Game.OnGemCountChanged += HandleOnGemCountChanged;

        SetInitSkill();
    }

    SpawningPool _spawningPool;
    Timer _timer;
    Define.StageType _stageType;
    public Define.StageType StageType
    {
        get { return _stageType; }
        set
        {
            _stageType = value;

            if (_spawningPool != null)
            {
                switch (value)
                {
                    case Define.StageType.Normal:
                        _spawningPool.Stopped = false;
                        break;
                }
            }
        }
    }
    string SetTimeText(int time)
    {
        string timeText = "";
        if (time < 10)
        {
            timeText = $"00:0{time}";
        }
        else if (time < 60)
        {
            timeText = $"00:{time}";
        }
        else if (time < 600)
        {
            if(time % 60 < 10)
                timeText = $"0{time / 60}:0{time % 60}";
            else
                timeText = $"0{time / 60}:{time % 60}";
        }
        else if (time == Define.WIN_TIME)
        {
            timeText = "10:00";
        }
        return timeText;
    }
    protected override void Init()
    {
        Managers.Game.Gem = 0;
        Managers.Game.KillCount = 0;
    }
    public override void Clear()
    {

    }
    private void FixedUpdate()
    {
        Updatetimer();
        CheckSurvive();
    }
    public void SetInitSkill()
    {
        Managers.UI.ShowPopup<UI_SkillSelectPopup>().SetInfo(0, Managers.Game.Player.level);
    }
    public void Updatetimer()
    {
        if (_timer == null)
            return;
        Managers.UI.GetSceneUI<UI_GameScene>().SetTimeInfo(SetTimeText(_timer.Time));
        UpdateStage(_timer.Time);
    }
    public void UpdateStage(int time)
    {

        if (_spawningPool.stageLevel == Define.STAGE_MAX_LEVEL)
            return;

        if (_spawningPool.updateSec == time)
        {
            if (_spawningPool.IsUpdated == true)
                return;
            _spawningPool.SetInfo(_spawningPool.nextLevel);
            _spawningPool.IsUpdated = true;
        }

        if (_spawningPool.IsUpdated == false)
            return;

        if (_spawningPool.updateSec != time)
        {
            if (_spawningPool.IsUpdated == true)
                _spawningPool.IsUpdated = false;
        }
    }
    public void CheckSurvive()
    {
        if (Managers.Game.Player.CreatureState == Define.CreatureState.Dead)
        {
            Managers.UI.ShowPopup<UI_GameResultPopup>().SetInfo(_spawningPool.stageLevel, SetTimeText(_timer.Time),false);
            Managers.Sound.Play(Define.LOSE_SOUND, Define.Sound.Effect);
        }
        else if(_timer.Time == Define.WIN_TIME)
        {
            Managers.UI.ShowPopup<UI_GameResultPopup>().SetInfo(_spawningPool.stageLevel, SetTimeText(_timer.Time),true);
            Managers.Sound.Play(Define.WIN_SOUND, Define.Sound.Effect);
        }
    }
    public void HandleOnGemCountChanged(int exp)
	{
		float expratio = 0;

		if (Managers.Game.Player.level == Define.PLAYER_MAX_LEVEL)
			return;

		if (Managers.Game.Gem >= Managers.Game.Player.maxExp)
		{
            Managers.Sound.Play(Define.LEVELUP_SOUND, Define.Sound.Effect);
            Managers.Game.Gem -= Managers.Game.Player.maxExp;
            Managers.Game.Player.SetInfo(++Managers.Game.Player.level);
            Managers.UI.GetSceneUI<UI_GameScene>().SetHpBar((float)Managers.Game.Player.Hp / Managers.Game.Player.maxHp);
            expratio = (float)Managers.Game.Gem / Managers.Game.Player.maxExp;
            Managers.UI.ShowPopup<UI_SkillSelectPopup>().SetInfo(expratio,Managers.Game.Player.level);
        }

        expratio = (float)Managers.Game.Gem / Managers.Game.Player.maxExp;
        Managers.UI.GetSceneUI<UI_GameScene>().SetGemCountRatio(expratio);
    }

    public void HandleOnKillCountChanged(int killCount)
	{
		Managers.UI.GetSceneUI<UI_GameScene>().SetKillCount(killCount);
	}

    private void OnDestroy()
    {
        if (Managers.Game != null)
        {
            Managers.Game.OnGemCountChanged -= HandleOnGemCountChanged;
            Managers.Game.OnKillCountChanged -= HandleOnKillCountChanged;
        }
    }
}
