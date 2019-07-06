using UnityEngine;
using System.Collections;
using Fluent;
using System;
using System.Collections.Generic;

namespace Fluent
{

    public class SequentialNode : FluentNode
    {
        Queue<FluentNode> childQueue = new Queue<FluentNode>();

        public SequentialNode(GameObject gameObject, FluentNode node) : base(gameObject)
        {
            // The children are defined as a node chain
            Children = node.UnravelFromBack();
        }

        public SequentialNode(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Execute()
        {
            // Add all the children to our list of child nodes that have not finished
            childQueue.Clear();
            Children.ForEach(n => childQueue.Enqueue(n));

            //
            HandleNextNode();
        }

        private void HandleNextNode()
        {
            if (childQueue.Count == 0)
            {
                Done();
                return;
            }

            FluentNode firstNode = childQueue.Dequeue();
            firstNode.SetDoneCallback(SequentialChildCompleted);
            firstNode.Execute();
        }

        private void SequentialChildCompleted(FluentNode node)
        {
            HandleNextNode();
        }

        public override void BeforeExecute()
        {
        }

        public override string StringInEditor()
        {
            return "Sequential";
        }
    }

    public partial class FluentScript : MonoBehaviour
    {
        public FluentNode SequentialNode(FluentNode node)
        {
            return new SequentialNode(gameObject, node);
        }
    }

}
