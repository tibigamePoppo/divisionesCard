using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState<T> : StateBase<T>
{
    public override void Init(IngameView ingameView)
    {
        Debug.Log("BattleState.Init");
        base.Init(ingameView);
    }

    public override void StateAction(T arg1)
    {
        BattleArg battleArg = arg1 as BattleArg;
        view.UpdatePlayerHand(battleArg.curendHand.ToArray());
        view.HidePreviewCard();
        base.StateAction(arg1);
    }
}
