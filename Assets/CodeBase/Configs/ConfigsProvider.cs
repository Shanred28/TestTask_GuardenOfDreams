using System.Collections.Generic;
using System.Linq;
using CodeBase.Configs.Enemy;
using CodeBase.Configs.Interface;
using CodeBase.Configs.Level;
using CodeBase.Configs.Player;
using CodeBase.Configs.Scene;
using CodeBase.Configs.WindowsConfig;
using CodeBase.Entity.Character.Enemy;
using CodeBase.UI.MainUI;
using UnityEngine;

namespace CodeBase.Configs
{
    public class ConfigsProvider : IConfigsProvider
    {
        private const string EnemiesConfigsPath = "Configs/Enemies";
        private const string LevelsConfigsPath = "Configs/Levels";
        private const string WindowsConfigsPath = "Configs/Windows";
        private const string ScenesConfigsPath = "Configs/Scene";
        private const string PlayerConfigsPath = "Configs/Player/PlayerConfig";

        private EnemyConfig[] _enemies;
        private Dictionary<string, LevelConfig> _levels;
        private Dictionary<WindowMainUIId, WindowConfig> _windows;

        private SceneConfig[] _configsSceneList;
        private LevelConfig[] _levelsList;
        private PlayerCharacterSetting _playerConfig;
        
        public int LevelAmount => _levelsList.Length;
        
        public void Load()
        {
            _enemies = Resources.LoadAll<EnemyConfig>(EnemiesConfigsPath).ToArray();
            _windows = Resources.LoadAll<WindowConfig>(WindowsConfigsPath).ToDictionary(x => x.windowId, x => x);
            
            _configsSceneList = Resources.LoadAll<SceneConfig>(ScenesConfigsPath).ToArray();
            _levelsList = Resources.LoadAll<LevelConfig>(LevelsConfigsPath).ToArray();
            _levels = _levelsList.ToDictionary(x => x.LevelName, x => x);
            _playerConfig = Resources.Load<PlayerCharacterSetting>(PlayerConfigsPath);
        }

        public PlayerCharacterSetting GetPlayerConfig()
        {
            return _playerConfig;
        }
        
        public SceneConfig GetSceneConfig(string nameScene)
        {
            foreach (var sceneConfig in _configsSceneList)
            {
                if (nameScene == sceneConfig.sceneName)
                {
                    return  sceneConfig;
                }
            }
            
            return null;
        }

        public LevelConfig GetLevelConfig(int index)
        {
            return _levelsList[index];
        }
        
        public LevelConfig GetLevelConfig(string name)
        {
            return _levels[name];
        }

        public EnemyConfig GetEmyConfig()
        {
            int index = Random.Range(0, _enemies.Length);
            return _enemies[index];
        }

        public WindowConfig GetWindowConfig(WindowMainUIId id)
        {
            return _windows[id];
        }

        public SpawnZone GetSpawnZone()
        {
            throw new System.NotImplementedException();
        }
    }
}

