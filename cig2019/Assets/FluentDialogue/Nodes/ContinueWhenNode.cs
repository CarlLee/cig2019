using UnityEngine;
using System.Collections;
using Fluent;
using System;

namespace Fluent
{
    [FluentNodeHandler(typeof(ContinueWhenHandler))]
    public class ContinueWhenNode : FluentNode
    {
        public Func<bool> Test;

        public ContinueWhenNode(Func<bool> test, GameObject gameObject) : base(gameObject)
        {
            Test = test;
        }

        public override void BeforeExecute()
        {
        }

        public override string StringInEditor()
        {
            return "<b>Continue When</b>";
        }
    }

    public partial class FluentScript : MonoBehaviour
    {
        public FluentNode ContinueWhen(Func<bool> test)
        {
            return new ContinueWhenNode(test, gameObject);
        }
    }

}
