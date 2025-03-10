using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Survivor
{
	public interface IMovementInput 
	{
		float Horizontal { get; }

		float Vertical { get; }
    }
}
