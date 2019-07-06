using UnityEngine;
using System.Collections;
using Fluent;
using System;

namespace Fluent
{
    public class EndNode : FluentNode
    {
        public EndNode(GameObject gameObject) 
            : base(gameObject)
        {
        }
        
        public override void Execute()
        {
            OptionsPresenter optionsPresenter = GameObject.GetComponent<OptionsPresenter>();
            if (optionsPresenter == null)
            {
                Debug.Log("You need to add an OptionsPresenter if you want to close it", GameObject);
                return;
            }
            optionsPresenter.End();
        }

        public override void BeforeExecute()
        {
        }

        public override string StringInEditor()
        {
            return "<b>End</b>";
        }
    }

    public partial class FluentScript : MonoBehaviour
    {
        public FluentNode End()
        {
            return new EndNode(gameObject);
        }
    }

}
