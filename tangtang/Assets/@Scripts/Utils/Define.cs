using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define 
{
	public enum UIEvent
	{
		Click,
		Pressed,
		PointerDown,
		PointerUp,
		BeginDrag,
		Drag,
		EndDrag,
	}

	public enum Scene
	{
		Unknown,
		LobbyScene,
		GameScene,
	}

	public enum Sound
	{
		Bgm,
		SubBgm,
		Effect,
		MaxCount
	}

	public enum ObjectType
	{
		Player,
		Monster,
		Projectile,
		Env
	}

	public enum SkillType
	{
		None,
		Sequence,
		Repeat,
	}

	public enum StageType
	{
		Normal,
	}

	public enum CreatureState
	{
		Idle,
		Moving,
		Skill,
		Hit,
		Dead
	}
    #region player
    public const int PLAYER_ID = 1;
	public const int WIN_TIME = 600;
	public const float DOTDAMAGE_TIME = 0.2f;
	public const int PLAYER_MIN_LEVEL = 1;
	public const int PLAYER_MAX_LEVEL = 20;
    public const int STAGE_MIN_LEVEL = 1;
    public const int STAGE_MAX_LEVEL = 10;
	public const int SKILL_MIN_LEVEL = 1;
    public const int SKILL_MAX_LEVEL = 5;
    #endregion

    #region enemy
    public const int ENEMY0_ID = 10;
	public const int ENEMY1_ID = 11;
	public const int ENEMY2_ID = 12;
	public const int ENEMY3_ID = 13;
	public const int ENEMY4_ID = 14;
	#endregion

	#region skillname
	public const string FIREBALL_NAME = "fireball";
	public const string SAW_NAME = "saw";
	public const string SWORD_NAME = "sword";
	public const string BOMB_NAME = "bomb";
	#endregion

	#region gem
	public const int GEM_PROB_MIN = 0;
	public const int GEM_PROB_MAX = 60;
    public const int APPLE_ID = 30;
	public const int ORANGE_ID = 31;
	public const int BANANAS_ID = 32;
    #endregion

    #region prefab
    public const string EXP_GEM_PREFAB = "EXPGem.prefab";
	public const string BULLET_PREFAB = "Bullet.prefab";
	public const string MAP_PREFAB = "Map_01.prefab";
	public const string JOYSTICK_PREFAB = "UI_Joystick.prefab";
	public const string EVENTSYSTEM_PREFAB = "EventSystem.prefab";
	public const string SKILLSELECT_PREFAB = "UI_SkillSelectPopup.prefab";
	public const string SKILLCARD_PREFAB = "UI_SkillCard.prefab";
	public const string GAMERESULT_PREFAB = "UI_GameResultPopup.prefab";
    #endregion 

    #region sound
    public const string WIN_SOUND = "Win.wav";
	public const string LOSE_SOUND = "Lose.wav";
	public const string LEVELUP_SOUND = "LevelUp.wav";
	public const string SELECT_SOUND = "Select.wav";
	public const string DEAD_SOUND = "Dead.wav";
	public const string HIT_SOUND = "Hit0.wav";
	public const string MELEE_SOUND = "Melee0.wav";
	public const string BGM_SOUND = "BGM.wav";
	#endregion

	#region sprite
	public const string FIREBALL_ICON = "Fireballicon.asset";
	public const string SAW_ICON = "Sawicon.asset";
	public const string SWORD_ICON = "Swordicon.asset";
	public const string BOMB_ICON = "Bombicon.asset";
    public const string TITLE_ICON = "Titleicon.asset";
	public const string SURVIVE_ICON = "Surviveicon.asset";
	public const string DEAD_ICON = "Deadicon.asset";
	#endregion

	#region skill
	public const int MAX_SAW_SIZE = 6;
    public const int FIREBALL_ID_LEVEL0 = 200;
    public const int FIREBALL_ID_LEVEL1 = 201;
    public const int FIREBALL_ID_LEVEL2 = 202;
    public const int FIREBALL_ID_LEVEL3 = 203;
    public const int FIREBALL_ID_LEVEL4 = 204;
    public const int FIREBALL_ID_LEVEL5 = 205;

	public const int SAW_ID_LEVEL0 = 210;
	public const int SAW_ID_LEVEL1 = 211;
	public const int SAW_ID_LEVEL2 = 212;
	public const int SAW_ID_LEVEL3 = 213;
	public const int SAW_ID_LEVEL4 = 214;
	public const int SAW_ID_LEVEL5 = 215;

	public const int SWORD_ID_LEVEL0 = 220;
    public const int SWORD_ID_LEVEL1 = 221;
    public const int SWORD_ID_LEVEL2 = 222;
    public const int SWORD_ID_LEVEL3 = 223;
    public const int SWORD_ID_LEVEL4 = 224;
    public const int SWORD_ID_LEVEL5 = 225;

	public const int BOMB_ID_LEVEL0 = 230;
    public const int BOMB_ID_LEVEL1 = 231;
    public const int BOMB_ID_LEVEL2 = 232;
    public const int BOMB_ID_LEVEL3 = 233;
    public const int BOMB_ID_LEVEL4 = 234;
    public const int BOMB_ID_LEVEL5 = 235;
	
    #endregion
}
