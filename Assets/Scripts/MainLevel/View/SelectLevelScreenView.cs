using TMPro;
using UnityEngine;


public class SelectLevelScreenView : MonoBehaviour
{
    [SerializeField] private TableLevelsView _tableLevelsView;
    [SerializeField] private TMP_Text _textTotalStars;

    public TableLevelsView TableLevelsView => _tableLevelsView;
    public int TotalStars
    {
        get => int.Parse(_textTotalStars.text);
        set => _textTotalStars.SetText(value.ToString());
    }
}
