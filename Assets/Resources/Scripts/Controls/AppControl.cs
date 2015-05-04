using UnityEngine;
using System.Collections;

public class AppControl : Core.MonoSingleton<AppControl>
{

    private Flock _flock;
    
	void Start () {
        _flock = new Flock();
        InitFlock();
	}
	
	void Update () {
        _flock.Simulate();
	}
    public void HandleTap(Vector3 position)
    {
        if (_flock != null)
        {
            _flock.Follow(position);
        }
        
    }
    public void InitFlock()
    {
        for (int i = 0; i < 10; i++)
        {
            _flock.AddUnit(EntityPool.Instance.Use<Sperm>());
        }
        _flock.LineUp();
    }
}
