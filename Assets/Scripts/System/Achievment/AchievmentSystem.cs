using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace ProjectSurvivor
{
    public class AchievementSystem : AbstractSystem
    {
        public AchievementItem Add(AchievementItem item)
        {
            Items.Add(item);
            return item;
        }

        protected override void OnInit()
        {
            var saveSystem = this.GetSystem<SaveSystem>();

            Add(new AchievementItem()
                    .WithKey("3_minutes")
                    .WithName("���������")
                    .WithDescription("��� 3 ����\n���� 1000 ���")
                    .WithIconName("achievement_time_icon")
                    .Condition(() => Global.CurrentSeconds.Value >= 60 * 3)
                    // .Condition(() => Global.CurrentSeconds.Value >= 10)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);

            Add(new AchievementItem()
                    .WithKey("5_minutes")
                    .WithName("��������")
                    .WithDescription("��� 5 ����\n���� 1000 ���")
                    .WithIconName("achievement_time_icon")
                    .Condition(() => Global.CurrentSeconds.Value >= 60 * 5)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);

            Add(new AchievementItem()
                    .WithKey("10_minutes")
                    .WithName("���ʮ����")
                    .WithDescription("��� 10 ����\n���� 1000 ���")
                    .WithIconName("achievement_time_icon")
                    .Condition(() => Global.CurrentSeconds.Value >= 60 * 10)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);

            Add(new AchievementItem()
                    .WithKey("15_minutes")
                    .WithName("��� 15 ����")
                    .WithDescription("��� 10 ����\n���� 1000 ���")
                    .WithIconName("achievement_time_icon")
                    .Condition(() => Global.CurrentSeconds.Value >= 60 * 15)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);

            Add(new AchievementItem()
                    .WithKey("lv30")
                    .WithName("30 ��")
                    .WithDescription("��һ�������� 30 ��\n���� 1000 ���")
                    .WithIconName("achievement_level_icon")
                    .Condition(() => Global.Level.Value >= 30)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);

            Add(new AchievementItem()
                    .WithKey("lv50")
                    .WithName("50 ��")
                    .WithDescription("��һ�������� 50 ��\n���� 1000 ���")
                    .WithIconName("achievement_level_icon")
                    .Condition(() => Global.Level.Value >= 50)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);

            Add(new AchievementItem()
                    .WithKey("first_time_paired_ball")
                    .WithName("�ϳɺ������")
                    .WithDescription("��һ�ν����ϳɺ������\n���� 1000 ���")
                    .WithIconName("paired_ball_icon")
                    .Condition(() => Global.SuperBasketBall.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);

            Add(new AchievementItem()
                    .WithKey("first_time_paired_bomb")
                    .WithName("�ϳɺ��ը��")
                    .WithDescription("��һ�ν����ϳɺ��ը��\n���� 1000 ���")
                    .WithIconName("paired_bomb_icon")
                    .Condition(() => Global.SuperBomb.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);

            Add(new AchievementItem()
                    .WithKey("first_time_paired_sword")
                    .WithName("�ϳɺ�Ľ�")
                    .WithDescription("��һ�ν����ϳɺ�Ľ�\n���� 1000 ���")
                    .WithIconName("paired_simple_sword_icon")
                    .Condition(() => Global.SuperSword.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);

            Add(new AchievementItem()
                    .WithKey("first_time_paired_knife")
                    .WithName("�ϳɺ�ķɵ�")
                    .WithDescription("��һ�ν����ϳɺ�ķɵ�\n���� 1000 ���")
                    .WithIconName("paired_simple_knife_icon")
                    .Condition(() => Global.SuperKnife.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);

            Add(new AchievementItem()
                    .WithKey("first_time_paired_circle")
                    .WithName("�ϳɺ��������")
                    .WithDescription("��һ�ν����ϳɺ��������\n���� 1000 ���")
                    .WithIconName("paired_rotate_sword_icon")
                    .Condition(() => Global.SuperRotateSword.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);

            Add(new AchievementItem()
                    .WithKey("first_time_paired_circle")
                    .WithName("ȫ����������")
                    .WithDescription("ȫ�������������\n���� 1000 ���")
                    .WithIconName("achievement_all_icon")
                    .Condition(() => ExpUpgradeSystem.AllUnlockedFinish)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);

            ActionKit.OnUpdate.Register(() =>
            {
                if (Time.frameCount % 10 == 0)
                {
                    foreach (var achievementItem in Items.Where(achievementItem =>
                                 !achievementItem.Unlocked && achievementItem.ConditionCheck()))
                    {
                        achievementItem.Unlock(saveSystem);
                    }
                }
            });
        }
        public List<AchievementItem> Items = new List<AchievementItem>();

        public static EasyEvent<AchievementItem> OnAchievementUnlocked = new EasyEvent<AchievementItem>();
    }
}