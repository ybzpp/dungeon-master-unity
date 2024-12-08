using UnityEngine;

namespace DungeonMaster
{
    public abstract class LocationBase : MonoBehaviour, IView
    {
        public virtual void Init(Config config, RuntimeData runtimeData) { }
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
