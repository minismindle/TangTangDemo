using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } } 
  
    public void LoadScene(Define.Scene type)
    {
        CurrentScene.Clear();
        SceneManager.LoadScene(GetSceneName(type));
        switch(type) 
        {
            case Define.Scene.LobbyScene:
                break;
            case Define.Scene.GameScene:
                break;
        }
    }
    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);

        return name;
    }
    public void Clear()
    {
        CurrentScene.Clear();
    }
}
