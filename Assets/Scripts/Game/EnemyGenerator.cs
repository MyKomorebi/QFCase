using QFramework;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSurvivor
{


    public partial class EnemyGenerator : ViewController
    {

        public LevelConfig LevelConfig;
        //当前生成时间
        private float currentGenerateSeconds = 0;
        //当前波次时间
        private float currentWaveSeconds = 0;
        //敌人数量
        public static BindableProperty<int> EnemyCount = new BindableProperty<int>(0);

       

        private Queue<EnemyWave> enemyWaveQueue=new Queue<EnemyWave>();

        public int WaveCount = 0;

        private int mTotalCount = 0;
        public bool LastWave => WaveCount == mTotalCount;

        public EnemyWave CurrentWave => currentWave;


        private void Start()
        {

            foreach(var group in LevelConfig.EnemyWaveGroups)
            {
                //将敌人波次入队列
                foreach (var wave in group.Waves)
                {
                    enemyWaveQueue.Enqueue(wave);
                    mTotalCount++;
                }
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
                if (currentGenerateSeconds >= currentWave.GenerateDuration) 
                {
                    //生成时间归零
                    currentGenerateSeconds = 0;

                   
                    //得到玩家
                    var player = Player.Default;
                    //如果玩家不为空
                    if (player)
                    {
                        var xOry = RandomUtility.Choose(-1, 1);

                        var pos = Vector2.zero;

                        if (xOry == -1)
                        {
                            pos.x = RandomUtility.Choose(CameraController.LBTrans.position.x,
                                CameraController.RTTrans.position.x);
                            pos.y = Random.Range(CameraController.LBTrans.position.y,
                                CameraController.RTTrans.position.y);
                        }
                        else
                        {
                            pos.x = Random.Range(CameraController.LBTrans.position.x,
                               CameraController.RTTrans.position.x);
                            pos.y = RandomUtility.Choose(CameraController.LBTrans.position.y,
                                CameraController.RTTrans.position.y);
                        }

                        //生成敌人，设置位置，显示
                        currentWave.EnemyPrefab.Instantiate()
                            .Position(pos)
                            .Self(self =>
                            {
                                var enemy=self.GetComponent<IEnemy>();
                                enemy.SetSpeedScale(currentWave.SpeedScale);
                                enemy.SetHPScale(currentWave.HPScale);
                            })
                             .Show();
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
