using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenView : MonoBehaviour
{
    [SerializeField] private LevelView[] levelViews;

    private string _lastFinishedLevelName;

    private void Start()
    {
        _lastFinishedLevelName = DataFinishedLevel.Instance.SceneName;
        if (_lastFinishedLevelName == null || _lastFinishedLevelName == "")
            return;

        LevelView l = levelViews.First(x => x.LevelName == _lastFinishedLevelName);
        l?._stars[0]?.gameObject.SetActive(true);
    }
}
