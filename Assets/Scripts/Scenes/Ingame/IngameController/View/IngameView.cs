using Cysharp.Threading.Tasks;
using Scenes.Ingame.Card;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

public class IngameView : MonoBehaviour
{
    [SerializeField] private Transform _playerHnadArea;//???D?????????????u
    [SerializeField] private Button _readyButton;
    [SerializeField] private CardView _cardPrefab;
    [SerializeField] private SelectCardView _selectCardView;
    [SerializeField] private SelectCardView _enemyCardView;
    [SerializeField] private BattleResultView _battleResultView;
    [SerializeField] private GameEndView _gameEndView;
    [SerializeField] private InformationPanelView _informationPanelView;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private TextMeshProUGUI _themeText;

    private List<CardView> _playerHands = new List<CardView>();
    private DivisionProfileType _theme;
    private Subject<DivisionData> _selectedCard = new Subject<DivisionData>();
    public IObservable<DivisionData> SelectedCard => _selectedCard;
    private bool _selected = false;
    private DivisionData _selectCardData = null;
    public void Init()
    {
        _selectCardView.Init();
        _enemyCardView.Init();
        _gameEndView.Init();
        _battleResultView.Init();
        _informationPanelView.Init();
        _scoreView.Init();
        _readyButton.interactable = false;
        _readyButton.OnClickAsObservable()
            .Where(_ => _selectCardData != null)
            .Subscribe(_ =>
            {
                _selectedCard.OnNext(_selectCardData);
            }).AddTo(this);
    }

    public void SetTheme(DivisionProfileType type)
    {
        _theme = type;
    }

    /// <summary>
    /// ???D??????
    /// </summary>
    public void CreateHand(DivisionData divisionData)
    {
        if (_playerHands.Select(v => v.Data).Contains(divisionData)) return;
        //Debug.Log($"Create card is {divisionData.name}");
        var card = Instantiate(_cardPrefab, _playerHnadArea.position, Quaternion.identity, _playerHnadArea);
        _playerHands.Add(card);
        card.Init(divisionData, _theme);
        card.SetScale(true);
        card.OnClick
            .Subscribe(data => SelectCard(data)).AddTo(this);
        card.OnPointerOver
            .Subscribe(data =>
            {
                if(data != null)
                {
                    _informationPanelView.ShowPanel(data);
                }
                else
                {
                    _informationPanelView.HidePanel();
                }
            }).AddTo(this);
    }

    public void SelectCard(DivisionData divisionData)
    {
        _selectCardView.ShowSelectCard(divisionData,_theme);
        _selectCardData = divisionData;
        ReadyButtonInteractable(true);
    }

    public void ReadyButtonInteractable(bool value)
    {
        _readyButton.interactable = value;
    }

    public void RemovePlayerHand(DivisionData[] divisionDatas)
    {
        var removeCard = _playerHands.Select(v => v.Data).Except(divisionDatas).ToArray();
        foreach (var card in removeCard)
        {
            var target = _playerHands.FirstOrDefault(v => v.Data == card).gameObject;
            Destroy(target);
            _playerHands.Remove(_playerHands.FirstOrDefault(v => v.Data == card));
        }
    }

    public void UpdatePlayerHand()
    {
        foreach (var card in _playerHands)
        {
            card.UpdateValue(_theme);
        }
    }

    public void EnemyCardSet(DivisionData data)
    {
        _enemyCardView.ShowSelectCard(data,_theme);
    }

    public void EnemyCardHide()
    {
        _enemyCardView.HideSelectCard();
    }
    public void HidePreviewCard()
    {
        _selectCardView.HideSelectCard();
    }

    public void ShowBattleResultPanel(DivisionData player,DivisionData enemy,string winnerName)
    {
        _battleResultView.ShowBattleResultPanel(player, enemy, winnerName, _theme);
    }

    public void HideBattleResultPanel()
    {
        _battleResultView.HidePanel();
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

    public void UpdateScore(string user1, int user1point, string user2, int user2point)
    {
        _scoreView.UpdateScoreText(user1, user1point, user2, user2point);
    }

    public void SetThemeText(DivisionProfileType text)
    {
        switch (text)
        {
            case DivisionProfileType.surfice:
                _themeText.text = "????(2022)";
                break;
            case DivisionProfileType.population:
                _themeText.text = "?l??(2022)";
                break;
            case DivisionProfileType.temperature:
                _themeText.text = "?????C??(2022)";
                break;
            case DivisionProfileType.urban:
                _themeText.text = "?s????(2022)";
                break;
            case DivisionProfileType.village:
                _themeText.text = "??????(2022)";
                break;
            case DivisionProfileType.forestSize:
                _themeText.text = "?X??????(ha)(2019)";
                break;
            case DivisionProfileType.Hospitals:
                _themeText.text = "?a?@??(2022)";
                break;
            case DivisionProfileType.College:
                _themeText.text = "???w??(2022)";
                break;
            default:
                break;
        }
    }
}
