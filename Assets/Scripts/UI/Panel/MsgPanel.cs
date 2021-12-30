using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Panel
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MsgPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        private CanvasGroup _canvasGroup;
        
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.transform.localScale = Vector3.zero;
            _canvasGroup.alpha = 0f;
            
        }

        public void Show(string message, Action callback)
        {
            text.SetText(message);
            var s = DOTween.Sequence();
            s.Append(_canvasGroup.DOFade(1, 0.7f));
            s.Append(_canvasGroup.transform.DOScale(1,0.25f));
            s.AppendInterval(2f);
            s.Play();
            s.OnComplete(() => Hide(callback));
            
        }

        private void Hide(Action callback)
        {
            var s = DOTween.Sequence();
            s.Append(_canvasGroup.DOFade(0, 0.7f));
            s.Append(_canvasGroup.transform.DOScale(0,0.25f));
            s.Play();
            s.OnComplete(() => callback());
        }
    }
}