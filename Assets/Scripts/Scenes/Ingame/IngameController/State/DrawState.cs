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
        Debug.Log("DrawState.StateAction");
    }
}
