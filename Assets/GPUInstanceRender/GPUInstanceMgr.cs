using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class GPUInstanceMgr : TMonoSingleton<GPUInstanceMgr>
    {
        private List<RenderGroup> m_RenderGroupLst = new List<RenderGroup>();
        private Dictionary<string, RenderGroup> m_RenderGroupMap;

        public List<RenderGroup> renderGroupLst => m_RenderGroupLst;


        public override void OnSingletonInit()
        {
            m_RenderGroupMap = new Dictionary<string, RenderGroup>();
        }


        public RenderGroup AddRenderGroup(RenderGroup group)
        {
            if (m_RenderGroupMap.ContainsKey(group.name))
            {
                return m_RenderGroupMap[group.name];
            }
            m_RenderGroupLst.Add(group);
            m_RenderGroupMap.Add(group.name, group);
            return group;
        }

        public RenderGroup GetRenderGroup(string group)
        {
            if (!m_RenderGroupMap.ContainsKey(group))
            {
                return null;
            }

            return m_RenderGroupMap[group];
        }

        public bool HasRenderGroup(string groupName)
        {
            return m_RenderGroupMap.ContainsKey(groupName);
        }

    }

}