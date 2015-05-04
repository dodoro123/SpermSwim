using UnityEngine;
using System.Collections;

public class Sperm : MonoBehaviour {


    private Vector3 direction = Vector3.zero;
    private float speed = 3f;
    private Vector3 force = Vector3.zero;
    private Vector3 formPosition;
    
	void Start () {
          
	}

	public void Simulate()
	{

        direction = force;
        //this.transform.position += speed * Time.deltaTime * direction;
        this.transform.localPosition = formPosition;
    }

    public void SetFormPosition(Vector3 position)
    {
        formPosition = position;
        
    }

    public Vector3 GetFormPosition()
    {
        return formPosition;
    }

    public void Steer(Vector3 steerForce, float weight = 1f)
    {
        force += steerForce.normalized * weight;
        force = force.normalized;
    }
    public void Form()
    {
        this.transform.localPosition = formPosition;
        direction = force;
    }
    public Vector3 GetDirection()
    {
        return direction;
    }
}
