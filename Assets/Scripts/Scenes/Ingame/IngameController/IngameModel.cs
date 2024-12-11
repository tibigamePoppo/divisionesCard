using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Cysharp.Threading.Tasks;
using TMPro.Examples;
using UniRx;
public class IngameModel
{
    private const int PLAYERHANDCOUNT = 5;//?v???C???[?????D??????

    private Subject<Unit> _stageReady = new Subject<Unit>();
    private ReactiveProperty<DivisionProfileType> _updateTheme = new ReactiveProperty<DivisionProfileType>();
    public IObservable<DivisionProfileType> UpdateTheme => _updateTheme;
    public DivisionProfileType CurrentTheme { get { return _updateTheme.Value; } }
    public IObservable<Unit> StageReady => _stageReady;
    private ReactiveProperty<StateType> _state = new ReactiveProperty<StateType>();
    public IObservable<StateType> State => _state;
    public StateType CurrentState { get { return _state.Value; } }
    private List<DivisionData> _deck = new List<DivisionData>();
    private List<DivisionData> _playerHnad = new List<DivisionData>();
    public List<DivisionData> deck { get { return _deck; } }
    public List<DivisionData> playerDeck { get { return _playerHnad; } }

    public DivisionData PlayerSelectCard { get => _playerSelectCard; }
    public string WinnerName { get => _winnerName; }

    private DivisionData _playerSelectCard;
    private DivisionData _enemySelectCard;
    public DivisionData EnemySelectCardData { get => _enemySelectCard; }
    private String _winnerName;

    private int _playerScore = 0;
    private int _enemyScore = 0;
    public int CurrentPlayerScore { get => _playerScore; }
    public int CurrentEnemyScore { get => _enemyScore; }

    public void Init()
    {
        CreateDeck();
        _state.Value = StateType.PlayerReady;

        State.Where(state => state == StateType.StageReady)
            .Subscribe(_ => _state.Value = StateType.PlayerReady);

        State.Where(state => state == StateType.PlayerReady)
            .Subscribe(_ => PlaeyrHandBuild());

        State.Where(state => state == StateType.Draw)
            .Subscribe(_ => PlayerDraw());

        State.Where(state => state == StateType.EnemyCardSelect)
            .Subscribe(_ => EnemySelectCard());

        State.Where(state => state == StateType.Battle)
            .Subscribe(_ => Battle().Forget());

        //UpdateThemeValue();
    }

    private void CreateDeck()
    {
        _deck = MasterDataReader.Instance.Master.DivisionData.ToList();
        _state.Value = StateType.StageReady;
    }

    private void PlaeyrHandBuild()
    {
        for (int i = 0; i < PLAYERHANDCOUNT; i++)
        {
            var card = _deck[UnityEngine.Random.Range(0, _deck.Count)];
            _playerHnad.Add(card);
            _deck.Remove(card);
        }
        _state.Value = StateType.Draw;
    }

    private void PlayerDraw()
    {
        if(_playerHnad.Count < PLAYERHANDCOUNT)
        {
            var card = _deck[UnityEngine.Random.Range(0, _deck.Count)];
            _playerHnad.Add(card);
            _deck.Remove(card);
        }
        _state.Value = StateType.CardSelect;
    }

    /// <summary>
    /// ?v???C???[?????D??????
    /// </summary>
    /// <param name="type"></param>
    public void PlayerHandSet(DivisionData type)
    {
        _playerHnad.Add(type);
        _deck.Remove(type);
    }

    public void SedPlayerSelectCard(DivisionData data)
    {
        _playerSelectCard = data;
        _state.Value = StateType.EnemyCardSelect;
    }

    public void EnemySelectCard()
    {
        _enemySelectCard = _deck[UnityEngine.Random.Range(0, _deck.Count)];
        _playerHnad.Remove(_playerSelectCard);
        _deck.Remove(_enemySelectCard);
        UnityEngine.Debug.Log($"EnemyCard is {_enemySelectCard.name}");
        _state.Value = StateType.Battle;
    }

    public async UniTaskVoid Battle()
    {

        //TODO:????????????????????????
        UnityEngine.Debug.Log($"current theme is {CurrentTheme}");
        UnityEngine.Debug.Log($"Player : {ValueByData(_playerSelectCard)}, enemy : {ValueByData(_enemySelectCard)}");
        if (ValueByData(_playerSelectCard) > ValueByData(_enemySelectCard))
        {
            _winnerName = "Player";
            _playerScore++;
            UnityEngine.Debug.Log("PlayerWin");
        }
        else if (ValueByData(_playerSelectCard) < ValueByData(_enemySelectCard))
        {
            _winnerName = "Enemy";
            _enemyScore++;
            UnityEngine.Debug.Log("EnemyWin");
        }
        else
        {
            _winnerName = "None";
            UnityEngine.Debug.Log("Draw");
        }

        await UniTask.Delay(TimeSpan.FromSeconds(1));

        if (deck.Count < 2)
        {
            UnityEngine.Debug.Log("GameEnd");
            _state.Value = StateType.GameEnd;
        }
        else
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            UpdateThemeValue();
            _state.Value = StateType.Draw;
        }
    }

    private float ValueByData(DivisionData data)
    {
        switch (_updateTheme.Value)
        {
            case DivisionProfileType.surfice:
                return data.surfaceSize;
            case DivisionProfileType.population:
                return data.population;
            case DivisionProfileType.temperature:
                return data.temperature;
            case DivisionProfileType.urban:
                return data.urban;
            case DivisionProfileType.village:
                return data.village;
            case DivisionProfileType.forestSize:
                return data.forestSize;
            case DivisionProfileType.Hospitals:
                return data.Hospitals;
            case DivisionProfileType.College:
                return data.College;
            default:
                return 0;
        }
    }

    private void UpdateThemeValue()
    {
        int maxCount = Enum.GetNames(typeof(DivisionProfileType)).Length;
        int number = UnityEngine.Random.Range(0, maxCount);
        _updateTheme.Value = (DivisionProfileType)Enum.ToObject(typeof(DivisionProfileType), number);
    }
}
