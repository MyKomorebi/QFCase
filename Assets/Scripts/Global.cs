using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
namespace ProjectSurvivor
{
    public class Global
    {
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

        [RuntimeInitializeOnLoadMethod]
        public static void AutoInit()
        {
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
            }
            percent = Random.Range(0, 1f);

            if (percent < CoinPercent.Value)
            {
                PowerUpManager.Default.Coin.Instantiate()
                   .Position(gameObject.Position())
                   .Show();
            }
           
        }

        public static void RestData()
        {
            Exp.Value = 0;

            Level.Value = 1;

            CurrentSeconds.Value = 0;

            SimpleAbillityDamage.Value = 1;

            SimpleAbillityDuration.Value = 1.5f;

            EnemyGenerator.EnemyCount.Value = 0;

        }
    }
}

