using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class SimpleAbitity : ViewController
    {
        //当前秒
        private float currentSeconds = 0;
       

        private void Update()
        {
            currentSeconds += Time.deltaTime;
            //如果到达了时间

            if (currentSeconds >= Global.SimpleAbillityDuration.Value)
            {
                //当前时间重置
                currentSeconds = 0;
                //获取所有的敌人
                var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
                //遍历获取的敌人
                foreach (var enemy in enemies)
                {
                    //计算与敌人的距离
                    var distance = (Player.Default.transform.position - enemy.transform.position).magnitude;
                    //如果距离小于5
                    if (distance <= 5) 
                    {
                        //敌人受伤
                        enemy.Hurt(Global.SimpleAbillityDamage.Value);

                      
                    }
                }
              
            }
        }
    }
}
