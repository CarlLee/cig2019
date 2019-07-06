using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace Fluent
{
    /// <summary>
    /// FluentManager is just an example to show how the FluentScript can be called.
    /// Your needs might differ.
    /// </summary>
    [AddComponentMenu("Fluent/Fluent Manager")]
    public class FluentManager : MonoBehaviour
    {
        List<FluentScript> possibleActions = new List<FluentScript>();
        public GameObject ClosestActionUIText;
        public GameObject PlayerObject;

        [HideInInspector]
        public List<FluentScript> FluentScripts = new List<FluentScript>();

        static FluentManager instance;
        public static FluentManager Instance
        {
            get
            {
                if (instance == null)
                    throw new UnityException("You need to add an FluentManager to your scene");
                return instance;
            }
        }

        public void Awake()
        {
            if (instance == null)
            {
                DontDestroyOnLoad(gameObject);
                instance = this;
            } else if (instance != null)
            {
                Destroy(gameObject);
            }
        }
        
        public bool AddScript(FluentScript fluentScript)
        {
            // Dont add any game actions that are not allowed to be added while active
            if (FluentScripts.Contains(fluentScript))
                return false;

            if (fluentScript == null)
            {
                Debug.LogWarning("You are trying to add a null action to the action manager", this);
                return false;
            }

            if (possibleActions.Contains(fluentScript))
                return true;

            // set the initiator
            possibleActions.Add(fluentScript);

            RecalculateUIActionText();
            return true;
        }

        public void RemoveScript(FluentScript fluentScript)
        {
            possibleActions.Remove(fluentScript);
            RecalculateUIActionText();
        }

        private void RecalculateUIActionText()
        {
            FluentScript closestGameAction = GetClosestAction(PlayerObject);
            if (ClosestActionUIText != null)
            {
                if (closestGameAction != null)
                    ClosestActionUIText.GetComponent<TextMeshProUGUI>().text = closestGameAction.Description();
                else
                    ClosestActionUIText.GetComponent<TextMeshProUGUI>().text = "";
            }
        }

        private FluentScript GetClosestAction(GameObject playerObject)
        {
            //
            // Find the closest game action
            //

            FluentScript closestGameAction = null;
            float closestDistance = float.MaxValue;
            foreach (FluentScript gameAction in possibleActions)
            {
                float distance = (gameAction.gameObject.transform.position - playerObject.transform.position).magnitude;

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestGameAction = gameAction;
                }
            }

            return closestGameAction;
        }

        public void ExecuteAction(FluentScript gameAction)
        {
            if (FluentScripts.Contains(gameAction))
            {
                Debug.LogWarning("This FluentScipt is already active " + gameAction.GetType().Name, gameAction);
                return;
            }

            gameAction.SetDoneCallback(ActionCompleted);
            FluentScripts.Add(gameAction);

            gameAction.Run();
        }

        private void ActionCompleted(FluentScript fluentScript)
        {
            FluentScripts.Remove(fluentScript);
            // The action just completed
            // The action initiator could have been stopped from adding this action as a viable action
            // TODO
            /*
            if (fluentScript.Initiator != null)
            {
                GameAction newGameAction = fluentScript.Initiator.ShouldAddGameAction();
                if (newGameAction != null)
                    AddAction(newGameAction, fluentScript.Initiator);
            }*/

        }

        public void ExecuteClosestAction(GameObject closestToWhat)
        {
            //
            FluentScript closestGameAction = GetClosestAction(closestToWhat);

            if (closestGameAction == null)
                return;

            ExecuteAction(closestGameAction);
        }

    }
}
