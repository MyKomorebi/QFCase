using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using QAssetBundle;
using QFramework;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;


namespace Survivor
{
	public class NewPlayer :BaseController,ITrackable
    {
        /// <summary>
        /// ����
        /// </summary>
		private IMovementInput mMovementInput;
		
        /// <summary>
        /// 2D����
        /// </summary>
		private Rigidbody2D mRigidbody2D;
        /// <summary>
        /// ����������
        /// </summary>
        private Animator mAnimator;
        /// <summary>
        /// QF������������
        /// </summary>
        private AudioPlayer mWalkSfx;

        /// <summary>
		/// �ƶ��ٶ�
		/// </summary>
		[SerializeField]
        private float mMovementSpeed = 5f;
        /// <summary>
        /// �Ƿ����ұ�
        /// </summary>
        private bool mIsFaceRight = false;
        /// <summary>
        /// ��ҵ�Transform
        /// </summary>
        public Transform PlayerTransform => transform;

        private void Start()
		{
			mMovementInput = new KeyboardMovementInput();
            mRigidbody2D= GetComponent<Rigidbody2D>();
            mAnimator=GetComponentInChildren<Animator>();
            StartCheckInput();
        }

        /// <summary>
        /// ������룬�����ƶ����޸�״̬
        /// </summary>
		private void StartCheckInput()
		{
			CheckPlayerInput().ForEachAsync(delta =>
			{
               
				var targetVelocity=
					new Vector2(delta.horizontal,delta.vertical)
					.normalized*mMovementSpeed;

                UpdateMovementState(delta.horizontal, delta.vertical);
               
                mRigidbody2D.velocity= Vector2.Lerp
                    (mRigidbody2D.velocity, targetVelocity, 1 - Mathf.Exp(-Time.deltaTime * 5));
             
            });

        }

        /// <summary>
        /// ��װ��ɫ��������Ч״̬����
        /// </summary>
        /// <param name="horizontal"></param>
        /// <param name="vertical"></param>
        private void UpdateMovementState(float horizontal, float vertical)
        {
            // ʹ�þֲ����������ƶ�״̬�������ظ�����
            bool isMoving = !(Mathf.Approximately(horizontal, 0) && Mathf.Approximately(vertical, 0));

            // ״̬�л�����ֹ -> �ƶ�
            if (!isMoving)
            {
                HandleIdleState();
                StopMovementSound();
            }
            else
            {
                HandleMovementSound();
                UpdateFacingDirection(horizontal);
                HandleWalkingAnimation();
            }
        }

        #region Sub-Methods �ӷ����ֽ�
        /// <summary>
        /// ��������״̬
        /// </summary>
        private void HandleIdleState()
        {
            string idleAnim = mIsFaceRight ? "PlayerIdleRight" : "PlayerIdleLeft";
            mAnimator.Play(idleAnim);
        }

        /// <summary>
        /// ֹͣ�ƶ���Ч
        /// </summary>
        private void StopMovementSound()
        {
            if (mWalkSfx == null) return;
            ///���ղ�����
            mWalkSfx.Stop();
            mWalkSfx = null; // ��ʽ�ͷ�����
        }

        /// <summary>
        /// �����ƶ���Ч
        /// </summary>
        private void HandleMovementSound()
        {
            if (mWalkSfx == null)
            {
                // �����ƶ���Ч�����һ�ȡ������
                mWalkSfx = AudioKit.PlaySound(Sfx.WALK, true);
            }
        }

        // ���½�ɫ����
        private void UpdateFacingDirection(float horizontal)
        {
            // ����ˮƽ������Чʱ���·���
            if (!Mathf.Approximately(horizontal, 0))
            {
                mIsFaceRight = horizontal > 0;
            }
        }

        /// <summary>
        /// �������߶���
        /// </summary>
        private void HandleWalkingAnimation()
        {
            string walkAnim = mIsFaceRight ? "PlayerWalkRight" : "PlayerWalkLeft";
            mAnimator.Play(walkAnim);
        }
        #endregion

        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        private IUniTaskAsyncEnumerable<( float horizontal, float vertical)>CheckPlayerInput()
		{
			return UniTaskAsyncEnumerable.Create<(float, float)>
				(async (writer, token) =>
			{
				await UniTask.Yield();

				while(!token.IsCancellationRequested)
				{
					await writer.YieldAsync((mMovementInput.Horizontal
						, mMovementInput.Vertical));
					await UniTask.Yield ();
				}
			});
		}
	}
}
