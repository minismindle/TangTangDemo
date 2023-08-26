using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Data
{
	#region CreatureData
	public class CreatureData
    {
        [XmlAttribute]
        public int templateID;

		[XmlAttribute]
		public string type;

        [XmlAttribute]
        public string prefab;

        [XmlAttribute]
        public int level;

        [XmlAttribute]
        public int attack;

        [XmlAttribute]
        public float speed;

        [XmlAttribute]
        public int maxHp;

        [XmlAttribute]
		public int maxExp;
    }

	[Serializable, XmlRoot("CreatureDatas")]
	public class CreatureDataLoader : ILoader<int, CreatureData>
	{
		[XmlElement("CreatureData")]
		public List<CreatureData> stats = new List<CreatureData>();

		public Dictionary<int, CreatureData> MakeDict()
		{
			Dictionary<int, CreatureData> dict = new Dictionary<int, CreatureData>();
			foreach (CreatureData stat in stats)
				dict.Add(stat.templateID, stat);
			return dict;
		}
	}
	#endregion

	#region SkillData

	[Serializable]
	public class SkillData
	{
		[XmlAttribute]
		public int templateID;

		[XmlAttribute] 
		public string type;

		[XmlAttribute]
		public string skillname;
		
		[XmlAttribute]
		public string prefab;

		[XmlAttribute]
		public int prev;

		[XmlAttribute]
		public int next;

		[XmlAttribute]
		public int level;

		[XmlAttribute]
		public int damage;

		[XmlAttribute]
		public float attackrange;

		[XmlAttribute]
		public float cooltime;

		[XmlAttribute]
		public float lifetime;

		[XmlAttribute]
		public float speed;
	}

	[Serializable, XmlRoot("SkillDatas")]
	public class SkillDataLoader : ILoader<int, SkillData>
	{
		[XmlElement("SkillData")]
		public List<SkillData> skills = new List<SkillData>();

		public Dictionary<int, SkillData> MakeDict()
		{
			Dictionary<int, SkillData> dict = new Dictionary<int, SkillData>();
			foreach (SkillData skill in skills)
				dict.Add(skill.templateID, skill);
			return dict;
		}
	}
	#endregion

	#region StageData
	[Serializable]
	public class StageData
	{
		[XmlAttribute]
		public int stageLevel;

		[XmlAttribute]
		public int prevLevel;

        [XmlAttribute]
        public int nextLevel;

		[XmlAttribute]
		public int updateSec;

        [XmlAttribute]
		public float spawnInterval;

		[XmlAttribute]
		public float maxMonsterCount;

    }
    [Serializable, XmlRoot("StageDatas")]
    public class StageDataLoader : ILoader<int, StageData>
    {
        [XmlElement("StageData")]
        public List<StageData> stats = new List<StageData>();

        public Dictionary<int, StageData> MakeDict()
        {
            Dictionary<int, StageData> dict = new Dictionary<int, StageData>();
            foreach (StageData stat in stats)
                dict.Add(stat.stageLevel, stat);
            return dict;
        }
    }
    #endregion

    #region GemData
    [Serializable]
	public class GemData
	{
		[XmlAttribute]
		public int templateID;
		[XmlAttribute]
		public string prefab;
		[XmlAttribute]
		public int prob;
		[XmlAttribute]
		public int exp;
	}
    [Serializable, XmlRoot("GemDatas")]
    public class GemDataLoader : ILoader<int, GemData>
    {
        [XmlElement("GemData")]
        public List<GemData> stats = new List<GemData>();

        public Dictionary<int, GemData> MakeDict()
        {
            Dictionary<int, GemData> dict = new Dictionary<int, GemData>();
            foreach (GemData stat in stats)
                dict.Add(stat.templateID, stat);
            return dict;
        }
    }
	#endregion

	#region LevelData
	[Serializable]
	public class LevelData
	{
		[XmlAttribute]
		public int level;
		[XmlAttribute]
		public int maxHp;
		[XmlAttribute]
		public int maxExp;
	}
    [Serializable, XmlRoot("LevelDatas")]
    public class LevelDataLoader : ILoader<int, LevelData>
    {
        [XmlElement("LevelData")]
        public List<LevelData> stats = new List<LevelData>();

        public Dictionary<int, LevelData> MakeDict()
        {
            Dictionary<int, LevelData> dict = new Dictionary<int, LevelData>();
            foreach (LevelData stat in stats)
                dict.Add(stat.level, stat);
            return dict;
        }
    }
    #endregion
}