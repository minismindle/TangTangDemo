using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public interface ILoader<Key, Value>
{
	Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
	public Dictionary<int, Data.CreatureData> CreatureDic { get; private set; } = new Dictionary<int, Data.CreatureData>();
	public Dictionary<int, Data.SkillData> SkillDic { get; private set; } = new Dictionary<int, Data.SkillData>();
	public Dictionary<int, Data.GemData> GemDic { get; private set; } = new Dictionary<int, Data.GemData>();
	public Dictionary<int, Data.LevelData> LevelDic { get; private set; } = new Dictionary<int, Data.LevelData>();
	public Dictionary<int, Data.StageData> StageDic { get; private set; } = new Dictionary<int, Data.StageData>();

	public void Init()
	{
		CreatureDic = LoadXml<Data.CreatureDataLoader, int, Data.CreatureData>("CreatureData.xml").MakeDict();
		SkillDic = LoadXml<Data.SkillDataLoader, int, Data.SkillData>("SkillData.xml").MakeDict();
        GemDic = LoadXml<Data.GemDataLoader, int, Data.GemData>("GemData.xml").MakeDict();
        LevelDic = LoadXml<Data.LevelDataLoader, int, Data.LevelData>("LevelData.xml").MakeDict();
        StageDic = LoadXml<Data.StageDataLoader, int, Data.StageData>("StageData.xml").MakeDict();
	}

	Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
	{
		TextAsset textAsset = Managers.Resource.Load<TextAsset>($"{path}");
		return JsonUtility.FromJson<Loader>(textAsset.text);
	}

	Item LoadSingleXml<Item>(string name)
	{
		XmlSerializer xs = new XmlSerializer(typeof(Item));
		TextAsset textAsset = Managers.Resource.Load<TextAsset>(name);
		using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
			return (Item)xs.Deserialize(stream);
	}

	Loader LoadXml<Loader, Key, Item>(string name) where Loader : ILoader<Key, Item>, new()
	{
		XmlSerializer xs = new XmlSerializer(typeof(Loader));
		TextAsset textAsset = Managers.Resource.Load<TextAsset>(name);
		using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
		return (Loader)xs.Deserialize(stream);
	}
}