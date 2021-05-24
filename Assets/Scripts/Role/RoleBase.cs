using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class RoleBase : EntityBase
    {

        public RoleBase() : base()
        {
            AddComponent(new RoleRenderComponent());
        }
    }

}