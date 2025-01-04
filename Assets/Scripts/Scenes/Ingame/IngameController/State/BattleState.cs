using Cysharp.Threading.Tasks;
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
        BattleAnime(battleArg).Forget();
        base.StateAction(arg1);
    }

    private async UniTaskVoid BattleAnime(BattleArg battleArg)
    {
        view.RemovePlayerHand(battleArg.curentHand.ToArray());
        view.ReadyButtonInteractable(false);
        view.ShowBattleResultPanel(battleArg.isCollectAnswer);
        await UniTask.Delay(System.TimeSpan.FromSeconds(1.5f));
        view.HideBattleResultPanel();
        view.HidePreviewCard();
        view.UpdateScore("Player", battleArg.playerScore);
    }
}
