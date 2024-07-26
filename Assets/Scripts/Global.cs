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

        public static BindableProperty<int> HP = new BindableProperty<int>(3);

        public static BindableProperty<int> MaxHP = new BindableProperty<int>(3);
        //经验值
        public static BindableProperty<int> Exp = new BindableProperty<int>(0);

        public static BindableProperty<int> Coin = new BindableProperty<int>(0);
        //等级
        public static BindableProperty<int> Level = new BindableProperty<int>(1);

        public static BindableProperty<float> CurrentSeconds = new BindableProperty<float>(0);

        public static BindableProperty<float> SimpleAbillityDamage = new BindableProperty<float>(1);

        public static BindableProperty<float> SimpleAbillityDuration = new BindableProperty<float>(1.5f);

        public static BindableProperty<float> Expercent = new BindableProperty<float>(0.3f);

        public static BindableProperty<float> CoinPercent = new BindableProperty<float>(0.05f);

        #endregion

        [RuntimeInitializeOnLoadMethod]
        public static void AutoInit()
        {
            ResKit.Init();
            UIKit.Root.SetResolution(1920, 1080, 1);

            Global.MaxHP.Value = PlayerPrefs.GetInt(nameof(MaxHP), 3);
            HP.Value = MaxHP.Value;

            Global.Coin.Value = PlayerPrefs.GetInt("coin", 0);


            Global.Expercent.Value = PlayerPrefs.GetFloat(nameof(Expercent), 0.4f);

            Global.CoinPercent.Value = PlayerPrefs.GetFloat(nameof(CoinPercent), 0.1f);

            Global.Coin.Register(coin =>
            {
                PlayerPrefs.SetInt(nameof(coin), coin);
            });
            Global.Expercent.Register(expercent =>
            {
                PlayerPrefs.SetFloat(nameof(Expercent), expercent);
            });
            Global.CoinPercent.Register(coinPercent =>
            {
                PlayerPrefs.SetFloat(nameof(CoinPercent), coinPercent);
            });

            Global.MaxHP.Register(maxHP =>
            {
                PlayerPrefs.SetInt(nameof(MaxHP), maxHP);
            });
        }
        public static int ExpToNext()
        {
            return Level.Value * 5;
        }

        public static void GeneratePowerUp(GameObject gameObject)
        {
            //90%掉落经验值

            var percent = Random.Range(0, 1f);

            if (percent < Expercent.Value)
            {
                PowerUpManager.Default.Exp.Instantiate()
                   .Position(gameObject.Position())
                   .Show();
                return;
            }
            percent = Random.Range(0, 1f);

            if (percent < CoinPercent.Value)
            {
                PowerUpManager.Default.Coin.Instantiate()
                   .Position(gameObject.Position())
                   .Show();
                return;
            }
            percent = Random.Range(0, 1f);
            if (percent < 0.1f)
            {
                PowerUpManager.Default.HP.Instantiate()
                   .Position(gameObject.Position())
                   .Show();
                return;

            }
            percent = Random.Range(0, 1f);
            if (percent < 0.1f)
            {
                PowerUpManager.Default.Bomb.Instantiate()
                   .Position(gameObject.Position())
                   .Show();
                return;
            }
            percent = Random.Range(0, 1f);
            if (percent < 0.1f)
            {
                PowerUpManager.Default.GetAllExp.Instantiate()
                   .Position(gameObject.Position())
                   .Show();
                return;
            }

        }

        public static void RestData()
        {

            HP.Value = MaxHP.Value;
            Exp.Value = 0;

            Level.Value = 1;

            CurrentSeconds.Value = 0;

            SimpleAbillityDamage.Value = 1;

            SimpleAbillityDuration.Value = 1.5f;

            EnemyGenerator.EnemyCount.Value = 0;

        }

        protected override void Init()
        {

        }
    }
}

