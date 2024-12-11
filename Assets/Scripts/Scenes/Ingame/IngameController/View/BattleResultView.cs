using Scenes.Ingame.Card;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;

public class BattleResultView : MonoBehaviour
{
    [SerializeField] CardView _playerCardView;
    [SerializeField] CardView _enemyCardView;
    [SerializeField] TextMeshProUGUI _winnerText;
    [SerializeField] TextMeshProUGUI _playerIndexText;
    [SerializeField] TextMeshProUGUI _enemyIndexText;

    public void Init()
    {
        gameObject.SetActive(false);
    }

    public void ShowBattleResultPanel(DivisionData player, DivisionData enemy, string winnerName,DivisionProfileType type)
    {
        gameObject.SetActive(true);
        _playerCardView.Init(player, type);
        _enemyCardView.Init(enemy, type);
        _winnerText.text = winnerName;
        List<float> value = new List<float>();
        switch (type)
        {
            case DivisionProfileType.surfice:
                value = MasterDataReader.Instance.Master.DivisionData.Select(v => (float)v.surfaceSize).OrderByDescending(x => x).ToList();
                _enemyIndexText.text = $"{value.IndexOf(enemy.surfaceSize) + 1} 位";
                _playerIndexText.text = $"{value.IndexOf(player.surfaceSize) + 1} 位";
                break;
            case DivisionProfileType.population:
                value = MasterDataReader.Instance.Master.DivisionData.Select(v => (float)v.population).OrderByDescending(x => x).ToList();
                _enemyIndexText.text = $"{value.IndexOf(enemy.population) + 1} 位";
                _playerIndexText.text = $"{value.IndexOf(player.population) + 1} 位";
                break;
            case DivisionProfileType.temperature:
                value = MasterDataReader.Instance.Master.DivisionData.Select(v => v.temperature).OrderByDescending(x => x).ToList();
                _enemyIndexText.text = $"{value.IndexOf(enemy.temperature) + 1} 位";
                _playerIndexText.text = $"{value.IndexOf(player.temperature) + 1} 位";
                break;
            case DivisionProfileType.urban:
                value = MasterDataReader.Instance.Master.DivisionData.Select(v => (float)v.urban).OrderByDescending(x => x).ToList();
                _enemyIndexText.text = $"{value.IndexOf(enemy.urban) + 1} 位";
                _playerIndexText.text = $"{value.IndexOf(player.urban) + 1} 位";
                break;
            case DivisionProfileType.village:
                value = MasterDataReader.Instance.Master.DivisionData.Select(v => (float)v.village).OrderByDescending(x => x).ToList();
                _enemyIndexText.text = $"{value.IndexOf(enemy.village) + 1} 位";
                _playerIndexText.text = $"{value.IndexOf(player.village) + 1} 位";
                break;
            case DivisionProfileType.forestSize:
                value = MasterDataReader.Instance.Master.DivisionData.Select(v => (float)v.forestSize).OrderByDescending(x => x).ToList();
                _enemyIndexText.text = $"{value.IndexOf(enemy.forestSize) + 1} 位";
                _playerIndexText.text = $"{value.IndexOf(player.forestSize) + 1} 位";
                break;
            case DivisionProfileType.Hospitals:
                value = MasterDataReader.Instance.Master.DivisionData.Select(v => (float)v.Hospitals).OrderByDescending(x => x).ToList();
                _enemyIndexText.text = $"{value.IndexOf(enemy.Hospitals) + 1} 位";
                _playerIndexText.text = $"{value.IndexOf(player.Hospitals) + 1} 位";
                break;
            case DivisionProfileType.College:
                value = MasterDataReader.Instance.Master.DivisionData.Select(v => (float)v.College).OrderByDescending(x => x).ToList();
                _enemyIndexText.text = $"{value.IndexOf(enemy.College) + 1} 位";
                _playerIndexText.text = $"{value.IndexOf(player.College) + 1} 位";
                break;
            default:
                break;
        }

    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
