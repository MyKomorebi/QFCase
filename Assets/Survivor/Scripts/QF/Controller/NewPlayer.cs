using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using QAssetBundle;
using QFramework;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;


namespace Survivor
{
	public class NewPlayer :BaseController
	{
		private IMovementInput mMovementInput;
		

		private Rigidbody2D mRigidbody2D;

        private Animator mAnimator;

        private AudioPlayer mWalkSfx;

        /// <summary>
		/// �ƶ��ٶ�
		/// </summary>
		[SerializeField]
        private float mMovementSpeed = 5f;

        private bool mIsFaceRight = false;


        private void Start()
		{
			mMovementInput = new KeyboardMovementInput();
            mRigidbody2D= GetComponent<Rigidbody2D>();
            mAnimator=GetComponentInChildren<Animator>();
            StartCheckInput();
        }


		private void StartCheckInput()
		{
			CheckPlayerInput().ForEachAsync(delta =>
			{
                Debug.Log("zxinf1");
				var targetVelocity=
					new Vector2(delta.horizontal,delta.vertical)
					.normalized*mMovementSpeed;

                UpdateMovementState(delta.horizontal, delta.vertical);
               
                mRigidbody2D.velocity= Vector2.Lerp
                    (mRigidbody2D.velocity, targetVelocity, 1 - Mathf.Exp(-Time.deltaTime * 5));
             
            });

        }

        // ��װ��ɫ��������Ч״̬����
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
        // ��������״̬
        private void HandleIdleState()
        {
            string idleAnim = mIsFaceRight ? "PlayerIdleRight" : "PlayerIdleLeft";
            mAnimator.Play(idleAnim);
        }

        // ֹͣ�ƶ���Ч
        private void StopMovementSound()
        {
            if (mWalkSfx == null) return;

            mWalkSfx.Stop();
            mWalkSfx = null; // ��ʽ�ͷ�����
        }

        // �����ƶ���Ч
        private void HandleMovementSound()
        {
            if (mWalkSfx == null)
            {
                // ʹ�ô�ѭ����������Ч����
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

        // �������߶���
        private void HandleWalkingAnimation()
        {
            string walkAnim = mIsFaceRight ? "PlayerWalkRight" : "PlayerWalkLeft";
            mAnimator.Play(walkAnim);
        }
        #endregion


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
