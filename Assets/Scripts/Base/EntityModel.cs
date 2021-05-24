using UnityEngine;
using System.Collections;
using Qarth;

namespace GameWish.Game
{
    public class EntityModel : BaseComponent
    {

        protected GameObject m_ObjModel;

        public Transform TrsModel
        {
            get
            {
                return m_ObjModel == null ? null : m_ObjModel.transform;
            }
        }

        public GameObject ObjModel
        {
            get { return m_ObjModel; }
        }

        public EntityModel()
        {

        }

        public override void OnDestory()
        {
            m_ObjModel = null;
        }
    }
}