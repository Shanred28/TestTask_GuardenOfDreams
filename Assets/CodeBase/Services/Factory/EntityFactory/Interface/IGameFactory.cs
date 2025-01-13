using UnityEngine;

namespace CodeBase.Services.Factory.EntityFactory.Interface
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero();
        
       void CreateEnemy(int countEnemy);

        GameObject HeroObject { get; }
    }
}

