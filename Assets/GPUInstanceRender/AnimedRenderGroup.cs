using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class AnimedRenderGroup : RenderGroup
    {

        public AnimedRenderGroup(Mesh mesh, Material material, AnimDataInfo animDataInfo) : this(60, mesh, material, animDataInfo)
        {
        }

        public AnimedRenderGroup(int capacity, Mesh mesh, Material material, AnimDataInfo animDataInfo) : base(capacity, mesh, material)
        {
            AnimedRenderCell.SetAnimData(animDataInfo);// static 
        }


        public override void UpdateMaterialProperties()
        {
            List<AnimedInfo> animInfos = new List<AnimedInfo>();
            for (int i = 0; i < m_Size; i++)
            {
                var animedCell = m_RenderCellLst[i] as AnimedRenderCell;
                AnimedInfo animInfo = new AnimedInfo()
                {
                    trs = Matrix4x4.TRS(animedCell.position, animedCell.rotation, Vector3.one),
                    animLerp = animedCell.animLerp,
                    animRate1 = animedCell.animRate1,
                    animRate2 = animedCell.animRate2
                };

                animInfos.Add(animInfo);
            }
            m_ComputeBuffer.SetData(animInfos);
            mpb.SetMatrix(ShaderProperties.LocalToWorldID, GPUInstanceMgr.S.transform.localToWorldMatrix);
            mpb.SetBuffer(ShaderProperties.AnimInfosID, m_ComputeBuffer);
        }


        private struct AnimedInfo
        {
            public Matrix4x4 trs;
            public float animRate1;
            public float animRate2;
            public float animLerp;
        }
    }

}