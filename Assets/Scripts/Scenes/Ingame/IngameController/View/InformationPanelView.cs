using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Linq;

public class InformationPanelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _divisionName;
    [SerializeField] private TextMeshProUGUI _divisionSpecialty;
    [SerializeField] private TextMeshProUGUI _divisionProfile;
    private float _hidePosition = -400;
    private float _showPosition = 400;

    public void Init()
    {
        transform.DOMoveX(_hidePosition, 0f).SetEase(Ease.InSine);
    }

    public void ShowPanel(DivisionData data)
    {
        _divisionName.text = data.name;
        _divisionSpecialty.text = MasterDataReader.Instance.Master.DivisionProfile.FirstOrDefault(d => d.name == data.name).specialty;
        _divisionProfile.text = MasterDataReader.Instance.Master.DivisionProfile.FirstOrDefault(d => d.name == data.name).profile;
        transform.DOMoveX(_showPosition, 0.1f).SetEase(Ease.InSine);
    }

    public void HidePanel()
    {
        transform.DOMoveX(_hidePosition, 0.1f).SetEase(Ease.InSine);
    }
    
}
