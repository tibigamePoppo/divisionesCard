using UnityEngine;
using TMPro;
using DG.Tweening;

public class BattleResultView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _resulttext;

    public void Init()
    {
        gameObject.SetActive(false);
        transform.DOScaleY(0, 0.2f).SetEase(Ease.InOutSine);
    }

    public void ShowBattleResultPanel(bool isCollectAnswer)
    {
        gameObject.SetActive(true);
        transform.DOScaleY(1, 0.2f).SetEase(Ease.InOutSine);
        _resulttext.text = isCollectAnswer ? "ê≥âÅI" : "ïsê≥âÅI";
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
        transform.DOScaleY(0, 0.2f).SetEase(Ease.InOutSine);
    }
}
