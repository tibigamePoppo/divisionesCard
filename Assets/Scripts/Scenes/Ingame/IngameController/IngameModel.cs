using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx;
public class IngameModel
{
    private const int PLAYERHANDCOUNT = 5;

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
    public List<DivisionData> playerHand { get { return _playerHnad; } }

    public DivisionData PlayerSelectCard { get => _playerSelectCard; }
    public bool IsCollectAnswer { get => _isCollectAnswer; }

    private DivisionData _playerSelectCard;
    private bool _isCollectAnswer;

    private int _playerScore = 0;
    public int CurrentPlayerScore { get => _playerScore; }

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

        State.Where(state => state == StateType.Battle)
            .Subscribe(_ => Battle().Forget());
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
            PlayerHandSet();
        }
        _state.Value = StateType.Draw;
    }

    private void PlayerDraw()
    {
        if(_playerHnad.Count < PLAYERHANDCOUNT)
        {
            PlayerHandSet();
        }
        _state.Value = StateType.CardSelect;
    }

    /// <summary>
    /// ?v???C???[?????D??????
    /// </summary>
    /// <param name="type"></param>
    public void PlayerHandSet()
    {
        var card = _deck[UnityEngine.Random.Range(0, _deck.Count)];
        _playerHnad.Add(card);
        _deck.Remove(card);
    }

    public void SetPlayerSelectCard(DivisionData data)
    {
        _playerSelectCard = data;
        _playerHnad.Remove(_playerSelectCard);
        _state.Value = StateType.Battle;
    }

    public async UniTaskVoid Battle()
    {

        //TODO:????????????????????????
        UnityEngine.Debug.Log($"current theme is {CurrentTheme}");
        UnityEngine.Debug.Log($"player hand count  is {_playerHnad.Count}");
        UnityEngine.Debug.Log($"Player : {ValueByData(_playerSelectCard)}, enemy : {_playerHnad.Max(v => ValueByData(v))}");
        if (ValueByData(_playerSelectCard) >= _playerHnad.Max(v => ValueByData(v)))
        {
            _isCollectAnswer =true;
            _playerScore++;
            UnityEngine.Debug.Log("Player answer is collect");
        }
        else
        {
            _isCollectAnswer = false;
            UnityEngine.Debug.Log("Enemy answer is failed");
        }

        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

        if (deck.Count < 2)
        {
            UnityEngine.Debug.Log("GameEnd");
            _state.Value = StateType.GameEnd;
        }
        else
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));
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
