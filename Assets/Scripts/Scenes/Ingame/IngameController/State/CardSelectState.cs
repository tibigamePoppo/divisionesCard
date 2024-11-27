using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectState<T> : StateBase<T>
{
    public override void Init(IngameView ingameView)
    {
        Debug.Log("CardSelectState.Init");
        base.Init(ingameView);
    }

    public override void StateAction(T arg1)
    {
        Debug.Log("CardSelectState.StateAction");
        base.StateAction(arg1);
    }
}
