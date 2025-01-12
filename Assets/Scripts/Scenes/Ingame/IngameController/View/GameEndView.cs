using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEndView : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Button _retryButton;
    public void Init()
    {
        _panel.SetActive(false);
        _retryButton.OnClickAsObservable().Subscribe(_ => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    }

    public void Show(string winnerName,int score)
    {
        _panel.SetActive(true);
        _scoreText.text = $"³“š” : {score}";
    }

    public void Hide()
    {
        _panel.SetActive(false); 
    }
}
