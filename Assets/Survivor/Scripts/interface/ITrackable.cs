using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Survivor
{
	public interface ITrackable
	{
		public Transform PlayerTransform { get; }
	}
}
