using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _userScore;

    public void Init()
    {
        _userScore.text = Text("Player", 0);
    }

    public void UpdateScoreText(string user,int userPoint)
    {
        _userScore.text = Text(user, userPoint);
    }

    private string Text(string name,int point)
    {
        return $"{name} のスコアは {point} 点";
    }
}
