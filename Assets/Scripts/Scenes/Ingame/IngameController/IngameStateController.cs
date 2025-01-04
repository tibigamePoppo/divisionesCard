using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UniRx;

public class IngameStateController
{
    private StateBase<Unit> _stageReadyState;
    private StateBase<Unit> _playerReadyState; 
    private StateBase<SelectArg> _cardSelectState;
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
        _drawState = new DrawState<Unit>();
        _battleState = new BattleState<BattleArg>();
        _endState = new GameEndState<EndArg>();
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
                selectArg.deck = _model.playerHand;
                _cardSelectState.StateAction(selectArg);
                break;
            case StateType.Battle:
                BattleArg battleArg = new BattleArg();
                battleArg.curentHand = _model.playerHand;
                battleArg.selectCard = _model.PlayerSelectCard;
                UnityEngine.Debug.Log($"model.playerHand.Count is {_model.playerHand.Count}");
                battleArg.isCollectAnswer = _model.IsCollectAnswer;
                battleArg.playerScore = _model.CurrentPlayerScore;
                _battleState.StateAction(battleArg);
                break;
            case StateType.GameEnd:
                EndArg endArg = new EndArg();
                endArg.winnerName = "";//TODO:need FIX
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

public class BattleArg
{
    public List<DivisionData> curentHand;
    public DivisionData selectCard;
    public bool isCollectAnswer;
    public int playerScore;
}
public class EndArg
{
    public string winnerName;
    public int score;
}


