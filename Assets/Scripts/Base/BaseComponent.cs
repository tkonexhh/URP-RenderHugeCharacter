using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BaseComponent
    {
        protected EntityBase m_Owner;
        public virtual void InitComponent(EntityBase owner)
        {
            m_Owner = owner;
        }

        #region to override funcs
        public virtual void Start() { }
        public virtual void Tick(float deltaTime) { }
        public virtual void OnDestory() { }
        public virtual void OnDrawGizmos() { }
        #endregion

    }
}