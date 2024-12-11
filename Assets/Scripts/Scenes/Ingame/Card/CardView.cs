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
        [SerializeField] private GameObject _greadEffect;
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

            _greadEffect.SetActive(false);
            CheckGreatEffet(type);
        }

        public void UpdateValue(DivisionProfileType type)
        {
            Value(type);
            CheckGreatEffet(type);
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

        private void CheckGreatEffet(DivisionProfileType type)
        {
            bool isActive = false;
            switch (type)
            {
                case DivisionProfileType.surfice:
                    isActive = MasterDataReader.Instance.Master.DivisionData.Select(v => (float)v.surfaceSize).OrderByDescending(x => x).ToList().IndexOf(_data.surfaceSize) < 3;
                    break;
                case DivisionProfileType.population:
                    isActive = MasterDataReader.Instance.Master.DivisionData.Select(v => (float)v.population).OrderByDescending(x => x).ToList().IndexOf(_data.population) < 3;
                    break;
                case DivisionProfileType.temperature:
                    isActive = MasterDataReader.Instance.Master.DivisionData.Select(v => (float)v.temperature).OrderByDescending(x => x).ToList().IndexOf(_data.temperature) < 3;
                    break;
                case DivisionProfileType.urban:
                    isActive = MasterDataReader.Instance.Master.DivisionData.Select(v => (float)v.urban).OrderByDescending(x => x).ToList().IndexOf(_data.urban) < 3;
                    break;
                case DivisionProfileType.village:
                    isActive = MasterDataReader.Instance.Master.DivisionData.Select(v => (float)v.village).OrderByDescending(x => x).ToList().IndexOf(_data.village) < 3;
                    break;
                case DivisionProfileType.forestSize:
                    isActive = MasterDataReader.Instance.Master.DivisionData.Select(v => (float)v.forestSize).OrderByDescending(x => x).ToList().IndexOf(_data.forestSize) < 3;
                    break;
                case DivisionProfileType.Hospitals:
                    isActive = MasterDataReader.Instance.Master.DivisionData.Select(v => (float)v.Hospitals).OrderByDescending(x => x).ToList().IndexOf(_data.Hospitals) < 3;
                    break;
                case DivisionProfileType.College:
                    isActive = MasterDataReader.Instance.Master.DivisionData.Select(v => (float)v.College).OrderByDescending(x => x).ToList().IndexOf(_data.College) < 3;
                    break;
                default:
                    break;
            }
            _greadEffect.SetActive(isActive);
        }
    }
}