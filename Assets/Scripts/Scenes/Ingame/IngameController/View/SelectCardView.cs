using Scenes.Ingame.Card;
using UnityEngine;

public class SelectCardView : MonoBehaviour
{
    [SerializeField]
    private CardView _cardView;
    public void Init()
    {
        _cardView.gameObject.SetActive(false);
    }

    public void ShowSelectCard(DivisionData division)
    {
        _cardView.Init(division);
        _cardView.gameObject.SetActive(true);
    }
    public void HideSelectCard()
    {
        _cardView.gameObject.SetActive(false);
    }
}
