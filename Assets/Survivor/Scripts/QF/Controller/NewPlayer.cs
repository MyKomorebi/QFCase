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
        /// 输入
        /// </summary>
		private IMovementInput mMovementInput;
		
        /// <summary>
        /// 2D刚体
        /// </summary>
		private Rigidbody2D mRigidbody2D;
        /// <summary>
        /// 动画控制器
        /// </summary>
        private Animator mAnimator;
        /// <summary>
        /// QF的声音播放器
        /// </summary>
        private AudioPlayer mWalkSfx;

        /// <summary>
		/// 移动速度
		/// </summary>
		[SerializeField]
        private float mMovementSpeed = 5f;
        /// <summary>
        /// 是否朝向右边
        /// </summary>
        private bool mIsFaceRight = false;
        /// <summary>
        /// 玩家的Transform
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
        /// 检测输入，控制移动，修改状态
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
        /// 封装角色动画和音效状态控制
        /// </summary>
        /// <param name="horizontal"></param>
        /// <param name="vertical"></param>
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
        /// <summary>
        /// 处理闲置状态
        /// </summary>
        private void HandleIdleState()
        {
            string idleAnim = mIsFaceRight ? "PlayerIdleRight" : "PlayerIdleLeft";
            mAnimator.Play(idleAnim);
        }

        /// <summary>
        /// 停止移动音效
        /// </summary>
        private void StopMovementSound()
        {
            if (mWalkSfx == null) return;
            ///回收播放器
            mWalkSfx.Stop();
            mWalkSfx = null; // 显式释放引用
        }

        /// <summary>
        /// 处理移动音效
        /// </summary>
        private void HandleMovementSound()
        {
            if (mWalkSfx == null)
            {
                // 播放移动音效，并且获取播放器
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

        /// <summary>
        /// 处理行走动画
        /// </summary>
        private void HandleWalkingAnimation()
        {
            string walkAnim = mIsFaceRight ? "PlayerWalkRight" : "PlayerWalkLeft";
            mAnimator.Play(walkAnim);
        }
        #endregion

        /// <summary>
        /// 检查输入
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
