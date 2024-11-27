using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawState<T> : StateBase<T>
{
    public override void Init(IngameView ingameView)
    {
        Debug.Log("DrawState.Init");
        base.Init(ingameView);
    }

    public override void StateAction(T arg1)
    {
        DrawArg drawArg = arg1 as DrawArg;
        Debug.Log($"DrawState.StateAction {drawArg.deck.Count}");
        base.StateAction(arg1);
        for (int i = 0; i < drawArg.deck.Count; i++)
        {
            view.CreateHand(drawArg.deck[i]);
        }
    }
}
