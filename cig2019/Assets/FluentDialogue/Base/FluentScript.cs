using UnityEngine;

namespace Fluent
{
    public delegate void FluentScriptCompleteDelegate(FluentScript fluentScript);

    /// <summary>
    /// FluentScripts are components that are placed on, for example, your non player characters.
    /// 
    /// Life Cyle
    /// 1. FluentScript.Run() is called by you 
    /// 2. FluentScript.OnStart() is called
    /// 3. FluentScript.Create() is called
    /// 4. FluentScript.OnBeforeExecute is called on all the nodes in the tree
    /// 5. The first FluentNode is executed
    /// 6. Continue executing nodes until the tree is done
    /// 7. FluentScript.OnFinish(), which you can overload, is executed
    /// 8. FluentScript.Done() callback is called
    /// 
    /// </summary>
    public abstract partial class FluentScript : MonoBehaviour
    {

        public void Awake()
        {
            // If there is no initiator add the default one
            if (GetComponent<GameActionInitiator>() == null)
            {
                if (gameObject.GetComponent<Collider>() == null)
                {
                    Debug.LogWarning("Conversations need to be triggered somehow. FluentDialog automatically adds a 3D Proximity Trigger when there is no trigger present. For that trigger to work you need a collider, please add for example a SphereCollider", gameObject);
                    return;
                }
                gameObject.AddComponent(typeof(ProximityInitiator));
            }

        }

        protected FluentScriptCompleteDelegate Done;
        public void SetDoneCallback(FluentScriptCompleteDelegate done)
        {
            Done = done;
        }

        /// <summary>
        /// This is the first method called by Run
        /// Use this to for example pause player movement
        /// </summary>
        public virtual void OnStart() { }

        /// <summary>
        /// Create is called by run after OnStart
        /// This is where you defined your logic.
        /// </summary>
        /// <returns></returns>
        public abstract FluentNode Create();

        /// <summary>
        /// This is the last method called by Run
        /// After Run executes the Done() delegate is called
        /// </summary>
        public virtual void OnFinish() { }

        public virtual string Description()
        {
            return "Talk";
        }

        /// <summary>
        /// Call this to start the FluentScript
        /// </summary>
        public virtual void Run()
        {
            //
            OnStart();

            // Create the fluent script
            FluentNode firstNode = SequentialNode(Create());

            // Do a couple of things to the tree before execution starts
            firstNode.Visit((n) =>
            {
                // Tell all the children who the parent is
                n.Children.ForEach(c => c.Parent = n);

                // Run their before execute methods
                n.BeforeExecute();
            });

            //
            firstNode.SetDoneCallback(RootDone);

            //
            firstNode.Execute();
        }

        private void RootDone(FluentNode node)
        {
            OnFinish();
            if (Done != null)
                Done(this);
        }
    }
}
