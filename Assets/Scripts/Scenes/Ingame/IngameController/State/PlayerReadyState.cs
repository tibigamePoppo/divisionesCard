using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReadyState<T> : StateBase<T>
{
    public override void Init(IngameView ingameView)
    {
        Debug.Log("PlayerReadyState.Init");
        base.Init(ingameView);
    }

    public override void StateAction(T arg1)
    {
        Debug.Log("PlayerReadyState.StateAction");
        base.StateAction(arg1);
    }
}
