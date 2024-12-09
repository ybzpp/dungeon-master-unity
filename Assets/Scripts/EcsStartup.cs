using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace DungeonMaster
{
    public class EcsStartup : MonoBehaviour 
    {
        [SerializeField] private Config _config;
        [SerializeField] private LocationManager _locationManager;
        [SerializeField] private BattleManager _battleManager;
        [SerializeField] private RuntimeData _runtimeData;
        [SerializeField] private SignalBus _signalBus;
        [SerializeField] private UI _ui;
        [SerializeField] private ObjectPoolController _objectPool;

        private EcsWorld _world;
        private EcsSystems _systems;

        void Start ()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif

            Init();
            _systems
                .Add(new InitSystem())
                .Add(new StartDungeonSystem()).OneFrame<StartDungeonEvent>()
                .Add(new ResetProgressSystem()).OneFrame<ResetProgressEvent>()
                .Add(new NextDungeonRoomSystem()).OneFrame<NextDungeonRoomEvent>()
                .Add(new StartBattleSystem())
                .Add(new CheckGameOverSystem())
                .Add(new WinSystem()).OneFrame<WinEvent>()
                .Add(new LoseSystem()).OneFrame<LoseEvent>()
                .Add(new ContinueDungeonSystem()).OneFrame<ContinueDungeonEvent>()
                .Add(new ChangeGameStateSystem()).OneFrame<ChangeGameStateEvent>()
                .Add(new ShowBoyBuyPopupSystem())
                .Add(new AddBoyToPartySystem())
                .Add(new BoyBuySystem())
                .Add(new RerollSystem())
                .Add(new DamageSystem()).OneFrame<DamageEvent>()
                .Add(new HealSystem()).OneFrame<HealEvent>()
                .Add(new EnemyDeadSystem())
                .Add(new DestroyAllBoysSystem())
                .Add(new DestroyEnemyDeadSystem())
                .Add(new HealthViewSystem())
                .Add(new RespawnSystem())
                .Add(new SelectSystem())

                .Inject(_config)
                .Inject(_locationManager)
                .Inject(_battleManager)
                .Inject(_runtimeData)
                .Inject(_objectPool)
                .Inject(_signalBus)
                .Inject(_ui)
                .Init();
        }

        void Update () 
        {
            _systems?.Run ();
        }

        void OnDestroy () 
        {
            if (_systems != null) 
            {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }

        public void Init()
        {
            FindRefs();

            _runtimeData = new RuntimeData();
            _signalBus = new SignalBus();
            _locationManager.Init(_config, _runtimeData);
            _ui.Init(_config, _runtimeData);

            Service<UI>.Set(_ui);
            Service<Config>.Set(_config);
            Service<EcsWorld>.Set(_world);
            Service<BattleManager>.Set(_battleManager);
            Service<LocationManager>.Set(_locationManager);
            Service<RuntimeData>.Set(_runtimeData);
            Service<SignalBus>.Set(_signalBus);
            Service<ObjectPoolController>.Set(_objectPool);
        }

        public void FindRefs()
        {
            if (_ui == null)
            {
                _ui = FindAnyObjectByType<UI>();
                if (_ui == null) _ui = Instantiate(_config.UiPrefab);
            }

            if (_locationManager == null)
            {
                _locationManager = FindAnyObjectByType<LocationManager>();
                if (_locationManager == null) _locationManager = Instantiate(_config.LocationManagerPrefab);
            }

            if (_battleManager == null)
            {
                _battleManager = FindAnyObjectByType<BattleManager>();
                if (_battleManager == null) _battleManager = Instantiate(_config.BattleManagerPrefab);
            }

            if (_objectPool == null)
            {
                _objectPool = FindAnyObjectByType<ObjectPoolController>();
                if (_objectPool == null) _objectPool = Instantiate(_config.ObjectPoolPrefab);
            }
        }
    }
}