using UnityEngine;
using System.Collections;
using Fluent;
using System;

namespace Fluent
{

    public class DoNode : FluentNode
    {
        public Action Action;
        public DoNode(Action action, GameObject gameObject) : base(gameObject)
        {
            this.Action = action;
        }

        public override void Execute()
        {
            Action();
            DoneDelegate(this);
        }

        public override void BeforeExecute()
        {
        }

        public override string StringInEditor()
        {
            return "<b>Do</b>";
        }
    }

    public partial class FluentScript : MonoBehaviour
    {
        public FluentNode Do(Action action)
        {
            return new DoNode(action, gameObject);
        }
    }

}
