using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Survivor
{
	public interface INewEnemy
	{
		void Hurt();

		void SetHP();

		void SetMoveSpeed();
	}
}
