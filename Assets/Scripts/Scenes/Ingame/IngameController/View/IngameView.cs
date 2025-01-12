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

    public void HidePreviewCard()
    {
        _selectCardView.HideSelectCard();
    }

    public void ShowBattleResultPanel(bool isCollectAnswer)
    {
        _battleResultView.ShowBattleResultPanel(isCollectAnswer);
    }

    public void HideBattleResultPanel()
    {
        _battleResultView.HidePanel();
    }

    public void ChangeActiveGameEndPanel(bool active,string winnerName,int socre)
    {
        if (active)
        {
            _gameEndView.Show(winnerName, socre);
        }
        else
        {
            _gameEndView.Hide();
        }
    }

    public void UpdateScore(int userPoint)
    {
        _scoreView.UpdateScoreText( userPoint);
    }

    public void SetThemeText(DivisionProfileType text)
    {
        switch (text)
        {
            case DivisionProfileType.surfice:
                _themeText.text = "面積 (2022年)";
                break;
            case DivisionProfileType.population:
                _themeText.text = "人口 (2022年)";
                break;
            case DivisionProfileType.temperature:
                _themeText.text = "平均気温 (2022年)";
                break;
            case DivisionProfileType.urban:
                _themeText.text = "村の数 (2022年)";
                break;
            case DivisionProfileType.village:
                _themeText.text = "町の数 (2022年)";
                break;
            case DivisionProfileType.forestSize:
                _themeText.text = "森林面積 (ha) (2019年)";
                break;
            case DivisionProfileType.Hospitals:
                _themeText.text = "病院数 (2022年)";
                break;
            case DivisionProfileType.College:
                _themeText.text = "大学数 (2022年)";
                break;
            default:
                break;
        }
    }
}
