using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Survivor
{
    public class KeyboardMovementInput : IMovementInput
    {
        public float Horizontal
        {
           get{ return Input.GetAxisRaw("Horizontal"); }
        }

        public float Vertical 
        {
           get { return Input.GetAxisRaw("Vertical"); }
        }
    }
}
