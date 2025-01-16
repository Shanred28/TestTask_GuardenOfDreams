using CodeBase.Configs.Enemy;
using CodeBase.Configs.Interface;
using CodeBase.Entity.Character;
using CodeBase.Entity.Character.Enemy;
using CodeBase.Entity.Character.Player;
using CodeBase.Entity.InventorySystem;
using CodeBase.Services.AssetManager;
using CodeBase.Services.Factory.EntityFactory.Interface;
using CodeBase.UI.Player;
using Lean.Pool;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Services.Factory.EntityFactory
{
    public class GameFactory : IGameFactory
    {
        public GameObject HeroObject { get; private set; }
        private readonly IAssetProvider _assetProvider;
        private readonly IConfigsProvider _configProvider;
        private readonly IObjectResolver _container;
        private readonly SpawnZone _spawnZone;

        public GameFactory(IAssetProvider assetProvider, IConfigsProvider configProvider,IObjectResolver container, SpawnZone spawnZone)
        {
            _assetProvider = assetProvider;
            _configProvider = configProvider;
            _container = container;
            _spawnZone = spawnZone;
        }
        
        public GameObject CreateHero()
        {
            var playerCharacterSettingConfig = _configProvider.GetPlayerConfig();
            
            HeroObject = LeanPool.Spawn(_assetProvider.GetPrefab<GameObject>(playerCharacterSettingConfig.PathPlayerPrefabe),playerCharacterSettingConfig.defaultSpawnPosition,Quaternion.identity);
            GameObject cameraObject = LeanPool.Spawn(_assetProvider.GetPrefab<GameObject>(playerCharacterSettingConfig.PathCameraPrefab));
            GameObject playerHUD = LeanPool.Spawn(_assetProvider.GetPrefab<GameObject>(playerCharacterSettingConfig.PathPlayerHUDPrefab));
            
            UI_PlayerHUDHolder holderUI = playerHUD.GetComponent<UI_PlayerHUDHolder>();
            HeroObject.GetComponent<PlayerLogic>().Initialization(holderUI.Joystick,playerCharacterSettingConfig.MaxHP,playerCharacterSettingConfig.WalkSpeed, holderUI.HealthBar, holderUI.PlayerInventory,holderUI.FireButton);
            cameraObject.GetComponent<CameraFollow>().Initialization(HeroObject.transform);
            
            return HeroObject;
        }
        
        public void CreateEnemy(int enemyCount)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                EnemyConfig config = _configProvider.GetEmyConfig();

                var spawnPoint = _spawnZone.GetRandomPosition();
                LeanPool.Spawn(config.enemyPrefab,spawnPoint,Quaternion.identity).GetComponent<Enemy>().Initialization(HeroObject.transform);
            }
        }
        
        private void InjectToGameObject(GameObject gameObject)
        {
            _container.InjectGameObject(gameObject);
        }
    }
}