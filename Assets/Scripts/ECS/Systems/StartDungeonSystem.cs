using Leopotam.Ecs;

namespace DungeonMaster
{
    public class StartDungeonSystem : IEcsRunSystem
    {
        private EcsFilter<StartDungeonEvent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {

            }
        }
    }
}