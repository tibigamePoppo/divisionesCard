using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Ingame.Card
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _value;
        [SerializeField] private Image _image;
        [SerializeField] private SpritePool _spritePool;
        private Button _button;
        private Subject<DivisionData> _buttonClick = new Subject<DivisionData>();
        public IObservable<DivisionData> OnClick => _buttonClick;
        private DivisionData _data;
        public DivisionData Data {  get { return _data; } }
        
        public void Init(DivisionData data, DivisionProfileType type)
        {
            _data = data;
            _name.text = _data.name;
            _image.sprite = _spritePool.sprites.FirstOrDefault(v => v.name == data.enName);
            Value(type);

            _button = GetComponent<Button>();
            _button.OnClickAsObservable().Subscribe(_ =>
            { 
                 _buttonClick.OnNext(_data);
            }).AddTo(this);
        }

        public void UpdateValue(DivisionProfileType type)
        {
            Value(type);
        }

        private void Value(DivisionProfileType type)
        {
            Debug.Log($"type : {type}");
            switch (type)
            {
                case DivisionProfileType.surfice:
                    _value.text = _data.surfaceSize.ToString();
                    break;
                case DivisionProfileType.population:
                    _value.text = _data.population.ToString();
                    break;
                case DivisionProfileType.temperature:
                    _value.text = _data.temperature.ToString();
                    break;
                case DivisionProfileType.urban:
                    _value.text = _data.urban.ToString() ;
                    break;
                case DivisionProfileType.village:
                    _value.text = _data.village.ToString();
                    break;
                case DivisionProfileType.forestSize:
                    _value.text = _data.forestSize.ToString();
                    break;
                case DivisionProfileType.Hospitals:
                    _value.text = _data.Hospitals.ToString() ;
                    break;
                case DivisionProfileType.College:
                    _value.text = _data.College.ToString();
                    break;
                default:
                    break;
            }
        }
    }
}