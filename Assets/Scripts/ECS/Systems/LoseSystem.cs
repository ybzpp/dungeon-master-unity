using Leopotam.Ecs;

namespace DungeonMaster
{
    public class LoseSystem : IEcsRunSystem
    {
        private EcsFilter<LoseEvent> _filter;
        private RuntimeData _runtimeData = null;

        public void Run()
        {
            foreach (var item in _filter)
            {
                Progress.DeathCount++;
                Progress.Money += _runtimeData.Reward.Coins;
                Progress.ClickerExp += _runtimeData.Reward.Exp;
                GameHelper.ChangeGameState(GameState.Lose);
            }
        }
    }
}