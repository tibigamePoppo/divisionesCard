using UnityEngine;
using TMPro;

public class GameEndView : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _winnerText;
    public void Init()
    {
        _panel.SetActive(false);
    }

    public void Show(string winnerName)
    {
        _panel.SetActive(true);
        _winnerText.text = $"{winnerName} èüóòÅI";
    }

    public void Hide()
    {
        _panel.SetActive(false); 
    }
}
