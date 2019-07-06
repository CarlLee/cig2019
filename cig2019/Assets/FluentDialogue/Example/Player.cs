using UnityEngine;
using Fluent;

public class Player : MonoBehaviour 
{
    public float MoveSpeed = 2.0f;

    public bool CanMove = true;
    public static Player Instance;

    void Awake()
    {
        Instance = this;
    }

	void Start () 
    {
	}
	
	void Update () 
    {
        if (!CanMove)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            FluentManager.Instance.ExecuteClosestAction(gameObject);
        }

        transform.position += new Vector3(-Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed, 0, -Input.GetAxis("Vertical") * Time.deltaTime * MoveSpeed);
	
	}
}
