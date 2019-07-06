using UnityEngine;
using System.Collections.Generic;
using System;

namespace Fluent
{
    public class OptionNode : FluentNode
    {
        Queue<FluentNode> childrenLeftToExecute = new Queue<FluentNode>();

        public bool GoesBack { get; set; }

        /// <summary>
        /// Should this option be shown ?
        /// </summary>
        public Func<bool> ShouldShow;
        public OptionNode VisibleIf(Func<bool> shouldShow)
        {
            ShouldShow = shouldShow;
            return this;
        }

        public bool HasBeenChosen { get; set; }
        public bool OnlyDisplayOnce { get; set; }
        public OptionNode OnlyOnce()
        {
            OnlyDisplayOnce = true;
            return this;
        }

        /// <summary>
        /// The text of this option
        /// </summary>
        public FluentString OptionText { get; set; }

        public OptionNode(GameObject go) : base(go)
        {
        }

        public override void Execute()
        {
            // Add all the children to our list of child nodes that have not finished
            childrenLeftToExecute.Clear();
            Children.ForEach(n => childrenLeftToExecute.Enqueue(n));

            //
            HandleNextNode();
        }

        private void HandleNextNode()
        {
            if (childrenLeftToExecute.Count == 0)
            {
                // Ugh, think about cleanup
                if (GoesBack && (Parent is OptionsNode) && (Parent.Parent is OptionNode) && (Parent.Parent.Parent is OptionsNode))
                {
                    Parent.Parent.Parent.Execute();
                }
                else
                {
                    HasBeenChosen = true;
                    Done();
                }

                return;
            }

            FluentNode firstNode = childrenLeftToExecute.Dequeue();
            firstNode.SetDoneCallback(OptionChildCompleted);
            firstNode.Execute();
        }

        private void OptionChildCompleted(FluentNode node)
        {
            HandleNextNode();
        }

        public OptionNode Back()
        {
            GoesBack = true;
            return this;
        }

        public OptionNode Text(string text)
        {
            this.OptionText = text;
            return this;
        }

        public OptionNode Text(FluentString fluentString)
        {
            OptionText = fluentString;
            return this;
        }

        protected override FluentNode Join(FluentNode rightNode)
        {
            // If the right node is an option, chain them
            if (rightNode is OptionNode)
                return base.Join(rightNode);

            // Otherwise we add the node as a child node to option and return the option
            // so that we can add other nodes to the same option
            Children.Add(rightNode);
            return this;
        }

        /// <summary>
        /// When two Options are *'d they are chained
        /// </summary>
        /// <param name="leftNode"></param>
        /// <param name="rightNode"></param>
        /// <returns></returns>
        public static OptionNode operator *(OptionNode leftNode, OptionNode rightNode)
        {
            leftNode.Next = rightNode;
            rightNode.Previous = leftNode;
            return rightNode;
        }

        public override void BeforeExecute()
        {
            HasBeenChosen = false;
        }

        public override string StringInEditor()
        {
            return OptionText;
        }


    }

    public partial class FluentScript : MonoBehaviour
    {
        public OptionNode Option(FluentString optionText)
        {
            return new OptionNode(gameObject).Text(optionText);
        }

        public OptionNode Option()
        {
            return new OptionNode(gameObject);
        }
    }

}
