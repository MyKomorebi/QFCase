using UnityEngine;
using QFramework;
using System.Collections.Generic;
using System;

namespace ProjectSurvivor
{

    [Serializable]

    public class EnemyWave
    {
        public float GeneraDuration = 1;
        public GameObject EnemyPrefab;
        public int Seconds = 10;
    }
    public partial class EnemyGenerator : ViewController
    {
        //当前生成时间
        private float currentGenerateSeconds = 0;
        //当前波次时间
        private float currentWaveSeconds = 0;
        //敌人数量
        public static BindableProperty<int> EnemyCount = new BindableProperty<int>(0);

        [SerializeField]
        public List<EnemyWave> EnemyWaves= new List<EnemyWave>();

        private Queue<EnemyWave> enemyWaveQueue=new Queue<EnemyWave>();

        public int WaveCount = 0;

        public bool LastWave=>WaveCount==EnemyWaves.Count;

        public EnemyWave CurrentWave => currentWave;


        private void Start()
        {
            //将敌人波次入队列
            foreach(var enemyEave in EnemyWaves)
            {
                enemyWaveQueue.Enqueue(enemyEave);
            }
        }

        private EnemyWave currentWave=null;

        private void Update()
        {
            if (currentWave == null)
            {
                if(enemyWaveQueue.Count > 0)
                {
                    WaveCount++;

                    currentWave=enemyWaveQueue.Dequeue();

                    currentGenerateSeconds = 0;

                    currentWaveSeconds = 0;
                }
            }
            if(currentWave != null)
            {
                //时间增加
                currentGenerateSeconds += Time.deltaTime;

                currentWaveSeconds += Time.deltaTime;  

              
                //如果时间达到1秒
                if (currentGenerateSeconds >= currentWave.GeneraDuration)
                {
                    //生成时间归零
                    currentGenerateSeconds = 0;

                   
                    //得到玩家
                    var player = Player.Default;
                    //如果玩家不为空
                    if (player)
                    {
                        //随机角度
                        var randomAngle = UnityEngine.Random.Range(0, 360f);
                        //将角度转为弧度
                        var randomRadius = randomAngle * Mathf.Deg2Rad;
                        //计算位置
                        var direction = new Vector3(Mathf.Cos(randomRadius), Mathf.Sin(randomRadius));
                        //随机生成位置
                        var generatePos = player.transform.position + direction * 10;
                        //生成敌人，设置位置，显示
                        currentWave.EnemyPrefab.Instantiate().Position(generatePos).Show();
                    }


                }
                if (currentWaveSeconds >= currentWave.Seconds)
                {
                    currentWave = null;
                }
            }
           
            
        }
    }
}
