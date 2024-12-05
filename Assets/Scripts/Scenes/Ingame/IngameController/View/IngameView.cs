using Cysharp.Threading.Tasks;
using Scenes.Ingame.Card;
using System.Collections;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

public class IngameView : MonoBehaviour
{
    [SerializeField] private Transform _playerHnadArea;//éËéDÇê∂ê¨Ç∑ÇÈà íu
    [SerializeField] private Button _readyButton;
    [SerializeField] private CardView _cardPrefab;
    [SerializeField] private SelectCardView _selectCardView;
    [SerializeField] private GameEndView _gameEndView;
    [SerializeField] private TextMeshProUGUI _themeText;
    private List<CardView> _playerHands = new List<CardView>();
    private Subject<DivisionData> _selectedCard = new Subject<DivisionData>();
    public IObservable<DivisionData> SelectedCard => _selectedCard;
    private bool _selected = false;
    private DivisionData _selectCardData = null;
    public void Init()
    {
        _selectCardView.Init();
        _gameEndView.Init();
        _readyButton.interactable = false;
        _readyButton.OnClickAsObservable()
            .Where(_ => _selectCardData != null)
            .Subscribe(_ =>
            {
                _selectedCard.OnNext(_selectCardData);
            }).AddTo(this);
    }

    /// <summary>
    /// éËéDÇÃê∂ê¨
    /// </summary>
    public void CreateHand(DivisionData divisionData)
    {
        if (_playerHands.Select(v => v.Data).Contains(divisionData)) return;
        Debug.Log($"Create card is {divisionData.name}");
        var card = Instantiate(_cardPrefab, _playerHnadArea.position, Quaternion.identity, _playerHnadArea);
        _playerHands.Add(card);
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

    public void UpdatePlayerHand(DivisionData[] divisionDatas)
    {
        var removeCard = _playerHands.Select(v => v.Data).Except(divisionDatas).ToArray();
        foreach (var card in removeCard)
        {
            var target = _playerHands.FirstOrDefault(v => v.Data == card).gameObject;
            Destroy(target);
            _playerHands.Remove(_playerHands.FirstOrDefault(v => v.Data == card));
        }
    }

    public void HidePreviewCard()
    {
        _selectCardView.HideSelectCard();
    }

    public void ChangeActiveGameEndPanel(bool active,string winnerName)
    {
        if (active)
        {
            _gameEndView.Show(winnerName);
        }
        else
        {
            _gameEndView.Hide();
        }
    }

    public void SetThemeText(DivisionProfileType text)
    {
        switch (text)
        {
            case DivisionProfileType.surfice:
                _themeText.text = "ñ êœ(2022)";
                break;
            case DivisionProfileType.population:
                _themeText.text = "êlå˚(2022)";
                break;
            case DivisionProfileType.temperature:
                _themeText.text = "ïΩãœãCâ∑(2022)";
                break;
            case DivisionProfileType.urban:
                _themeText.text = "ésãÊêî(2022)";
                break;
            case DivisionProfileType.village:
                _themeText.text = "í¨ë∫êî(2022)";
                break;
            case DivisionProfileType.forestSize:
                _themeText.text = "êXó—ñ êœ(ha)(2019)";
                break;
            case DivisionProfileType.Hospitals:
                _themeText.text = "ïaâ@êî(2022)";
                break;
            case DivisionProfileType.College:
                _themeText.text = "ëÂäwêî(2022)";
                break;
            default:
                break;
        }
    }
}
