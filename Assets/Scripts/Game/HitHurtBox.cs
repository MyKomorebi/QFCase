using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class HitHurtBox : GameplayObject
	{
        protected override Collider2D Collider2D =>mCollider2D;

		private Collider2D mCollider2D;

        public GameObject Owner;

        private void Awake()
        {
            mCollider2D = GetComponent<Collider2D>();
        }
        void Start()
		{
			if (!Owner)
			{
				Owner=transform.parent.gameObject;
			}
		

		}
	}
}
