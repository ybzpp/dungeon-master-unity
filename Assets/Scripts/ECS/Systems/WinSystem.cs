using Leopotam.Ecs;

namespace DungeonMaster
{
    public class WinSystem : IEcsRunSystem
    {
        private EcsFilter<WinEvent> _filter;
        private RuntimeData _runtimeData = null;
        private Config _config = null;
        private EcsWorld _world = null;
        private UI _ui = null;

        public void Run()
        {
            foreach (var item in _filter)
            {
                var level = _runtimeData.DungeonLevel;
                if (level != 0 && level % _config.FinalBossLevel == 0)
                {
                    Progress.Money += _runtimeData.Reward.Coins;
                    Progress.ClickerExp += _runtimeData.Reward.Exp;
                    GameHelper.ChangeGameState(GameState.Win);
                }
                else
                {
                    _ui.TransitionFade.Transition(() =>
                    {
                        _world.NewEntity().Get<NextDungeonRoomEvent>();
                        _world.NewEntity().Get<DestroyEnemyDeadEvent>();
                    });
                }
            }
        }
    }
}