using UnityEngine;
using System.Collections;
using Fluent;
using System;
using System.Collections.Generic;

namespace Fluent
{

    public class ParallelNode : FluentNode
    {
        List<FluentNode> childNodesNotDone = new List<FluentNode>();

        public ParallelNode(FluentNode node, GameObject gameObject) : base(gameObject)
        {
            // The children are defined as a node chain
            Children = node.UnravelFromBack();
        }

        public override void Execute()
        {
            // Add all the children to our list of child nodes that have not finished
            childNodesNotDone.Clear();
            childNodesNotDone.AddRange(Children);

            // Due to the callbacks that could change our list while we are
            // iterating through it, we make a copy
            List<FluentNode> childrenCopy = new List<FluentNode>(Children);

            // Start all the children
            foreach (FluentNode child in childrenCopy)
            {
                // Connect the child's done callback to this node
                // so that we can remove it from the children that 
                // have not yet completed
                child.SetDoneCallback(ParallelChildCompleted);

                // 
                child.SetInterruptCallback(ParallelChildInterrupted);

                // Start the child
                child.Execute();
            }

            //
        }

        private void ParallelChildCompleted(FluentNode node)
        {
            // It is possible that a child node completes via normal means before the interruption is triggered
            // Make sure we don't remove it twice, which would also make parallel's done be called twice
            if (!childNodesNotDone.Contains(node))
                return;

            // Remove the node
            childNodesNotDone.Remove(node);

            // If all the child nodes have been completed
            // Notify whoever that this parallel node is complete
            if (childNodesNotDone.Count == 0)
                DoneDelegate(this);
        }

        /// <summary>
        /// Child nodes tell us when they are interrupted so that we can interrupt the other parallel children
        /// eg. Write might be interrupted by being clicked on, we then also need to stop any sounds from playing
        /// </summary>
        /// <param name="node"></param>
        private void ParallelChildInterrupted(FluentNode node)
        {
            // Interrupt all the other children
            FluentNode[] childrenToInterrupt = childNodesNotDone.ToArray();

            foreach(FluentNode childToInterrupt in childrenToInterrupt)
                childToInterrupt.Interrupt();
        }

        public override void BeforeExecute()
        {
        }

        public override string StringInEditor()
        {
            return "Parallel";
        }
    }

    public partial class FluentScript : MonoBehaviour
    {
        public FluentNode Parallel(FluentNode node)
        {
            return new ParallelNode(node, gameObject);
        }
    }

}
