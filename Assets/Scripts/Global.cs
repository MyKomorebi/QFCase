using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
namespace ProjectSurvivor
{
    public class Global : Architecture<Global>
    {
        #region Model
        //Ѫ��
        public static BindableProperty<int> HP = new BindableProperty<int>(3);
        //���Ѫ��
        public static BindableProperty<int> MaxHP = new BindableProperty<int>(3);
        //����ֵ
        public static BindableProperty<int> Exp = new BindableProperty<int>(0);
        //���
        public static BindableProperty<int> Coin = new BindableProperty<int>(0);
        //�ȼ�
        public static BindableProperty<int> Level = new BindableProperty<int>(1);
        //��ǰ��
        public static BindableProperty<float> CurrentSeconds = new BindableProperty<float>(0);
        //�����˺�
        public static BindableProperty<float> SimpleAbillityDamage = new BindableProperty<float>(1);
        //���ܳ���ʱ��
        public static BindableProperty<float> SimpleAbillityDuration = new BindableProperty<float>(1.5f);
        //�������
        public static BindableProperty<float> Expercent = new BindableProperty<float>(0.3f);
        //��Ҹ���
        public static BindableProperty<float> CoinPercent = new BindableProperty<float>(0.05f);

        #endregion

        [RuntimeInitializeOnLoadMethod]
       //��ʼ��
        public static void AutoInit()
        {
            //��ʼ��ResKit
            ResKit.Init();
            //����UIRootCanvas�Ĵ�С
            UIKit.Root.SetResolution(1920, 1080, 1);
            //��ʼ�����Ѫ����Ѫ��
            Global.MaxHP.Value = PlayerPrefs.GetInt(nameof(MaxHP), 3);
            HP.Value = MaxHP.Value;
            //��ʼ�����
            Global.Coin.Value = PlayerPrefs.GetInt("coin", 0);

            //��ʼ���������
            Global.Expercent.Value = PlayerPrefs.GetFloat(nameof(Expercent), 0.4f);
            //��ʼ����Ҹ���
            Global.CoinPercent.Value = PlayerPrefs.GetFloat(nameof(CoinPercent), 0.1f);
            //ע���ұ仯�¼�
            Global.Coin.Register(coin =>
            {
                //����������
                PlayerPrefs.SetInt(nameof(coin), coin);
            });
            //ע�ᾭ����ʱ仯�¼�
            Global.Expercent.Register(expercent =>
            {
                //���澭�����
                PlayerPrefs.SetFloat(nameof(Expercent), expercent);
            });
            //ע���Ҹ��ʱ仯�¼�
            Global.CoinPercent.Register(coinPercent =>
            {
                //�����Ҹ���
                PlayerPrefs.SetFloat(nameof(CoinPercent), coinPercent);
            });
            //ע�����Ѫ���仯�¼�
            Global.MaxHP.Register(maxHP =>
            {
                //�������Ѫ��
                PlayerPrefs.SetInt(nameof(MaxHP), maxHP);
            });
            var _ = Interface;
        }
        /// <summary>
        /// ������Ҫ�ľ���
        /// </summary>
        /// <returns></returns>
        public static int ExpToNext()
        {
            return Level.Value * 5;
        }
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="gameObject">���ɶ���</param>
        public static void GeneratePowerUp(GameObject gameObject)
        {
          
            //����ֵ����
            var percent = Random.Range(0, 1f);

            if (percent < Expercent.Value)
            {
                PowerUpManager.Default.Exp.Instantiate()
                   .Position(gameObject.Position())
                   .Show();
                return;
            }
            //��ҵ���
            percent = Random.Range(0, 1f);

            if (percent < CoinPercent.Value)
            {
                PowerUpManager.Default.Coin.Instantiate()
                   .Position(gameObject.Position())
                   .Show();
                return;
            }
            //Ѫ���ظ�����
            percent = Random.Range(0, 1f);
            if (percent < 0.1f)
            {
                PowerUpManager.Default.HP.Instantiate()
                   .Position(gameObject.Position())
                   .Show();
                return;

            }
            //ը������
            percent = Random.Range(0, 1f);
            if (percent < 0.1f)
            {
                PowerUpManager.Default.Bomb.Instantiate()
                   .Position(gameObject.Position())
                   .Show();
                return;
            }
            //��ȡ���о���ֵ����
            percent = Random.Range(0, 1f);
            if (percent < 0.1f)
            {
                PowerUpManager.Default.GetAllExp.Instantiate()
                   .Position(gameObject.Position())
                   .Show();
                return;
            }

        }
        /// <summary>
        /// ��������
        /// </summary>

        public static void RestData()
        {

            //Ѫ������
            HP.Value = MaxHP.Value;
            //��������
            Exp.Value = 0;
            //�ȼ�����
            Level.Value = 1;
            //��ǰ������
            CurrentSeconds.Value = 0;
            //��ǰ�����˺�����
            SimpleAbillityDamage.Value = 1;
            //��ǰ���ܳ���ʱ������
            SimpleAbillityDuration.Value = 1.5f;
            //������������
            EnemyGenerator.EnemyCount.Value = 0;

            Interface.GetSystem<ExpUpgradeSystem>().ResetData();

        }

        protected override void Init()
        {

            this.RegisterSystem(new ExpUpgradeSystem());
            this.RegisterSystem(new SaveSystem());
            this.RegisterSystem(new CoinUpgradeSystem());
        }
    }
}

