using CodeBase.Configs.Interface;
using CodeBase.Services.Scene;
using CodeBase.Services.StateMachine.Common.Interface;
using CodeBase.Services.StateMachine.GameStateMachine.Interface;

namespace CodeBase.Services.StateMachine.GameStateMachine.GameState
{
    public class LoadNextLevelGameState : IEnterableState, IService
    {
       private readonly ISceneLoader _sceneLoader;
       private readonly IConfigsProvider _configsProvider;

        public LoadNextLevelGameState(ISceneLoader sceneLoader,IConfigsProvider configsProvider)
        {
            _sceneLoader = sceneLoader;
            _configsProvider = configsProvider;
        }

        public void Enter()
        {
            //TODO
            string sceneName = _configsProvider.GetLevelConfig(0).LevelName;
            
            
            _sceneLoader.Load(sceneName);
        }
    }
}