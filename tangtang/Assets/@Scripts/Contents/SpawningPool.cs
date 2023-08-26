using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
	Coroutine _coUpdateSpawningPool;
	public Data.StageData StageData { get; set; }
	public virtual int stageLevel { get; set; }
	public virtual int prevLevel { get; set; }
	public virtual int nextLevel { get; set; }
	public virtual int updateSec { get; set; }
	public virtual float spawnInterval { get; set; }	
	public virtual float maxMonsterCount { get; set; }	
	public bool Stopped { get; set; } = false;
	public bool IsUpdated { get; set; } = false;
	void Start()
    {
		if (_coUpdateSpawningPool != null)
			StopCoroutine(CoUpdateSpawningPool());
		_coUpdateSpawningPool = StartCoroutine(CoUpdateSpawningPool());
	}
	public void SetInfo(int stageLevel)
	{
		StageData = Managers.Data.StageDic[stageLevel];
		this.stageLevel = StageData.stageLevel;
		prevLevel = StageData.prevLevel;	
		nextLevel = StageData.nextLevel;	
		updateSec = StageData.updateSec;
		spawnInterval = StageData.spawnInterval;
		maxMonsterCount = StageData.maxMonsterCount;
	}
	IEnumerator CoUpdateSpawningPool()
	{
		while (true)
		{
			TrySpawn();
			yield return new WaitForSeconds(spawnInterval);
		}
	}
    void TrySpawn()
	{
		if (Stopped)
			return;

		int monsterCount = Managers.Object.Monsters.Count;

		if (monsterCount >= maxMonsterCount)
			return;

		Vector3 randPos = Utils.GenerateMonsterSpawnPosition(Managers.Game.Player.transform.position, 10, 20);
		int monsterID = Random.Range(Define.ENEMY0_ID, Define.ENEMY4_ID+1);
		MonsterController mc = Managers.Object.Spawn<MonsterController>(randPos, monsterID);
		mc.SetInfo(monsterID);
	}
	
}
