using UniRx;
using UnityEngine;

public class IngamePresenter : MonoBehaviour
{
    private IngameModel _ingameModel;
    private IngameView _ingameView;
    private IngameStateController _stateController;

    void Start()
    {
        _ingameModel = new IngameModel();
        _stateController = new IngameStateController();
        _ingameView = GetComponent<IngameView>();

        _stateController.Init(_ingameModel, _ingameView);
        _ingameModel.Init();
        _ingameView.Init();

        _ingameView.SelectedCard.Where(_ => _ingameModel.CurrentState == StateType.CardSelect).Subscribe(data =>_ingameModel.SetPlayerSelectCard(data)).AddTo(this);
        _ingameModel.UpdateTheme.Subscribe(theme =>
        {
            _ingameView.SetThemeText(theme);
            _ingameView.SetTheme(theme);
        }).AddTo(this);
    }
}
