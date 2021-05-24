using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class EntityBase
    {

        public enum EntityStateEnum
        {
            None,
            BeforeStart,
            Active,
            WaitingDestory,
            Destroyed,
        }

        protected int _entityId = -1;
        public int EntityID
        {
            get { return _entityId; }
            set { _entityId = value; }
        }
        protected EntityStateEnum _eEntityState = EntityStateEnum.None;
        protected List<BaseComponent> _components = new List<BaseComponent>();

        public EntityBase()
        {
            _eEntityState = EntityStateEnum.BeforeStart;
            EntityMgr.S.RegisterEntity(this);
        }

        static public void DestorySelf(EntityBase tar)
        {
            if (tar != null)
                tar.OnDesotrySelf();
        }
        protected virtual void OnDesotrySelf()
        {
            _eEntityState = EntityStateEnum.WaitingDestory;
        }

        protected virtual T AddComponent<T>(T comp) where T : BaseComponent
        {
            comp.InitComponent(this);
            _components.Add(comp);
            return comp;
        }

        protected virtual void Start()
        {
            _components.ForEach(p => p.Start());
        }
        public virtual void Tick(float deltaTime)
        {
            switch (_eEntityState)
            {
                case EntityStateEnum.BeforeStart:
                    {
                        Start();
                        _eEntityState = EntityStateEnum.Active;
                    }
                    break;
                case EntityStateEnum.Active:
                    {
                        _components.ForEach(p => p.Tick(deltaTime));
                    }
                    break;
                case EntityStateEnum.WaitingDestory:
                    {
                        _eEntityState = EntityStateEnum.Destroyed;
                        _components.ForEach(p => p.OnDestory());
                        EntityMgr.S.UnRegisterEntity(this);
                    }
                    break;
            }
        }

    }
}