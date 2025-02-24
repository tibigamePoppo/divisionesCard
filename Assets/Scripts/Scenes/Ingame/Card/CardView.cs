using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace Scenes.Ingame.Card
{
    public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _value;
        [SerializeField] private GameObject _valuePanel;
        [SerializeField] private Image _image;
        [SerializeField] private SpritePool _spritePool;
        private Subject<DivisionData> _pointerOverEvent = new Subject<DivisionData>();
        private Vector2 _nonSelectScale = new Vector2(1f,1f);
        private Vector2 _selectScale = new Vector2(1.1f, 1.1f);
        private bool _isScale = false;

        private Button _button;
        private Subject<DivisionData> _buttonClick = new Subject<DivisionData>();
        public IObservable<DivisionData> OnClick => _buttonClick;
        public IObservable<DivisionData> OnPointerOver => _pointerOverEvent;
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

#if UNITY_EDITOR
#else
            _valuePanel.SetActive(false);//Unity上だけ値を表示する
#endif
        }

        public void UpdateValue(DivisionProfileType type)
        {
            Value(type);
        }

        private void Value(DivisionProfileType type)
        {
            switch (type)
            {
                case DivisionProfileType.surfice:
                    _value.text = $"{_data.surfaceSize/1000} k";
                    break;
                case DivisionProfileType.population:
                    _value.text = $"{_data.population/1000} k";
                    break;
                case DivisionProfileType.temperature:
                    _value.text = $"{_data.temperature} c";
                    break;
                case DivisionProfileType.urban:
                    _value.text = $"{_data.urban} 個";
                    break;
                case DivisionProfileType.village:
                    _value.text = $"{_data.village} 個";
                    break;
                case DivisionProfileType.forestSize:
                    _value.text = $"{_data.forestSize/1000} k ha";
                    break;
                case DivisionProfileType.Hospitals:
                    _value.text = $"{_data.Hospitals} 個";
                    break;
                case DivisionProfileType.College:
                    _value.text = $"{_data.College} 個";
                    break;
                default:
                    break;
            }
        }

        public void SetScale(bool value)
        {
            _isScale = value;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isScale) return;
            transform.DOScale(_selectScale, 0.2f).SetEase(Ease.InOutQuad);
            _pointerOverEvent.OnNext(_data);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isScale) return;
            transform.DOScale(_nonSelectScale, 0.2f).SetEase(Ease.InOutQuad);
            _pointerOverEvent.OnNext(null);
        }
    }
}