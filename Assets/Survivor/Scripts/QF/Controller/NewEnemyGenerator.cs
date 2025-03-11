using ProjectSurvivor;
using QFramework;
using Survivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Demo_Project
{
    public class NewEnemyGenerator : MonoBehaviour
    {
        public GameObject mEnemyPrefab;
        /// <summary>
        /// 屏幕左下
        /// </summary>
        private Vector3 mScreenLB
                = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        /// <summary>
        /// 屏幕右上
        /// </summary>
        private Vector3 mScreenRT
                = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        private Transform mPlayerTransform;

        private void Start()
        {
            mPlayerTransform = FindAnyObjectByType<NewPlayer>().transform;

            mScreenLB
                = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            mScreenRT
                    = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            var pos = GeneratePos();

            mEnemyPrefab.InstantiateWithParent(transform)
                .Position2D(pos);

        }
        /// <summary>
        /// 生成随机坐标
        /// </summary>
        /// <returns></returns>
        private Vector2  GeneratePos()
        {
            int direction = RandomUtility.Choose(1, -1);
            Vector2 pos;
            if (direction == 1)
            {
               
              pos.x= RandomUtility.Choose(mScreenLB.x, mScreenRT.x);
              pos.y=Random.Range(mScreenLB.y, mScreenRT.y);
              
            }
            else
            {
               
              pos.y=RandomUtility.Choose(mScreenLB.y, mScreenRT.y);
              pos.x=Random.Range(mScreenLB.x, mScreenRT.x);
               
            }

            return pos;
        }
    }
}
