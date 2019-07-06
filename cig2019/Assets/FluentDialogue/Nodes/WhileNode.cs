using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fluent
{
    public class WhileNode : FluentNode
    {
        Queue<FluentNode> childQueue = new Queue<FluentNode>();
        Func<bool> test;

        public WhileNode(GameObject gameObject, Func<bool> test, FluentNode node)
            : base(gameObject)
        {            
            // The children are defined as a node chain
            Children = node.UnravelFromBack();

            this.test = test;
        }

        public override void Execute()
        {
            if (!test())
            {
                Done();
                return;
            }

            // Add all the children to our list of child nodes that have not finished
            childQueue.Clear();
            Children.ForEach(n => childQueue.Enqueue(n));

            //
            HandleNextNode();
        }

        private void HandleNextNode()
        {
            // Check if we handled all the children
            if (childQueue.Count == 0)
            {
                // Lets see if we should restart the while
                if (!test())
                {
                    Done();
                    return;
                }

                // Add all the children again
                Children.ForEach(n => childQueue.Enqueue(n));
            }

            FluentNode firstNode = childQueue.Dequeue();
            firstNode.SetDoneCallback(ChildCompleted);
            firstNode.Execute();
        }

        private void ChildCompleted(FluentNode node)
        {
            HandleNextNode();
        }

        public override void BeforeExecute()
        {
        }

        public override string StringInEditor()
        {
            return "While";
        }
    }

    public partial class FluentScript : MonoBehaviour
    {
        public FluentNode While(Func<bool> test, FluentNode node)
        {
            return new WhileNode(gameObject, test, node);
        }
    }

}
