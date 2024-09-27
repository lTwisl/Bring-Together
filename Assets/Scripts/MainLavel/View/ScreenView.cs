using TMPro;
using UnityEngine;

public class ScreenView : MonoBehaviour
{
    [field: SerializeField] public TableLevelsView TableLevelsView { get; private set; }
    [SerializeField] private TMP_Text _txtTotalStars;

    public int TotalStars
    {
        get => int.Parse(_txtTotalStars.text);
        set => _txtTotalStars.SetText(value.ToString());
    }
}
