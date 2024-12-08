namespace DungeonMaster
{
    public interface IView
    {
        void Init(Config config, RuntimeData runtimeData);
        void Show();
        void Hide();
    }
}
