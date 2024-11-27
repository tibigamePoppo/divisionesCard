using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UniRx;
public class IngameModel
{
    private const int PLAYERHANDCOUNT = 5;//プレイヤーの手札の枚数

    private Subject<Unit> _stageReady = new Subject<Unit>();
    public IObservable<Unit> StageReady => _stageReady;
    private ReactiveProperty<StateType> _state = new ReactiveProperty<StateType>();
    public IObservable<StateType> State => _state;
    private List<DivisionData> _deck = new List<DivisionData>();
    private List<DivisionData> _playerHnad = new List<DivisionData>();
    public List<DivisionData> deck { get { return _deck; } }
    public List<DivisionData> playerDeck { get { return _playerHnad; } }
    private DivisionData _playerSelectCard;
    private DivisionData _enemySelectCard;
    public void Init()
    {
        CreateDeck();
        _state.Value = StateType.PlayerReady;

        State.Where(state => state == StateType.StageReady)
            .Subscribe(_ => _state.Value = StateType.PlayerReady);

        State.Where(state => state == StateType.PlayerReady)
            .Subscribe(_ => PlaeyrHandBuild());

        State.Where(state => state == StateType.CardSelect)
            .Subscribe(_ => EnemySelectCard());

        State.Where(state => state == StateType.CardSelect)
            .Subscribe(_ => Battle());
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

    /// <summary>
    /// プレイヤーの手札の設定
    /// </summary>
    /// <param name="type"></param>
    public void PlayerHandSet(DivisionData type)
    {
        _playerHnad.Add(type);
        _deck.Remove(type);
    }

    public void PlayerSelectCard(DivisionData data)
    {
        _playerSelectCard = data;
        _state.Value = StateType.CardSelect;
    }

    public void EnemySelectCard()
    {

        _enemySelectCard = _deck[UnityEngine.Random.Range(0, _deck.Count)];
        _deck.Remove(_enemySelectCard);
        UnityEngine.Debug.Log($"EnemyCard is {_enemySelectCard.name}");
        _state.Value = StateType.Battle;
    }

    public void Battle()
    {
        //TODO:とりあえず面積のみで勝負
        if (_playerSelectCard.surfaceSize > _enemySelectCard.surfaceSize)
        {
            UnityEngine.Debug.Log("PlayerWin");
        }
        else if (_playerSelectCard.surfaceSize < _enemySelectCard.surfaceSize)
        {
            UnityEngine.Debug.Log("EnemyWin");
        }
        else
        {
            UnityEngine.Debug.Log("Draw");
        }
    }
}
