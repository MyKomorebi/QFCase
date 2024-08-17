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
        //技能伤害
        public static BindableProperty<float> SimpleAbillityDamage = new BindableProperty<float>(1);
        //技能持续时间
        public static BindableProperty<float> SimpleAbillityDuration = new BindableProperty<float>(1.5f);
        //经验概率
        public static BindableProperty<float> Expercent = new BindableProperty<float>(0.3f);
        //金币概率
        public static BindableProperty<float> CoinPercent = new BindableProperty<float>(0.05f);

        #endregion

        [RuntimeInitializeOnLoadMethod]
       //初始化
        public static void AutoInit()
        {
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
        public static void GeneratePowerUp(GameObject gameObject)
        {
          
            //经验值掉落
            var percent = Random.Range(0, 1f);

            if (percent < Expercent.Value)
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
            if (percent < 0.1f)
            {
                PowerUpManager.Default.HP.Instantiate()
                   .Position(gameObject.Position())
                   .Show();
                return;

            }
            //炸弹掉落
            percent = Random.Range(0, 1f);
            if (percent < 0.1f)
            {
                PowerUpManager.Default.Bomb.Instantiate()
                   .Position(gameObject.Position())
                   .Show();
                return;
            }
            //获取所有经验值掉落
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
            SimpleAbillityDamage.Value = 1;
            //当前技能持续时间重置
            SimpleAbillityDuration.Value = 1.5f;
            //敌人数量重置
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

