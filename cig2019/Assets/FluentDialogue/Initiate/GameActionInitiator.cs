using UnityEngine;
using System.Collections;
using System;

namespace Fluent
{
    public class GameActionInitiator : MonoBehaviour
    {
        public virtual GameAction ShouldAddGameAction()
        {
            return null;
        }
    }

}
