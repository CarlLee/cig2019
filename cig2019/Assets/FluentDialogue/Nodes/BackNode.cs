using UnityEngine;
using System.Collections;
using Fluent;
using System;

namespace Fluent
{

    public class BackNode : FluentNode
    {

        public BackNode(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Execute()
        {
            if ((Parent is OptionNode) && // Back is a child of option
                (Parent.Parent is OptionsNode) && // Option has its parent
                (Parent.Parent.Parent is OptionNode) && // Which has another option on top of that
                (Parent.Parent.Parent.Parent is OptionsNode))
            {
                Parent.Parent.Parent.Parent.Execute();
            }
        }

        public override void BeforeExecute()
        {
        }

        public override string StringInEditor()
        {
            return "<b>Back</b>";
        }

    }

    public partial class FluentScript : MonoBehaviour
    {
        public FluentNode Back()
        {            
            return new BackNode(gameObject);
        }
    }

}