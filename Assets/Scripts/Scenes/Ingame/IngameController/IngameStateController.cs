using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UniRx;

public class IngameStateController
{
    private StateBase<Unit> _stageReadyState;
    private StateBase<Unit> _playerReadyState; 
    private StateBase<Unit> _cardSelectState;
    private StateBase<DrawArg> _drawState;
    private StateBase<Unit> _battleState;
    private IngameModel _model;
    private IngameView _view;
    public void Init(IngameModel model,IngameView view)
    {
        _model = model;
        _view = view;

        _stageReadyState = new StageRaadyState<Unit>();
        _playerReadyState = new PlayerReadyState<Unit>();
        _cardSelectState = new CardSelectState<Unit>();
        _drawState = new DrawState<DrawArg>();
        _battleState = new BattleState<Unit>();
        _stageReadyState.Init(view);
        _playerReadyState.Init(view);
        _cardSelectState.Init(view);
        _drawState.Init(view);
        _battleState.Init(view);

        model.State.Subscribe(StateType => ChangeState(StateType)).AddTo(view);
    }

    public void ChangeState(StateType state)
    {
        switch (state)
        {
            case StateType.StageReady:
                _stageReadyState.StateAction(default);
                break;
            case StateType.PlayerReady:
                _playerReadyState.StateAction(default);
                break;
            case StateType.Draw:
                DrawArg drawArg = new DrawArg();
                drawArg.deck = _model.playerDeck;
                _drawState.StateAction(drawArg);
                break;
            case StateType.CardSelect:
                _cardSelectState.StateAction(default);
                break;
            case StateType.Battle:
                _battleState.StateAction(default);
                break;
            default:
                break;
        }
    }
}
public class DrawArg
{
    public List<DivisionData> deck;
}

 
