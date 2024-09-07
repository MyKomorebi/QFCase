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
        //血量
        public static BindableProperty<int> HP = new BindableProperty<int>(3);
        //最大血量
        public static BindableProperty<int> MaxHP = new BindableProperty<int>(3);
        //经验值
        public static BindableProperty<int> Exp = new BindableProperty<int>(0);
        //金币
        public static BindableProperty<int> Coin = new BindableProperty<int>(0);
        //等级
        public static BindableProperty<int> Level = new BindableProperty<int>(1);
        //当前秒
        public static BindableProperty<float> CurrentSeconds = new BindableProperty<float>(0);

        public static BindableProperty<bool>SimpleSwordUnlocked = new BindableProperty<bool>(false);
        //技能伤害
        public static BindableProperty<float> SimpleAbillityDamage = new(Config.InitSimpleSwordDamage);
        //技能持续时间
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
        //经验概率
        public static BindableProperty<float> Expercent = new BindableProperty<float>(0.3f);
        //金币概率
        public static BindableProperty<float> CoinPercent = new BindableProperty<float>(0.05f);

        #endregion

        [RuntimeInitializeOnLoadMethod]
       //初始化
        public static void AutoInit()
        {
            AudioKit.PlaySoundMode=PlaySoundModes.IgnoreSameSoundInGlobalFrames ;
            //初始化ResKit
            ResKit.Init();
            //设置UIRootCanvas的大小
            UIKit.Root.SetResolution(1920, 1080, 1);
            //初始化最大血量，血量
            Global.MaxHP.Value = PlayerPrefs.GetInt(nameof(MaxHP), 3);
            HP.Value = MaxHP.Value;
            //初始化金币
            Global.Coin.Value = PlayerPrefs.GetInt("coin", 0);

            //初始化经验概率
            Global.Expercent.Value = PlayerPrefs.GetFloat(nameof(Expercent), 0.4f);
            //初始化金币概率
            Global.CoinPercent.Value = PlayerPrefs.GetFloat(nameof(CoinPercent), 0.1f);
            //注册金币变化事件
            Global.Coin.Register(coin =>
            {
                //保存金币数量
                PlayerPrefs.SetInt(nameof(coin), coin);
            });
            //注册经验概率变化事件
            Global.Expercent.Register(expercent =>
            {
                //保存经验概率
                PlayerPrefs.SetFloat(nameof(Expercent), expercent);
            });
            //注册金币概率变化事件
            Global.CoinPercent.Register(coinPercent =>
            {
                //保存金币概率
                PlayerPrefs.SetFloat(nameof(CoinPercent), coinPercent);
            });
            //注册最大血量变化事件
            Global.MaxHP.Register(maxHP =>
            {
                //保存最大血量
                PlayerPrefs.SetInt(nameof(MaxHP), maxHP);
            });
            var _ = Interface;
        }
        /// <summary>
        /// 升级需要的经验
        /// </summary>
        /// <returns></returns>
        public static int ExpToNext()
        {
            return Level.Value * 5;
        }
        /// <summary>
        /// 掉落物生成
        /// </summary>
        /// <param name="gameObject">生成对象</param>
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
            //经验值掉落
            var percent = Random.Range(0, 1f);

            if (percent < Expercent.Value+AdditionalExpPercent.Value)
            {
                PowerUpManager.Default.Exp.Instantiate()
                   .Position(gameObject.Position())
                   .Show();
                return;
            }
            //金币掉落
            percent = Random.Range(0, 1f);

            if (percent < CoinPercent.Value)
            {
                PowerUpManager.Default.Coin.Instantiate()
                   .Position(gameObject.Position())
                   .Show();
                return;
            }
            //血量回复掉落
            percent = Random.Range(0, 1f);
            if (percent < 0.1f&&!GameObject.FindObjectOfType<HP>())
            {
                PowerUpManager.Default.HP.Instantiate()
                   .Position(gameObject.Position())
                   .Show();
                return;

            }
            //炸弹掉落

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
            
            //获取所有经验值掉落
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
        /// 数据重置
        /// </summary>

        public static void RestData()
        {

            //血量重置
            HP.Value = MaxHP.Value;
            //经验重置
            Exp.Value = 0;
            //等级重置
            Level.Value = 1;
            //当前秒重置
            CurrentSeconds.Value = 0;
            //当前技能伤害重置
            SimpleAbillityDamage.Value = Config.InitSimpleSwordDamage;
            //当前技能持续时间重置
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
            //敌人数量重置
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

