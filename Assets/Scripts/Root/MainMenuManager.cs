using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    //public static MainMenuManager Instance { get; private set; }

    public void Run(DataFinishedLevel data)
    {
        Debug.Log($"You Win!!! Score = {data.Score}");
    }

    public virtual void Awake()
    {
        //Instance = this;
    }

    public void LoadSceneGame(string scene)
    {
        GameManager.Instance.LoadScene(scene);
    }
}
