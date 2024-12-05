using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndState<T> : StateBase<T>
{
    public override void Init(IngameView ingameView)
    {
        Debug.Log("GameEndState.Init");
        base.Init(ingameView);
    }

    public override void StateAction(T arg1)
    {
        Debug.Log("GameEndState.StateAction");
        Debug.Log("CardSelectState.StateAction");
        EndArg endArg = arg1 as EndArg;
        view.ChangeActiveGameEndPanel(true, endArg.winnerName);
    }
}
