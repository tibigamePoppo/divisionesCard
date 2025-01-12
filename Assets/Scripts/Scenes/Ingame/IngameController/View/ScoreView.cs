using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _userScore;

    public void Init()
    {
        _userScore.text = Text(0);
    }

    public void UpdateScoreText(int userPoint)
    {
        _userScore.text = Text( userPoint);
    }

    private string Text(int point)
    {
        return $"正解数： {point} 問";
    }
}
