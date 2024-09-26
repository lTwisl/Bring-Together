using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DataFinishedLevel : MonoBehaviour
{
    public static DataFinishedLevel Instance;


    public string SceneName { get; set; }
    public int Score {  get; set; }

    private void Awake()
    {
        /*DontDestroyOnLoad(gameObject);*/

        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
}
