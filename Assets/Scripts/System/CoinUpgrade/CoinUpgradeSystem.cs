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
        Add(new CoinUpgradeItem()
             .WithKey("coin_percent_lv1")
             .WithDescription("��ҵ���������� Lv1")
             .WithPrice(100)
             .OnUpgrade((item) =>
             {
                 Global.CoinPercent.Value += 0.1f;
                 Global.Coin.Value -= item.Price;
             })
             .Next(Add(new CoinUpgradeItem()
                .WithKey("coin_percent_lv2")
                .WithDescription("��ҵ���������� Lv2")
                .WithPrice(1000)
            //.Condition((_) => coinPercentLv1.UpgradeFinish)
                .OnUpgrade((item) =>
                {
                    Global.CoinPercent.Value += 0.1f;
                    Global.Coin.Value -= item.Price;
                }))))
            .Next(Add(new CoinUpgradeItem()
                .WithKey("coin_percent_lv3")
                .WithDescription("��ҵ���������� Lv3")
                .WithPrice(2000)
                 // .Condition((_) => coinPercentLv2.UpgradeFinish)
                .OnUpgrade((item) =>
                {
                    Global.CoinPercent.Value += 0.1f;
                    Global.Coin.Value -= item.Price;
                })))
            .Next(Add(new CoinUpgradeItem()
                .WithKey("coin_percent_lv4")
                .WithDescription("��ҵ���������� Lv4")
                .WithPrice(5000)
                // .Condition((_) => coinPercentLv2.UpgradeFinish)
                .OnUpgrade((item) =>
                {
                    Global.CoinPercent.Value += 0.1f;
                    Global.Coin.Value -= item.Price;
                })));
       

       
        Items.Add(new CoinUpgradeItem()
            .WithKey("exp_percent")
            .WithDescription("����ֵ�����������")
            .WithPrice(5)
            .OnUpgrade((item) =>
            {
                Global.CoinPercent.Value += 0.1f;
                Global.Coin.Value -= item.Price;
            }));
        Add(new CoinUpgradeItem()
            .WithKey("max_hp")
            .WithDescription("���ǵ����Ѫ��+1")
            .WithPrice(1000)
            .OnUpgrade((item) =>
            {
                Global.MaxHP.Value++;
                Global.Coin.Value -= item.Price;
            }))
            .Next(Add(new CoinUpgradeItem()
                .WithKey("max_hp1")
                .WithDescription("���ǵ����Ѫ��+1")
                .WithPrice(2000)
                .OnUpgrade((item) =>
                {
                    Global.MaxHP.Value++;
                    Global.Coin.Value -= item.Price;
                })))
            .Next(Add(new CoinUpgradeItem()
                .WithKey("max_hp2")
                .WithDescription("���ǵ����Ѫ��+1")
                .WithPrice(3000)
                .OnUpgrade((item) =>
                {
                    Global.MaxHP.Value++;
                    Global.Coin.Value -= item.Price;
                })))
            .Next(Add(new CoinUpgradeItem()
                .WithKey("max_hp3")
                .WithDescription("���ǵ����Ѫ��+1")
                .WithPrice(4000)
                .OnUpgrade((item) =>
                {
                    Global.MaxHP.Value++;
                    Global.Coin.Value -= item.Price;
                })))
            .Next(Add(new CoinUpgradeItem()
                .WithKey("max_hp4")
                .WithDescription("���ǵ����Ѫ��+1")
                .WithPrice(5000)
                .OnUpgrade((item) =>
                {
                    Global.MaxHP.Value++;
                    Global.Coin.Value -= item.Price;
                })))
            .Next(Add(new CoinUpgradeItem()
                .WithKey("max_hp5")
                .WithDescription("���ǵ����Ѫ��+1")
                .WithPrice(6000)
                .OnUpgrade((item) =>
                {
                    Global.MaxHP.Value++;
                    Global.Coin.Value -= item.Price;
                })))
            .Next(Add(new CoinUpgradeItem()
                .WithKey("max_hp6")
                .WithDescription("���ǵ����Ѫ��+1")
                .WithPrice(7000)
                .OnUpgrade((item) =>
                {
                    Global.MaxHP.Value++;
                    Global.Coin.Value -= item.Price;
                })))
            .Next(Add(new CoinUpgradeItem()
                .WithKey("max_hp7")
                .WithDescription("���ǵ����Ѫ��+1")
                .WithPrice(8000)
                .OnUpgrade((item) =>
                {
                    Global.MaxHP.Value++;
                    Global.Coin.Value -= item.Price;
                })))
            .Next(Add(new CoinUpgradeItem()
                .WithKey("max_hp8")
                .WithDescription("���ǵ����Ѫ��+1")
                .WithPrice(9000)
                .OnUpgrade((item) =>
                {
                    Global.MaxHP.Value++;
                    Global.Coin.Value -= item.Price;
                })))
            .Next(Add(new CoinUpgradeItem()
                .WithKey("max_hp10")
                .WithDescription("���ǵ����Ѫ��+1")
                .WithPrice(10000)
                .OnUpgrade((item) =>
                {
                    Global.MaxHP.Value++;
                    Global.Coin.Value -= item.Price;
                })));
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
