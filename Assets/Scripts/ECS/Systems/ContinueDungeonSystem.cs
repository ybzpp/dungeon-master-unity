using Leopotam.Ecs;

namespace DungeonMaster
{
    public class ContinueDungeonSystem : IEcsRunSystem
    {
        private EcsFilter<ContinueDungeonEvent> _filter;
        private BattleManager _battleManager = null;

        public void Run()
        {
            foreach (var item in _filter)
            {
                _battleManager.ContinueDungeon();
            }
        }
    }
}