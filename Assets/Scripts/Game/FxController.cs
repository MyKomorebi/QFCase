using QFramework;
using UnityEngine;

namespace ProjectSurvivor
{
    public partial class FxController : MonoBehaviour
    {
        private static FxController mDefault;

        private void Awake()
        {
            mDefault=this;
        }
        private void OnDestroy()
        {
            mDefault=null;
        }

        public static void Play(SpriteRenderer sprite ,Color dissolveColr)
        {
            mDefault.EnemyDieFx.Instantiate()
                .Position(sprite.Position())
                .LocalScale(sprite.Scale())
                .Self(self =>
                {
                    self.GetComponent<Dissolve>().DissolveColor = dissolveColr;
                    self.sprite=sprite.sprite;
                })
                .Show();

        }
    }
}
