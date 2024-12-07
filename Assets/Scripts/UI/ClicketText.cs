using TMPro;
using UnityEngine;

namespace DungeonMaster
{
    public class ClicketText : ElementUI
    {
        [SerializeField] private Animation _animation;
        [SerializeField] private TMP_Text _text;

        public void Init(Vector3 position, string text)
        {
            gameObject.SetActive(true);
            transform.position = position;
            _text.text = text;
            _animation.Play();
        }
    }
}

