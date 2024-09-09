using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DelayedText : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _text;
    [SerializeField]private string _targetText;
    [SerializeField]private float _duration;
    [SerializeField]private float _delayTime;
    [SerializeField]private bool _playOnAwake;

    private int _textIndex;
    void Start()
    {
        if (_playOnAwake)
            Play();
    }

    public void Play()
    {
        _text.text=string.Empty;
        _textIndex = 0;
        DOTween.To(() => _textIndex, x => _textIndex = x, _targetText.Length,_duration)
            .OnUpdate(() =>_text.text = _targetText.Substring(0, _textIndex))
            .SetEase(Ease.Linear)
            .SetDelay(_delayTime);
    }
           // .SetLoops(-1, LoopType.Yoyo);

}
