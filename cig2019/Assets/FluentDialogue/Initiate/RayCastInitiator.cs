using UnityEngine;
using System.Collections;

namespace Fluent
{

    /// <summary>
    /// Clicking on the game object's collider this component is placed on will initiate a TalkAction when clicked.
    /// Put the object this component is placed on a seperate layer to improve performance.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class RayCastInitiator : GameActionInitiator
    {

        public GameObject GameObjectWithScript;
        bool over = false;
        bool everythingOk = false;

        void Start()
        {
            if (GameObjectWithScript == null)
            {
                Debug.LogError("GameObjectWithTalk needs to be set to an object that has a FluentScript component", this);
                return;
            }

            if (GameObjectWithScript.GetComponent<FluentScript>() == null)
            {
                Debug.LogError("You need a FluentScript component on this object to initiate FluentDialogue", this);
                return;
            }
            everythingOk = true;
        }

        void Update()
        {
            if (!everythingOk)
                return;

            // Perform a ray cast to see if the mouse is over this game object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, float.MaxValue, 1 << gameObject.layer);
            bool found = false;
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject == this.gameObject)
                    found = true;
            }

            // Add or remove the talk action to the available actions in the action manager
            if (!found && over)
            {
                FluentManager.Instance.RemoveScript(GameObjectWithScript.GetComponent<FluentScript>());
                over = false;
            }
            else if (found && !over)
            {
                FluentManager.Instance.AddScript(GameObjectWithScript.GetComponent<FluentScript>());
                over = true;
            }

            // Execute the talk action if this collider was hit
            if (over && Input.GetMouseButtonDown(0))
            {
                // Todo: You might want to check the distance your player is from GameObjectWithTalk
                FluentManager.Instance.ExecuteAction(GameObjectWithScript.GetComponent<FluentScript>());
            }

        }

    }
}
