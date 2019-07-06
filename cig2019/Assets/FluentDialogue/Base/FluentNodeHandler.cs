using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Fluent
{

    public abstract class FluentNodeHandler : MonoBehaviour
    {
        public abstract void HandleFluentNode(FluentNode fluentNode);

        /// <summary>
        /// Interrupt should interrupt the execution of the handler 
        /// and call Done() for the fluentNode that was passed as parameter
        /// </summary>
        /// <param name="fluentNode"></param>
        public virtual void Interrupt(FluentNode fluentNode)
        {
        }
    }
}
