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
		/// 移动速度
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

        // 封装角色动画和音效状态控制
        private void UpdateMovementState(float horizontal, float vertical)
        {
            // 使用局部变量缓存移动状态，避免重复计算
            bool isMoving = !(Mathf.Approximately(horizontal, 0) && Mathf.Approximately(vertical, 0));

            // 状态切换：静止 -> 移动
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

        #region Sub-Methods 子方法分解
        // 处理闲置状态
        private void HandleIdleState()
        {
            string idleAnim = mIsFaceRight ? "PlayerIdleRight" : "PlayerIdleLeft";
            mAnimator.Play(idleAnim);
        }

        // 停止移动音效
        private void StopMovementSound()
        {
            if (mWalkSfx == null) return;

            mWalkSfx.Stop();
            mWalkSfx = null; // 显式释放引用
        }

        // 处理移动音效
        private void HandleMovementSound()
        {
            if (mWalkSfx == null)
            {
                // 使用带循环参数的音效播放
                mWalkSfx = AudioKit.PlaySound(Sfx.WALK, true);
            }
        }

        // 更新角色朝向
        private void UpdateFacingDirection(float horizontal)
        {
            // 仅当水平输入有效时更新方向
            if (!Mathf.Approximately(horizontal, 0))
            {
                mIsFaceRight = horizontal > 0;
            }
        }

        // 处理行走动画
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
