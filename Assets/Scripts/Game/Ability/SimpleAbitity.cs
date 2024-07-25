using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class SimpleAbitity : ViewController
    {
        private float currentSeconds = 0;
        void Start()
        {
            // Code Here
        }

        private void Update()
        {
            currentSeconds += Time.deltaTime;

            if (currentSeconds >= Global.SimpleAbillityDuration.Value)
            {
                currentSeconds = 0;

                var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

                foreach (var enemy in enemies)
                {
                    var distance = (Player.Default.transform.position - enemy.transform.position).magnitude;

                    if (distance <= 5) 
                    {

                        enemy.Hurt(Global.SimpleAbillityDamage.Value);

                      
                    }
                }
              
            }
        }
    }
}
