using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleMgr : TMonoSingleton<BattleMgr>
    {
        public Mesh mesh;
        public Material material;
        public TextAsset textAsset;



        private void Start()
        {
            Init();
        }

        public void Init()
        {
            for (int i = 0; i < 30000; i++)
            {
                RoleBase role = new RoleBase();
            }
        }

        private void Update()
        {
            EntityMgr.S.Tick(Time.deltaTime);
        }
    }

}