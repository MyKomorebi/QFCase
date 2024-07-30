using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class SimpleAbitity : ViewController
    {
        //��ǰ��
        private float currentSeconds = 0;
       

        private void Update()
        {
            currentSeconds += Time.deltaTime;
            //���������ʱ��

            if (currentSeconds >= Global.SimpleAbillityDuration.Value)
            {
                //��ǰʱ������
                currentSeconds = 0;
                //��ȡ���еĵ���
                var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
                //������ȡ�ĵ���
                foreach (var enemy in enemies)
                {
                    //��������˵ľ���
                    var distance = (Player.Default.transform.position - enemy.transform.position).magnitude;
                    //�������С��5
                    if (distance <= 5) 
                    {
                        //��������
                        enemy.Hurt(Global.SimpleAbillityDamage.Value);

                      
                    }
                }
              
            }
        }
    }
}
