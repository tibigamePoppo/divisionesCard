using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _user1Score;
    [SerializeField] private TextMeshProUGUI _user2Score;

    public void Init()
    {
        _user1Score.text = Text("Player", 0);
        _user2Score.text = Text("Enemy", 0);
    }

    public void UpdateScoreText(string user1,int user1point, string user2, int user2point)
    {
        _user1Score.text = Text(user1, user1point);
        _user2Score.text = Text(user2, user2point);
    }

    private string Text(string name,int point)
    {
        return $"{name} のスコアは {point} 点";
    }
}
