
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace Fluent
{
    [AddComponentMenu("")]
    public class ContinueWhenHandler : FluentNodeHandler
    {
        ContinueWhenNode continueWhen;

        public override void HandleFluentNode(FluentNode fluentNode)
        {
            continueWhen = (ContinueWhenNode)fluentNode;

            // Start pause
            StartCoroutine("RunTest");
        }

        IEnumerator RunTest()
        {
            while (!continueWhen.Test())
            {
                yield return new WaitForSeconds(0.5f);
            }
            continueWhen.Done();
        }

        public override void Interrupt(FluentNode fluentNode)
        {
            StopCoroutine("RunTest");
        }
    }
}
