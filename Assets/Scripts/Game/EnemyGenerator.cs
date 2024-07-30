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
        //��ǰ����ʱ��
        private float currentGenerateSeconds = 0;
        //��ǰ����ʱ��
        private float currentWaveSeconds = 0;
        //��������
        public static BindableProperty<int> EnemyCount = new BindableProperty<int>(0);

        [SerializeField]
        public List<EnemyWave> EnemyWaves= new List<EnemyWave>();

        private Queue<EnemyWave> enemyWaveQueue=new Queue<EnemyWave>();

        public int WaveCount = 0;

        public bool LastWave=>WaveCount==EnemyWaves.Count;

        public EnemyWave CurrentWave => currentWave;


        private void Start()
        {
            //�����˲��������
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
                //ʱ������
                currentGenerateSeconds += Time.deltaTime;

                currentWaveSeconds += Time.deltaTime;  

              
                //���ʱ��ﵽ1��
                if (currentGenerateSeconds >= currentWave.GeneraDuration)
                {
                    //����ʱ�����
                    currentGenerateSeconds = 0;

                   
                    //�õ����
                    var player = Player.Default;
                    //�����Ҳ�Ϊ��
                    if (player)
                    {
                        //����Ƕ�
                        var randomAngle = UnityEngine.Random.Range(0, 360f);
                        //���Ƕ�תΪ����
                        var randomRadius = randomAngle * Mathf.Deg2Rad;
                        //����λ��
                        var direction = new Vector3(Mathf.Cos(randomRadius), Mathf.Sin(randomRadius));
                        //�������λ��
                        var generatePos = player.transform.position + direction * 10;
                        //���ɵ��ˣ�����λ�ã���ʾ
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
