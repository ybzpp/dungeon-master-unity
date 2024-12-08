using Leopotam.Ecs;

namespace DungeonMaster
{
    public class StartBattleSystem : IEcsRunSystem
    {
        private EcsFilter<StartBattleEvent> _filter;
        private BattleManager _battleManager = null;

        public void Run()
        {
            foreach (var item in _filter)
            {
                _battleManager.StartBattle();
                _filter.GetEntity(item).Del<StartBattleEvent>();
            }
        }
    }
}