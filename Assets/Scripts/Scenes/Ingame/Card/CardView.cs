using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Ingame.Card
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _image;
        private Button _button;
        private Subject<DivisionData> _buttonClick = new Subject<DivisionData>();
        public IObservable<DivisionData> OnClick => _buttonClick;
        private DivisionData _data;
        public DivisionData Data {  get { return _data; } }
        
        public void Init(DivisionData data)
        {
            _data = data;
            _name.text = _data.name;

            _button = GetComponent<Button>();
            _button.OnClickAsObservable().Subscribe(_ =>
            { 
                 _buttonClick.OnNext(_data);
            }).AddTo(this);
        }
    }
}