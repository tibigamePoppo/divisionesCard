using UnityEngine;
using UnityEngine.UI;

public class ProcessUnitView : MonoBehaviour
{
    [SerializeField] private Sprite _correct;
    [SerializeField] private Sprite _failed;
    [SerializeField] private Image _image;
    
    public void Init()
    {

    }

    public void ChangeImage(bool isCorrect)
    {
        if (isCorrect)
        {
            _image.sprite = _correct;
        }
        else
        {
            _image.sprite = _failed;
        }
    }
}
