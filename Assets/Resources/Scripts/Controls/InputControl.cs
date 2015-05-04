using UnityEngine;
using System.Collections;

public class InputControl : Core.MonoSingleton<InputControl>
{

	// Use this for initialization
	void Start () {
	
	}
    private Vector3 GetCurrentMouseWorldPosition()
    {
        Vector3 world = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 100f);
        world = Camera.main.ViewportToWorldPoint(new Vector3(world.x, world.y, 100f));
        return world;

    }
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {

            AppControl.Instance.HandleTap(GetCurrentMouseWorldPosition());
            
        }
	}
}
