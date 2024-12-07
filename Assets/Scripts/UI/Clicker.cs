using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DungeonMaster
{

    public class Clicker : ElementUI, IPointerDownHandler
    {
        [Header("Ref")]
        [SerializeField] private Image _clickImage;
        [SerializeField] private Sprite _idleSprite;
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private ClicketText[] _clicketTextPool;
        [SerializeField] private ParticleSystem _clickFx;
        [SerializeField] private ParticleSystem _lvlFx;

        [Header("SliderRef")]
        [SerializeField] private Slider _expSlider;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private int[] _levelsExp;

        [Header("Settings")]
        public float TimeActive = .1f;
        public float TargetScale = .8f;
        public float LerpRotate = .1f;

        private int _poolIndex;

        private void OnEnable()
        {
            _clickImage.sprite = _idleSprite;
            UpdateLevelText(Progress.ClickerLvl);
            UpdateExpSlider(Progress.ClickerExp);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            foreach (var item in _clicketTextPool)
            {
                item.gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            Progress.OnChangeClickerLvl += UpdateLevelText;
            Progress.OnChangeClickerExp += UpdateExpSlider;
        }

        private void OnDestroy()
        {
            Progress.OnChangeClickerLvl -= UpdateLevelText;
            Progress.OnChangeClickerExp -= UpdateExpSlider;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            var value = Progress.ClickerLvl;
            Progress.Money += value;
            Progress.ClickerExp += value;

            _clickImage.sprite = _activeSprite;
            _clicketTextPool[_poolIndex % _clicketTextPool.Length].Init(eventData.position, $"+{value}");
            _poolIndex++;

            _clickFx.transform.position = eventData.position;
            _clickFx.Emit(1);

            StopAllCoroutines();
            StartCoroutine(ClickAnimation());
        }

        public IEnumerator ClickAnimation()
        {
            var time = 0f;
            while (time < TimeActive)
            {
                time += Time.deltaTime;
                _clickImage.transform.localScale = Vector3.Lerp(Vector3.one * TargetScale, Vector3.one, time / TimeActive);
                yield return null;
            }

            _clickImage.transform.localScale = Vector3.one;
            _clickImage.sprite = _idleSprite;
        }

        public void UpdateLevelText(int lvl)
        {
            _levelText.text = $"LVL {lvl}";
        }

        public void UpdateExpSlider(int value)
        {
            _expSlider.minValue = 0;
            _expSlider.maxValue = _levelsExp[Progress.ClickerLvl];
            _expSlider.value = value;
            CalculateLevel(value);
        }

        public void CalculateLevel(int value)
        {
            var level = Progress.ClickerLvl;
            while (_levelsExp[level] <= value)
            {
                value -= _levelsExp[level];
                Progress.ClickerExp = value;
                Progress.ClickerLvl++;
                _lvlFx.gameObject.SetActive(true);
                Debug.Log("LvlFx.Play();");
            }
        }
    }
}

