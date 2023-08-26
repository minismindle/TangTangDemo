using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders.Simulation;

public class GemController : BaseController
{
	public Data.GemData Gemdata;
	public virtual int templateID { get; set; }
	public virtual string prefab { get; set; }
	public virtual int prob { get; set; }
	public virtual int exp { get; set; }
	public override bool Init()
	{
		base.Init();
		ChooseGem();
		SetInfo(templateID);
		return true;
	}
	public void ChooseGem()
	{
		int prob = Random.Range(Define.GEM_PROB_MIN, Define.GEM_PROB_MAX+1);

		if (prob <= Managers.Data.GemDic[Define.APPLE_ID].prob)
			templateID = Define.APPLE_ID;
		else if(prob <= Managers.Data.GemDic[Define.ORANGE_ID].prob)
            templateID = Define.ORANGE_ID;
		else if(prob <= Managers.Data.GemDic[Define.BANANAS_ID].prob)
            templateID = Define.BANANAS_ID;
    }
    public override void SetInfo(int templateID)
    {
        Gemdata = Managers.Data.GemDic[templateID];
		prefab = Gemdata.prefab;
		prob = Gemdata.prob;
		exp = Gemdata.exp;
        GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>(prefab);
    }
}
