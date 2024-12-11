using Scenes.Ingame.Card;
using UnityEngine;
using TMPro;
public class BattleResultView : MonoBehaviour
{
    [SerializeField] CardView _playerCardView;
    [SerializeField] CardView _enemyCardView;
    [SerializeField] TextMeshProUGUI _winnerText;
    public void Init()
    {
        gameObject.SetActive(false);
    }

    public void ShowBattleResultPanel(DivisionData player, DivisionData enemy, string winnerName,DivisionProfileType type)
    {
        gameObject.SetActive(true);
        _playerCardView.Init(player, type);
        _enemyCardView.Init(enemy, type);
        _winnerText.text = winnerName;
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
