using Leopotam.Ecs;
using UnityEngine;

namespace DungeonMaster
{
    public abstract class View : MonoBehaviour
    {
        public EcsEntity Entity;

        public virtual void Init(EcsEntity entity)
        {
            Entity = entity;
        }
    }
}

