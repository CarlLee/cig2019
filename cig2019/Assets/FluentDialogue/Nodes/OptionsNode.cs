using UnityEngine;
using System.Collections.Generic;

namespace Fluent
{

    /// <summary>
    /// Presents the user with options
    /// </summary>
    public class OptionsNode : FluentNode
    {
        Queue<FluentNode> childQueue = new Queue<FluentNode>();

        private OptionsPresenter optionsPresenter;

        public OptionsNode(FluentNode node, GameObject gameObject) : base(gameObject)
        {
            Children = node.UnravelFromBack();
        }

        public override void Execute()
        {
            optionsPresenter = GameObject.GetComponent<OptionsPresenter>();
            optionsPresenter.ClearOptions();

            //
            if (optionsPresenter == null)
            {
                Debug.Log("You need to add an OptionsPresenter if you want to show options", GameObject);
                DoneDelegate(this);
                return;
            }

            // This node can have fluent nodes in it that are not options
            // We execute them first before handling the options
            childQueue.Clear();
            Children.ForEach(n =>
                {
                    if (!(n is OptionNode))
                        childQueue.Enqueue(n);
                });

            if (childQueue.Count == 0)
                optionsPresenter.SetupOptions(this);
            else
                HandleNextNode();
        }

        private void HandleNextNode()
        {
            // Check if all the nodes have been handled
            if (childQueue.Count == 0)
            {
                // Start the options presenter
                optionsPresenter.SetupOptions(this);
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
            return "Options";
        }
    }

    public partial class FluentScript : MonoBehaviour
    {
        public FluentNode Options(FluentNode node)
        {
            return new OptionsNode(node, gameObject);
        }
    }

}
