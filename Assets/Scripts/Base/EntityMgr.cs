using UnityEngine;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public class EntityMgr : TSingleton<EntityMgr>
    {
        private int _nCounter = 0;
        List<EntityBase> _entities = new List<EntityBase>();

        public void RegisterEntity(EntityBase entity)
        {
            entity.EntityID = _nCounter++;
            _entities.Add(entity);
        }

        public void UnRegisterEntity(EntityBase entity)
        {
            _entities.Remove(entity);
        }

        public void Tick(float fDeltaTime)
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                _entities[i].Tick(fDeltaTime);
            }
        }
    }

}