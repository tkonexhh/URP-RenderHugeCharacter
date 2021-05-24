using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class EntityData
    {
        protected EntityBase m_Owner;

        public EntityData(EntityBase owner)
        {
            m_Owner = owner;
        }
    }

}