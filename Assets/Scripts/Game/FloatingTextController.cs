using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace ProjectSurvivor
{
    public partial class FloatingTextController : ViewController
    {
        void Start()
        {
            //�����ı�
            FloatingText.Hide();
        }

        /// <summary>
        /// ����Ʈ������
        /// </summary>
        /// <param name="postion">λ��</param>
        /// <param name="text">����</param>
        public static void Play(Vector2 postion, string text)
        {
            mDefault.FloatingText.InstantiateWithParent(mDefault.transform)
                .Position(postion.x, postion.y)
              .Self(f =>
              {
                  //��ȡ�ı�����������ı�
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
