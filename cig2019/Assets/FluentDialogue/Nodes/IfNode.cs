using UnityEngine;
using System.Collections;
using Fluent;
using System;

namespace Fluent
{
    public class IfNode : SequentialNode
    {
        Func<bool> test;

        public IfNode(GameObject gameObject, Func<bool> test, FluentNode node)
            : base(gameObject, node)
        {
            this.test = test;
        }

        public override void Execute()
        {
            if (!test())
            {
                Done();
                return;
            }
            base.Execute();
        }

        public override void BeforeExecute()
        {
        }

        public override string StringInEditor()
        {
            return "If";
        }
    }

    public partial class FluentScript : MonoBehaviour
    {
        public FluentNode If(Func<bool> test, FluentNode node)
        {
            return new IfNode(gameObject, test, node);
        }
    }

}
