using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class ButtonClickScale : MonoBehaviour
{
    void Start()
    {
        var _button = GetComponent<Button>();
        _button.OnClickAsObservable().Subscribe(_ =>
        {
            transform.DOScale(0.95f, 0.08f).From(1).SetLoops(2, LoopType.Yoyo);
        }).AddTo(this);
    }
}
