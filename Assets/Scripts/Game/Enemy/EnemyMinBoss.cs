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
        //�ƶ��ٶ�
        public float MovementSpeed = 2f;


        public FSM<States>FSM=new FSM<States>();
        void Start()
        {
            //����������һ
            EnemyGenerator.EnemyCount.Value++;
            FSM.State(States.FlowingPlayer)
                .OnFixedUpdate(() =>
                {
                    //�����Ҵ���
                    if (Player.Default)
                    {
                        //�õ�ָ����ҵķ���
                        var direction = (Player.Default.transform.position - transform.position).normalized;
                        //������ƶ�
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
            //�����������С��0
            if (HP <= 0)
            {
                //�����Լ�
                this.DestroyGameObjGracefully();

                //��������ֵ�ĵ���

                Global.GeneratePowerUp(this.gameObject);
            }

        }
        private void OnDestroy()
        {
            //����������һ
            EnemyGenerator.EnemyCount.Value--;
        }
        private bool IgnoreHurt = false;
        public void Hurt(float value, bool force = false)
        {
            if (IgnoreHurt && !force) return;
            //�����˺�����
            FloatingTextController.Play(transform.position, value.ToString());
            //��ʾ��ɫ
            Sprite.color = Color.red;
            //�������˶���
            AudioKit.PlaySound("Hit");
            //�ӳ�0.2��
            ActionKit.Delay(0.2f, () =>
            {
                //Ѫ������
                this.HP -= value;
                //��ʾ��ɫ
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
