using System.Collections.Generic;
using UnityEngine;

public class ProcessView : MonoBehaviour
{
    [SerializeField] private ProcessUnitView _proccesUnit;
    private List<ProcessUnitView> _proccesUnitViews = new List<ProcessUnitView>();
    private int _maxCount = 42; // 問題の最大数
    private int _count = 0; // 現在の問題
    public void Init()
    {
        Debug.Log($" ProcessView init");
        for (int i = 0; i < _maxCount; i++)
        {
            Debug.Log($"count : {i}");
            var newUnit = Instantiate(_proccesUnit,transform);
            _proccesUnitViews.Add(newUnit);
        }
    }

    public void SetRessult(bool isCorrect)
    {
        if (_count > _proccesUnitViews.Count) return;
        _proccesUnitViews[_count].ChangeImage(isCorrect);
        _count++;
    }
}
