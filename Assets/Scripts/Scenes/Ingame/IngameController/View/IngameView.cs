using Cysharp.Threading.Tasks;
using Scenes.Ingame.Card;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using System;

public class IngameView : MonoBehaviour
{
    [SerializeField] private Transform _playerHnadArea;//手札を生成する位置
    [SerializeField] private Button _readyButton;
    [SerializeField] private CardView _cardPrefab;
    [SerializeField] private SelectCardView _selectCardView;
    private Subject<DivisionData> _selectedCard = new Subject<DivisionData>();
    public IObservable<DivisionData> SelectedCard => _selectedCard;
    private bool _selected = false;
    private DivisionData _selectCardData = null;
    public void Init()
    {
        _selectCardView.Init();

        _readyButton.interactable = false;
        _readyButton.OnClickAsObservable()
            .Where(_ => _selectCardData != null)
            .Subscribe(_ =>
            {
                _selectedCard.OnNext(_selectCardData);
            }).AddTo(this);
    }

    /// <summary>
    /// 手札の生成
    /// </summary>
    public void CreateHand(DivisionData divisionData)
    {
        var card = Instantiate(_cardPrefab, _playerHnadArea.position, Quaternion.identity, _playerHnadArea);
        card.Init(divisionData);
        card.OnClick
            .Subscribe(data => SelectCard(data)).AddTo(this);
    }

    public void SelectCard(DivisionData divisionData)
    {
        _selectCardView.ShowSelectCard(divisionData);
        _selectCardData = divisionData;
        _readyButton.interactable = true;
    }
}
