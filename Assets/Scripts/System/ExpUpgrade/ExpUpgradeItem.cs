using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpUpgradeItem 
{

    public ExpUpgradeItem(bool isWeapon)
    {
        IsWeapon = isWeapon;
    }
  public bool IsWeapon=false;
    public bool UpgradeFinish { get; set; } = false;
    public string Key { get; private set; }

    public string Name {  get; private set; }
    public string Description =>mDescriptionFactory(CurrentLevel.Value+1);

   public int MaxLevel { get;private set; }

    public string IconName {  get; private set; }   

    public BindableProperty<int> CurrentLevel=new BindableProperty<int>(0);

    public BindableProperty<bool>Visible=new BindableProperty<bool>();

    public Func<int, string> mDescriptionFactory;

    public void Upgrade()
    {
        CurrentLevel.Value++;
        mOnUpgrade?.Invoke(this,CurrentLevel.Value);
        if (CurrentLevel.Value > 10)
        {
            UpgradeFinish = true;
        }
      
        
    }

   

    private Action<ExpUpgradeItem,int> mOnUpgrade;

   

    public ExpUpgradeItem WithKey(string key)
    {
        Key = key;
        return this;
    }
    public ExpUpgradeItem WithName(string name)
    {
        Name = name; 
        return this;
    }
    public ExpUpgradeItem WithIconName(string iconName)
    {
        IconName = iconName;
        return this;
    }
    public string PairedName {  get; private set; }
    public string PairedDescription {  get; private set; }
    public string PairedIconName {  get; private set; }
    public ExpUpgradeItem WithPairedName(string pairedName)
    {
        PairedName = pairedName;
        return this;
    }

    public ExpUpgradeItem WithPairedIconName(string pairedIconName)
    {
        PairedIconName = pairedIconName;
        return this;
    }
    public ExpUpgradeItem WithPairedDescription(string pairedDescription)
    {
        PairedDescription = pairedDescription;
        return this;
    }
    public ExpUpgradeItem WithDescription(Func<int,string> descriptionFactory)
    {
        mDescriptionFactory = descriptionFactory;
        return this;
    }

   

    public ExpUpgradeItem OnUpgrade(Action<ExpUpgradeItem,int> onUpgrade)
    {
        mOnUpgrade = onUpgrade;
        return this;
    }
   
    public ExpUpgradeItem WithMaxLevel(int maxLevel)
    {
        MaxLevel = maxLevel;
        return this;
    }
}
