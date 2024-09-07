using QFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;
using static QFramework.AudioKit;
namespace ProjectSurvivor
{
    public class Global : Architecture<Global>
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Tool/Clear All Data")]
        public static void ClearAllData()
        {
            PlayerPrefs.DeleteAll();
        }
#endif
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

        public static BindableProperty<bool>SimpleSwordUnlocked = new BindableProperty<bool>(false);
        //�����˺�
        public static BindableProperty<float> SimpleAbillityDamage = new(Config.InitSimpleSwordDamage);
        //���ܳ���ʱ��
        public static BindableProperty<float> SimpleAbillityDuration = new(Config.InitSimpleSwordDuration);
        public static BindableProperty<int> SimpleSwordCount = new(Config.InitSimpleSwordCount);
        public static BindableProperty<float> SimpleSwordRange = new(Config.InitSimpleSwordRange);

        public static BindableProperty<bool> BombUnlocked = new(false);
        public static BindableProperty<float> BombDamage = new(Config.InitBombDamage);
        public static BindableProperty<float> BombPercent = new(Config.InitBombPercent);

        public static BindableProperty<bool>SimpleKnifeUnlocked=new BindableProperty<bool>(false);
        public static BindableProperty<float> SimpleKinfeDamage = new(Config.InitSimpleKnifeDamage);
        public static BindableProperty<float> SimpleKinfeDuration = new(Config.InitSimpleKnifeDuration);
        public static BindableProperty<int> SimpleKinfeCount = new(Config.InitSimpleKnifeCount);
        public static BindableProperty<int> SimpleKinfeAttackCount = new BindableProperty<int>(1);

        public static BindableProperty<bool>RotateSwordUnlocked=new BindableProperty<bool>(false);
        public static BindableProperty<float>RotateSwordDamage=new(Config.InitRotateSwordDamage);
        public static BindableProperty<int> RotateSwordCount = new(Config.InitRotateSwordCount);
        public static BindableProperty<float> RotateSwordSpeed = new(Config.InitRotateSwordSpeed);
        public static BindableProperty<float>RotateSwordRange=new(Config.InitRotateSwordRange);

        public static BindableProperty<bool>BasketBallUnlocked=new BindableProperty<bool>(false);
        public static BindableProperty<float> BasketBallDamage = new(Config.InitBasketBallDamage);
        public static BindableProperty<float> BasketBallSpeed = new(Config.InitBasketBallSpeed);
        public static BindableProperty<int> BasketBallCount = new(Config.InitBasketBallCount);

        public static BindableProperty<float>CriticalRate=new BindableProperty<float>(Config.InitCriticalRate);
        public static BindableProperty<float>DamageRate=new BindableProperty<float>(1);
        public static BindableProperty<int>AdditionalFlyThingCount=new BindableProperty<int>(0);

        public static BindableProperty<float> CollectableArea = new(Config.InitCollectableArea);
        public static BindableProperty<float> MovementSpeedRate = new BindableProperty<float>(1.0f);
        public static BindableProperty<float> AdditionalExpPercent = new(0);

        public static BindableProperty<bool> SuperKnife = new(false);
        public static BindableProperty<bool> SuperSword = new(false);
        public static BindableProperty<bool> SuperRotateSword = new(false);
        public static BindableProperty<bool> SuperBomb = new(false);
        public static BindableProperty<bool> SuperBasketBall = new(false);
        //�������
        public static BindableProperty<float> Expercent = new BindableProperty<float>(0.3f);
        //��Ҹ���
        public static BindableProperty<float> CoinPercent = new BindableProperty<float>(0.05f);

        #endregion

        [RuntimeInitializeOnLoadMethod]
       //��ʼ��
        public static void AutoInit()
        {
            AudioKit.PlaySoundMode=PlaySoundModes.IgnoreSameSoundInGlobalFrames ;
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
        public static void GeneratePowerUp(GameObject gameObject,bool genTreasureChest)
        {
          if(genTreasureChest)
            {
                PowerUpManager.Default.TreasureChest
                    .Instantiate()
                    .Position(gameObject.Position())
                    .Show();
                return;

            }
            //����ֵ����
            var percent = Random.Range(0, 1f);

            if (percent < Expercent.Value+AdditionalExpPercent.Value)
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
            if (percent < 0.1f&&!GameObject.FindObjectOfType<HP>())
            {
                PowerUpManager.Default.HP.Instantiate()
                   .Position(gameObject.Position())
                   .Show();
                return;

            }
            //ը������

            if (BombUnlocked.Value && !GameObject.FindObjectOfType<Bomb>())
            {
                percent = Random.Range(0, 1f);
                if (percent < BombPercent.Value)
                {
                    PowerUpManager.Default.Bomb.Instantiate()
                       .Position(gameObject.Position())
                       .Show();
                    return;
                }
            }
            
            //��ȡ���о���ֵ����
            percent = Random.Range(0, 1f);
            if (percent < 0.1f&&!GameObject.FindObjectOfType<GetAllExp>())
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
            SimpleAbillityDamage.Value = Config.InitSimpleSwordDamage;
            //��ǰ���ܳ���ʱ������
            SimpleSwordUnlocked.Value = false;
            SimpleAbillityDuration.Value =Config.InitSimpleSwordDuration;
            SimpleSwordCount.Value= Config.InitSimpleSwordCount;
            SimpleSwordRange.Value= Config.InitSimpleSwordRange;


            BombPercent.Value = Config.InitBombPercent;
            BombUnlocked.Value = false;
            BombDamage.Value = Config.InitBombDamage;

            SimpleKnifeUnlocked.Value = false;
            SimpleKinfeDamage.Value = Config.InitSimpleKnifeDamage;
            SimpleKinfeDuration.Value=Config.InitSimpleKnifeDuration;
            SimpleKinfeCount.Value= Config.InitSimpleKnifeCount;
            SimpleKinfeAttackCount.Value = 1;

            RotateSwordUnlocked.Value = false;
         RotateSwordDamage.Value=Config.InitRotateSwordDamage;
            RotateSwordCount.Value= Config.InitRotateSwordCount;
            RotateSwordSpeed.Value= Config.InitRotateSwordSpeed;
            RotateSwordRange.Value= Config.InitRotateSwordRange;

            BasketBallUnlocked.Value = false;
            BasketBallDamage.Value = Config.InitBasketBallDamage;
            BasketBallCount.Value = Config.InitBasketBallCount;
            BasketBallSpeed.Value = Config.InitBasketBallSpeed;

            CriticalRate.Value = Config.InitCriticalRate;
            AdditionalFlyThingCount.Value = 0;

            CollectableArea.Value = Config.InitCollectableArea;
            DamageRate.Value = 1;
            AdditionalExpPercent.Value = 0;
            SuperKnife.Value = false;
            SuperBomb.Value = false;
            SuperRotateSword.Value = false;
            SuperSword.Value = false;
            SuperBasketBall.Value = false;

            MovementSpeedRate.Value = 1;
            //������������
            EnemyGenerator.EnemyCount.Value = 0;

            Interface.GetSystem<ExpUpgradeSystem>().ResetData();

        }

        protected override void Init()
        {

            this.RegisterSystem(new ExpUpgradeSystem());
            this.RegisterSystem(new SaveSystem());
            this.RegisterSystem(new CoinUpgradeSystem());
            this.RegisterSystem(new AchievementSystem());
        }
    }
}

