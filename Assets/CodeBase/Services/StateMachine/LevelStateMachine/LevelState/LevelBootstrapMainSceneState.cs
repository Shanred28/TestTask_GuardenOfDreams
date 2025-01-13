using CodeBase.Configs.Interface;
using CodeBase.Configs.Level;
using CodeBase.Services.Factory.EntityFactory.Interface;
using CodeBase.Services.StateMachine.Common.Interface;
using CodeBase.Services.StateMachine.LevelStateMachine.Interface;
using CodeBase.Services.WindowsProvider;
using CodeBase.UI.MainUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Services.StateMachine.LevelStateMachine.LevelState
{
    public class LevelBootstrapMainSceneState : IEnterableState, IService
    {
        private readonly IGameFactory _gameFactory;
        private readonly ILevelStateSwitcher _levelStateSwitcher;
        private readonly IConfigsProvider _configsProvider;
        private readonly IWindowsProvider _windowsProvider;

        public LevelBootstrapMainSceneState(ILevelStateSwitcher levelStateSwitcher, IConfigsProvider configsProvider, IWindowsProvider windowsProvider,IGameFactory gameFactory)
        {
            Debug.Log("LEVEL: InitMainScene");
            _gameFactory = gameFactory;
            _levelStateSwitcher = levelStateSwitcher;
            _configsProvider = configsProvider;
            _windowsProvider = windowsProvider;
        }

        public void Enter()
        {
            _gameFactory.CreateHero();
            _gameFactory.CreateEnemy(3);
        }
    }
}