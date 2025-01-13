using CodeBase.Configs.Enemy;
using CodeBase.Configs.Level;
using CodeBase.Configs.Player;
using CodeBase.Configs.Scene;
using CodeBase.Configs.WindowsConfig;
using CodeBase.Entity.Character.Enemy;
using CodeBase.Services;
using CodeBase.UI.MainUI;

namespace CodeBase.Configs.Interface
{
    public interface IConfigsProvider : IService
    {
        void Load();
        
        public PlayerCharacterSetting GetPlayerConfig();
        public SceneConfig GetSceneConfig(string nameScene);
        public LevelConfig GetLevelConfig(int index);
        int LevelAmount { get; }
        public EnemyConfig GetEmyConfig();
        public WindowConfig GetWindowConfig(WindowMainUIId id);
        
        public SpawnZone GetSpawnZone();
    }
}