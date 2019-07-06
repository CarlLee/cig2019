using UnityEngine;
using System.Collections;
using Fluent;
using System;

namespace Fluent
{

    public class OnceNode : SequentialNode
    {
        private bool hasFired = false;

        public OnceNode(GameObject gameObject, FluentNode node) 
            : base(gameObject, node)
        {
        }

        public override void Execute()
        {
            if (hasFired)
            {
                Done();
                return;
            }

            hasFired = true;
            base.Execute();
        }

        public override void BeforeExecute()
        {
            hasFired = false;            
        }

        public override string StringInEditor()
        {
            return "Once";
        }
    }

    public partial class FluentScript : MonoBehaviour
    {
        public FluentNode Once(FluentNode node)
        {
            return new OnceNode(gameObject, node);
        }
    }

}
