using System;
using System.Collections;
using UnityEngine;

namespace DungeonMaster
{

    public class TransitionFadeScreen : ElementUI
    {
        [SerializeField] private float _fadeTime = .5f;
        [SerializeField] private CanvasGroup _canvasGroup;

        public void Transition(Action onComplete)
        {
            gameObject.SetActive(true);
            StartCoroutine(TransitionAnimation(onComplete));
        }

        public IEnumerator TransitionAnimation(Action onComplete)
        {
            var time = 0f;
            _canvasGroup.blocksRaycasts = true;
            while (time < _fadeTime)
            {
                time += Time.deltaTime;
                _canvasGroup.alpha = time / _fadeTime;
                yield return null;
            }
            time = 1f;
            _canvasGroup.alpha = time;
            onComplete?.Invoke();

            while (time > 0)
            {
                time -= Time.deltaTime;
                _canvasGroup.alpha = time / _fadeTime;
                yield return null;
            }
            time = 0;
            _canvasGroup.alpha = time;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}
