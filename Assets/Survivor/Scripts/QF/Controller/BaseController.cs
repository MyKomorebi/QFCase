using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Survivor
{
    public class BaseController :MonoBehaviour, IController
    {
        public IArchitecture GetArchitecture()
        {
            return SurvivorGame.Interface;
        }
    }
}
