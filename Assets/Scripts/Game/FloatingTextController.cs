using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace ProjectSurvivor
{
    public partial class FloatingTextController : ViewController
    {
        void Start()
        {
            //隐藏文本
            FloatingText.Hide();
        }

        /// <summary>
        /// 播放飘动文字
        /// </summary>
        /// <param name="postion">位置</param>
        /// <param name="text">内容</param>
        public static void Play(Vector2 postion, string text)
        {
            mDefault.FloatingText.InstantiateWithParent(mDefault.transform)
                .Position(postion.x, postion.y)
              .Self(f =>
              {
                  //获取文本组件，设置文本
                  var postionY=postion.y;
                var textTrans = f.transform.Find("Text");
                var textcomp = textTrans.GetComponent<Text>();
                textcomp.text = text;
               ActionKit.Sequence().Lerp(0, 0.5f, 0.5f, p =>
               {
                  
                   f.PositionY(postionY + p*0.25f);
                   textcomp.LocalScaleX(Mathf.Clamp01(p * 8));
                   textcomp.LocalScaleY(Mathf.Clamp01(p * 8));
               }).Delay(0.5f).Lerp(1.0f, 0, 0.3f, (p) =>
               {
                   textcomp.ColorAlpha(p);
               }, () =>
               {
                   textTrans.DestroyGameObjGracefully();
               })
                  .Start(textcomp);

                  
              }).Show();

        }
        private static FloatingTextController mDefault;

        private void Awake()
        {
            mDefault = this;
        }
        private void OnDestroy()
        {
            mDefault = null;
        }
    }
}
