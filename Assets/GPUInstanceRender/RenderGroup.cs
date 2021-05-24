using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class RenderGroup
    {
        private Mesh m_Mesh;
        private Material m_Mat;
        protected int m_Size;
        private int m_Capacity;
        protected MaterialPropertyBlock m_MatPropBlock;
        protected ComputeBuffer m_ComputeBuffer;
        protected List<RenderCell> m_RenderCellLst = new List<RenderCell>();

        public Mesh mesh => m_Mesh;
        public Material material => m_Mat;

        public MaterialPropertyBlock mpb
        {
            get
            {
                if (m_MatPropBlock == null)
                {
                    m_MatPropBlock = new MaterialPropertyBlock();
                }
                return m_MatPropBlock;
            }
        }

        public string name { get; private set; }
        public int size => m_Size;



        public RenderGroup(Mesh mesh, Material material) : this(60, mesh, material)
        {
        }

        public RenderGroup(int capacity, Mesh mesh, Material material)
        {
            m_Mesh = mesh;
            m_Mat = material;
            m_Capacity = capacity;
            m_ComputeBuffer = new ComputeBuffer(m_Capacity, 76);
            name = mesh.name;
        }

        public void AddRenderCell(RenderCell cell)
        {
            m_RenderCellLst.Add(cell);

            if (m_Size + 1 > m_Capacity)
            {
                m_Capacity *= 2;
                m_ComputeBuffer = new ComputeBuffer(m_Capacity, 76);
            }

            m_Size++;
        }

        public void RemoveRenderCell(RenderCell cell)
        {
            m_RenderCellLst.Remove(cell);
            m_Size--;
        }

        public virtual void UpdateMaterialProperties()
        {
            List<CellInfo> animInfos = new List<CellInfo>();
            for (int i = 0; i < m_Size; i++)
            {
                CellInfo animInfo = new CellInfo()
                {
                    trs = Matrix4x4.TRS(m_RenderCellLst[i].position, m_RenderCellLst[i].rotation, Vector3.one),
                    animLerp = 0,
                    animRate1 = 0,
                    animRate2 = 0
                };

                animInfos.Add(animInfo);
            }
            m_ComputeBuffer.SetData(animInfos);
            mpb.SetMatrix(ShaderProperties.LocalToWorldID, GPUInstanceMgr.S.transform.localToWorldMatrix);
            mpb.SetBuffer(ShaderProperties.AnimInfosID, m_ComputeBuffer);
        }

        public class ShaderProperties
        {
            public static readonly int LocalToWorldID = Shader.PropertyToID("_LocalToWorld");
            public static readonly int AnimInfosID = Shader.PropertyToID("_AnimInfos");

        }

        public struct CellInfo
        {
            public Matrix4x4 trs;
            public float animRate1;
            public float animRate2;
            public float animLerp;
        }
    }




}