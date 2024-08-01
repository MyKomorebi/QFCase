using ProjectSurvivor;
using QFramework;
using System.Collections.Generic;
using UnityEngine;

public class CoinUpgradeSystem : AbstractSystem, ICanSvae
{

    public static EasyEvent OnCoinUpgradeSystem = new EasyEvent();
    public List<CoinUpgradeItem> Items { get; } = new List<CoinUpgradeItem>();

    public CoinUpgradeItem Add(CoinUpgradeItem item)
    {
        Items.Add(item);
        return item;
    }
    protected override void OnInit()
    {
        var coinPercentLv1 = Add(new CoinUpgradeItem()
             .WithKey("coin_percent")
             .WithDescription("金币掉落概率提升 Lv1")
             .WithPrice(5)
             .OnUpgrade((item) =>
             {
                 Global.CoinPercent.Value += 0.1f;
                 Global.Coin.Value -= item.Price;
             }));
        var coinPercentLv2 = Add(new CoinUpgradeItem()
            .WithKey("coin_percent")
            .WithDescription("金币掉落概率提升 Lv2")
            .WithPrice(7)
            .Condition((_) => coinPercentLv1.UpgradeFinish)
            .OnUpgrade((item) =>
            {
                Global.CoinPercent.Value += 0.1f;
                Global.Coin.Value -= item.Price;
            }));

        coinPercentLv1.Onchange.Register(() =>
        {
            coinPercentLv2.Onchange.Trigger();
        });
        var coinPercentLv3 = Add(new CoinUpgradeItem()
            .WithKey("coin_percent")
            .WithDescription("金币掉落概率提升 Lv3")
            .WithPrice(10)
              .Condition((_) => coinPercentLv2.UpgradeFinish)
            .OnUpgrade((item) =>
            {
                Global.CoinPercent.Value += 0.1f;
                Global.Coin.Value -= item.Price;
            }));

        coinPercentLv2.Onchange.Register(() =>
        {
            coinPercentLv3.Onchange.Trigger();
        });
        Items.Add(new CoinUpgradeItem()
            .WithKey("exp_percent")
            .WithDescription("经验值掉落概率提升")
            .WithPrice(5)
            .OnUpgrade((item) =>
            {
                Global.CoinPercent.Value += 0.1f;
                Global.Coin.Value -= item.Price;
            }));
        Items.Add(new CoinUpgradeItem()
            .WithKey("max_hp")
            .WithDescription("主角的最大血量+1")
            .WithPrice(30)
            .OnUpgrade((item) =>
            {
                Global.MaxHP.Value++;
                Global.Coin.Value -= item.Price;
            }));
        Load();
        OnCoinUpgradeSystem.Register(() =>
        {
            Save();
        });

    }

    public void Say()
    {
        Debug.Log("Hello CoinUpgradeSystem");
    }

    public void Save()
    {
        var saveSystem=this.GetSystem<SaveSystem>();
        foreach (var coinUpgradeItem in Items)
        {
            saveSystem.SaveBool(coinUpgradeItem.Key, coinUpgradeItem.UpgradeFinish);
        }
    }

    public void Load()
    {
        var saveSystem = this.GetSystem<SaveSystem>();
        foreach (var coinUpgradeItem in Items)
        {
            coinUpgradeItem.UpgradeFinish= saveSystem.LoadBool(coinUpgradeItem.Key,false);
        }
    }
}
