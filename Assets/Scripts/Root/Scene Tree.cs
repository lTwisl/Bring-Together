using UnityEngine;
using static ItemsContainer;


[CreateAssetMenu(fileName = "Scenes Tree", menuName = "Create Scenes Tree File")]
public class ScenesTree : ScriptableObject
{
    [System.Serializable]
    public struct SceneInfo
    {
        public string sceneTitle;
        public string sceneName;
    }
    public SceneInfo[] sceneInfos;

    public SceneInfo this[int index]
    {
        get => sceneInfos[index];
    }

    public int Length => sceneInfos.Length;
}