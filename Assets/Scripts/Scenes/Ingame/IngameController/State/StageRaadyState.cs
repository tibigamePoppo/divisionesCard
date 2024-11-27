using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRaadyState<T> : StateBase<T>
{
    public override void Init(IngameView ingameView)
    {
        Debug.Log("StageRaadyState.Init");
        base.Init(ingameView);
    }

    public override void StateAction(T arg1)
    {
        Debug.Log("StageRaadyState.StateAction");
        base.StateAction(arg1);
    }
}
