using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UniRx;

public class IngameStateController
{
    private StateBase<Unit> _stageReadyState;
    private StateBase<Unit> _playerReadyState; 
    private StateBase<SelectArg> _cardSelectState;
    private StateBase<EnemySelectArg> _enemyCardSelectState;
    private StateBase<Unit> _drawState;
    private StateBase<BattleArg> _battleState;
    private StateBase<EndArg> _endState;
    private IngameModel _model;
    private IngameView _view;
    public void Init(IngameModel model,IngameView view)
    {
        _model = model;
        _view = view;

        _stageReadyState = new StageRaadyState<Unit>();
        _playerReadyState = new PlayerReadyState<Unit>();
        _cardSelectState = new CardSelectState<SelectArg>();
        _enemyCardSelectState = new EnemyCardSelectState<EnemySelectArg>();
        _drawState = new DrawState<Unit>();
        _battleState = new BattleState<BattleArg>();
        _endState = new GameEndState<EndArg>();
        _enemyCardSelectState.Init(view);
        _stageReadyState.Init(view);
        _playerReadyState.Init(view);
        _cardSelectState.Init(view);
        _drawState.Init(view);
        _battleState.Init(view);
        _endState.Init(view);

        model.State.Subscribe(StateType => ChangeState(StateType).Forget()).AddTo(view);
    }

    public async UniTaskVoid ChangeState(StateType state)
    {
        await UniTask.WaitForFixedUpdate();
        switch (state)
        {
            case StateType.StageReady:
                _stageReadyState.StateAction(default);
                break;
            case StateType.PlayerReady:
                _playerReadyState.StateAction(default);
                break;
            case StateType.Draw:
                _drawState.StateAction(default);
                break;
            case StateType.CardSelect:
                SelectArg selectArg = new SelectArg();
                selectArg.deck = _model.playerDeck;
                _cardSelectState.StateAction(selectArg);
                break;
            case StateType.EnemyCardSelect:
                EnemySelectArg enemySelectArg = new EnemySelectArg();
                enemySelectArg.card = _model.EnemySelectCardData;
                _enemyCardSelectState.StateAction(enemySelectArg);
                break;
            case StateType.Battle:
                BattleArg battleArg = new BattleArg();
                battleArg.curentHand = _model.playerDeck;
                battleArg.enemyCard = _model.EnemySelectCardData;
                battleArg.playerCard = _model.PlayerSelectCard;
                battleArg.winnerName = _model.WinnerName;
                battleArg.playerScore = _model.CurrentPlayerScore;
                battleArg.enemyScore = _model.CurrentEnemyScore;
                _battleState.StateAction(battleArg);
                break;
            case StateType.GameEnd:
                EndArg endArg = new EndArg();
                endArg.winnerName = _model.WinnerName;
                endArg.score = _model.CurrentPlayerScore;
                _endState.StateAction(endArg);
                break;
            default:
                break;
        }
    }
}
public class SelectArg
{
    public List<DivisionData> deck;
}

public class EnemySelectArg
{
    public DivisionData card;
}

public class BattleArg
{
    public List<DivisionData> curentHand;
    public DivisionData enemyCard;
    public DivisionData playerCard;
    public string winnerName;
    public int playerScore;
    public int enemyScore;
}
public class EndArg
{
    public string winnerName;
    public int score;
}


