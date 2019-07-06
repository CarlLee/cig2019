using UnityEngine;
using System.Collections;
using Fluent;
using System;

namespace Fluent
{
    public class HideNode : FluentNode
    {
        GameObject gameObjectToHide;

        public HideNode(GameObject gameObject) : base(gameObject)
        {
        }

        public HideNode(GameObject gameObject, GameObject gameObjectToHide) : base(gameObject)
        {
            if (gameObjectToHide == null)
                Debug.LogWarning("You are trying to hide a null gameObject");
            this.gameObjectToHide = gameObjectToHide;
        }

        public override void Execute()
        {
            if (gameObjectToHide != null)
            {
                gameObjectToHide.SetActive(false);
                Done();
                return;
            }

            OptionsPresenter optionsPresenter = GameObject.GetComponent<OptionsPresenter>();
            if (optionsPresenter == null)
            {
                Debug.Log("You need to add an OptionsPresenter if you want to hide it", GameObject);
                return;
            }
            optionsPresenter.Hide();
            Done();
        }

        public override void BeforeExecute()
        {
        }

        public override string StringInEditor()
        {
            return "<b>Hide</b>";
        }
    }

    public partial class FluentScript : MonoBehaviour
    {
        public FluentNode Hide()
        {
            return new HideNode(gameObject);
        }

        public FluentNode Hide(GameObject gameObjectToHide)
        {
            return new HideNode(gameObject, gameObjectToHide);
        }
    }

}
