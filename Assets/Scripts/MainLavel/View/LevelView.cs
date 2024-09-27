using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [SerializeField] private Button _btnOpenLevel;
    [SerializeField] private TMP_Text _stars;

    public Button BtnOpenLevel => _btnOpenLevel;

    public int Stars
    {
        get => int.Parse(_stars.text);
        set => _stars.SetText(value.ToString());
    }
}
