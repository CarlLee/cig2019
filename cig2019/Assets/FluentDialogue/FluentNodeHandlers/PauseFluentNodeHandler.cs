using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace Fluent
{
    [AddComponentMenu("")]
    public class PauseFluentNodeHandler : FluentNodeHandler
    {
        PauseNode pause;

        public override void HandleFluentNode(FluentNode fluentNode)
        {
            pause = (PauseNode)fluentNode;

            // Start pause
            StartCoroutine("StartPause");
        }

        private IEnumerator StartPause()
        {
            // Pause x seconds
            if (pause.Seconds > 0)
                yield return new WaitForSeconds(pause.Seconds);

            pause.Done();
        }


        public override void Interrupt(FluentNode fluentNode)
        {
            StopCoroutine("StartPause");
        }
    }
}
