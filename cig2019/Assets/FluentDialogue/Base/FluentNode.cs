using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Fluent
{
    public delegate void FluentNodeDelegate(FluentNode fluentNode);
    public delegate string StringDelegate();

    public abstract class FluentNode
    {
        public GameObject GameObject { get; set; }
        public FluentNode Parent { get; set; }

        private FluentNodeHandler currentHandler;

        public FluentNode(GameObject gameObject)
        {
            InterruptDelegate = null;
            currentHandler = null;
            GameObject = gameObject;
        }

        /// <summary>
        /// This is a callback for whoever is listening to know when this fluent node is complete
        /// </summary>
        protected FluentNodeDelegate DoneDelegate;
        public void SetDoneCallback(FluentNodeDelegate done)
        {
            DoneDelegate = done;
        }

        /// <summary>
        /// Call Done() to notify whoever is listening that this node has finished executing
        /// </summary>
        public void Done()
        {
            if (DoneDelegate == null)
                throw new Exception("Done cannot be null on a FluentNode");

            DoneDelegate(this);
        }

        /// <summary>
        /// This is a callback for whoever is listening to know when this fluent node is interrupted
        /// </summary>
        protected FluentNodeDelegate InterruptDelegate;
        public void SetInterruptCallback(FluentNodeDelegate interruptDelegate)
        {
            InterruptDelegate = interruptDelegate;
        }

        /// <summary>
        /// When a FluentNode is interrupted, eg. when a user clicks on Text to skip it,
        /// it needs to call this method so that parents like ParallelNode can also inform
        /// it's other children that a node was interrupted, this way we can skip sound and text 
        /// </summary>
        public void IWasInterrupted()
        {
            // Its possible that no one is interested in the interruption of this node
            if (InterruptDelegate == null)
                return;
            InterruptDelegate(this);
        }

        /// <summary>
        /// Others might want to interrupt this FluentNode
        /// This FluentNode should stop all it's FluentNodeHandlers and call Done()
        /// </summary>
        public virtual void Interrupt()
        {
        }

        /// <summary>
        /// The next node to execute
        /// </summary>
        protected FluentNode Next { get; set; }

        /// <summary>
        /// The previous node that would have executed
        /// </summary>
        protected FluentNode Previous { get; set; }

        /// <summary>
        /// Fluent node children
        /// </summary>
        public List<FluentNode> Children = new List<FluentNode>();

        /// <summary>
        /// Does this node have children ?
        /// </summary>
        public bool HasChildren
        {
            get { return Children.Count > 0; }
        }

        public virtual string StringInEditor()
        {
            return GetType().Name;
        }

        /// <summary>
        /// The default join behaviour for two nodes is to chain them with Next and Previous and return the right node
        /// </summary>
        /// <param name="rightNode"></param>
        /// <returns></returns>
        protected virtual FluentNode Join(FluentNode rightNode)
        {
            Next = rightNode;
            rightNode.Previous = this;
            return rightNode;
        }

        /// <summary>
        /// Join two nodes, the first node decides how by overriding Join
        /// The list eventually needs to be reconstructed with UnravelFromBack
        /// </summary>
        /// <param name="leftNode"></param>
        /// <param name="rightNode"></param>
        /// <returns></returns>
        public static FluentNode operator *(FluentNode leftNode, FluentNode rightNode)
        {
            return leftNode.Join(rightNode);
        }


        /// <summary>
        /// Start at the back and reconstruct a FluentNode chain.
        /// FluentNodes are constructed by chaining them with the * operator.
        /// Most of the times we want to turn that chain into a list.
        /// Will also remove the previous and next pointers from each node, they are only used internally
        /// </summary>
        /// <returns></returns>
        public List<FluentNode> UnravelFromBack()
        {
            List<FluentNode> nodeList = new List<FluentNode>();
            FluentNode currentNode = this;
            while (currentNode != null)
            {
                nodeList.Insert(0, currentNode);
                FluentNode prevNode = currentNode;
                currentNode = currentNode.Previous;
                prevNode.Next = null;
                prevNode.Previous = null;

            }
            return nodeList;
        }

        public FluentNode GetFirst()
        {
            FluentNode currentNode = this;
            while (true)
            {
                if (currentNode.Previous == null)
                    return currentNode;
                currentNode = currentNode.Previous;
            }
        }

        /// <summary>
        /// Visit all the nodes in this tree
        /// </summary>
        /// <param name="action"></param>
        public void Visit(Action<FluentNode> action)
        {
            // First visit the node itself
            action(this);

            // Then it's children
            foreach (FluentNode n in Children)
                n.Visit(action);

            // Then the next node
            if (Next != null)
                Next.Visit(action);
        }

        /// <summary>
        /// If a node does not override Execute we'll try and see if it has a handler
        /// If it does have a handler we start it, otherwise we just call Done
        /// </summary>
        /// <param name="fluentScript"></param>
        public virtual void Execute()
        {
            // Try to get a handler
            FluentNodeHandler handler = GetHandler();

            // If there isn't a handler we complete the fluentnode
            if (handler == null)
            {
                DoneDelegate(this);
                return;
            }

            // Give this node to the handler we just created
            handler.HandleFluentNode(this);
        }

        /// <summary>
        /// This is called on all the FluentNodes before execution starts
        /// </summary>
        public virtual void BeforeExecute()
        {
        }

        public virtual FluentNodeHandler GetHandler()
        {
            // ?
            if (currentHandler != null)
                return currentHandler;

            // Get the handler type attribute defined on the class
            FluentNodeHandlerAttribute handlerAttribute = (FluentNodeHandlerAttribute)this.GetType().GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(FluentNodeHandlerAttribute));
            if (handlerAttribute == null)
            {
                Debug.LogError("You need to specify the FluentNodeHandler for '" + GetType().Name + "' by adding the FluentNodeHandler attribute or overriding GetHandler()", GameObject);
                return null;
            }

            // 
            Type fluentNodeHandlerType = handlerAttribute.FluentNodeHandlerType;

            // Make sure the handler is a monobehaviour
            if (!fluentNodeHandlerType.IsSubclassOf(typeof(MonoBehaviour)))
            {
                Debug.LogError("Your FluentNodeHandlerType '" + fluentNodeHandlerType.Name + "' needs to inherit from MonoBehaviour", GameObject);
                return null;
            }

            // Try to get a component with the handler type from this game object
            FluentNodeHandler handler = GameObject.GetComponent(fluentNodeHandlerType) as FluentNodeHandler;

            if (handler == null)
            {
                // If the user had to add the handler explicitly but its missing, show an error
                if (handlerAttribute.IsExplicit)
                {
                    Debug.LogError("You have to explicitly add the '" + fluentNodeHandlerType.Name + "' handler for the '" + GetType().Name + "' FluentNode to work", GameObject);
                    return null;
                }

                // Add the handler if we are allowed to
                if (fluentNodeHandlerType != null)
                    handler = GameObject.AddComponent(fluentNodeHandlerType) as FluentNodeHandler;
            }

            currentHandler = handler;
            return currentHandler;
        }

    }
}
