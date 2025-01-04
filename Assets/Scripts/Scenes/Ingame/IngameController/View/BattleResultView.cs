using UnityEngine;
using TMPro;

public class BattleResultView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _resulttext;

    public void Init()
    {
        gameObject.SetActive(false);
    }

    public void ShowBattleResultPanel(bool isCollectAnswer)
    {
        gameObject.SetActive(true);
        _resulttext.text = isCollectAnswer ? "Collect" : "Failed";
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
