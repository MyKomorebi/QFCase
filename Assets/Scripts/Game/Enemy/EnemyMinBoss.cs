using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class EnemyMinBoss : ViewController,IEnemy
	{
        public enum States
        {
            FlowingPlayer,
            Warning,
            Dash,
            Wait
        }

        public float HP = 50f;
        //移动速度
        public float MovementSpeed = 2f;


        public FSM<States>FSM=new FSM<States>();
        void Start()
        {
            //敌人总数加一
            EnemyGenerator.EnemyCount.Value++;
            FSM.State(States.FlowingPlayer)
                .OnFixedUpdate(() =>
                {
                    //如果玩家存在
                    if (Player.Default)
                    {
                        //得到指向玩家的方向
                        var direction = (Player.Default.transform.position - transform.position).normalized;
                        //向玩家移动
                        SelfRigidbody2D.velocity = direction * MovementSpeed;

                        if ((Player.Default.Position() - transform.Position()).magnitude <= 15)
                        {
                            FSM.ChangeState(States.Warning);
                        }
                    }
                    else
                    {
                        SelfRigidbody2D.velocity = Vector2.zero;
                    }
                });

            FSM.State(States.Warning)
                .OnUpdate(() =>
                {
                    SelfRigidbody2D.velocity = Vector2.zero;
                })
                .OnUpdate(() =>
                {
                    var frames = 3 + (60 * 3-FSM.FrameCountOfCurrentState)/10;

                    if(FSM.FrameCountOfCurrentState / frames % 2 == 0)
                    {
                        Sprite.color = Color.red;
                    }
                    else
                    {
                        Sprite.color=Color.white;
                    }

                    if (FSM.FrameCountOfCurrentState >= 60*3)
                    {
                        FSM.ChangeState(States.Dash);
                    }
                })
                .OnExit(() =>
                {
                    Sprite.color = Color.white;
                });

            var dashStartPos=Vector3.zero;

            var dashStartDistanceToPlayer = 0f;
            FSM.State(States.Dash)
                .OnEnter(() =>
                {
                    var direction=(Player.Default.Position()-transform.Position()).normalized;
                    SelfRigidbody2D.velocity = direction * 15;
                    dashStartPos = transform.Position();
                    dashStartDistanceToPlayer = (Player.Default.Position() - transform.Position()).magnitude;
                })
                .OnUpdate(() =>
                {

                    var distance=(transform.Position() - dashStartPos).magnitude;

                    if (distance >= dashStartDistanceToPlayer +5)
                    {
                        FSM.ChangeState(States.Wait);
                    }
                   
                });

            FSM.State(States.Wait)
                .OnEnter(() =>
                {
                    SelfRigidbody2D.velocity = Vector2.zero;
                })
                .OnUpdate(() =>
                {
                    if (FSM.FrameCountOfCurrentState >= 30)
                    {
                        FSM.ChangeState(States.FlowingPlayer);
                    }
                });

            FSM.StartState(States.FlowingPlayer);

        }
        private void FixedUpdate()
        {

            FSM.FixedUpdate();
           
        }
        void Update()
        {
            FSM.Update();
            //如果敌人数量小于0
            if (HP <= 0)
            {
                //销毁自己
                this.DestroyGameObjGracefully();

                //触发经验值的掉落

                Global.GeneratePowerUp(this.gameObject);
            }

        }
        private void OnDestroy()
        {
            //敌人数量减一
            EnemyGenerator.EnemyCount.Value--;
        }
        private bool IgnoreHurt = false;
        public void Hurt(float value, bool force = false)
        {
            if (IgnoreHurt && !force) return;
            //播放伤害文字
            FloatingTextController.Play(transform.position, value.ToString());
            //显示红色
            Sprite.color = Color.red;
            //播放受伤动画
            AudioKit.PlaySound("Hit");
            //延迟0.2秒
            ActionKit.Delay(0.2f, () =>
            {
                //血量减少
                this.HP -= value;
                //显示白色
                this.Sprite.color = Color.white;

                IgnoreHurt = false;

            }).Start(this);
        }

        public void SetHPScale(float hPScale)
        {
           HP *= hPScale;
        }

        public void SetSpeedScale(float speedScale)
        {
           MovementSpeed *= speedScale;
        }
    }
}
