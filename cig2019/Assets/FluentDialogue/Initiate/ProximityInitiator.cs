using UnityEngine;

namespace Fluent
{

    /// <summary>
    /// Getting close enough to an object will add an action to the action manager
    /// </summary>
    [RequireComponent(typeof(Collider), typeof(FluentScript))]
    public class ProximityInitiator : MonoBehaviour
    {
        void Start()
        {
        }

        void OnTriggerEnter(Collider collider)
        {
            FluentManager.Instance.AddScript(GetComponent<FluentScript>());
        }

        void OnTriggerExit(Collider collider)
        {
            FluentManager.Instance.RemoveScript(GetComponent<FluentScript>());
        }

    }

}
