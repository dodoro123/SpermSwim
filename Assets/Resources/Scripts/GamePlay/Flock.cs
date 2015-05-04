using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Flock  {

	private Form _form;
	private List<Sperm>_sperms;
    private Vector3 _targetPosition = Vector3.zero;
    public Flock()
    {
		_sperms = new List<Sperm> ();
        _form = new TriangleForm();
	}
	
	public void AddUnit(Sperm sperm)
	{
		if (!_sperms.Contains (sperm)) {
			_sperms.Add (sperm);
		}
	}
	
	public void RemoveUnit(Sperm sperm)
	{
		if (_sperms.Contains (sperm)) {
			_sperms.Remove (sperm);
		}
	}

	public void Simulate()
	{
		for (int i = 0; i < _sperms.Count; i++)
        {
		    
            Sperm sperm = _sperms[i];
            UpdateSperm(sperm);
            sperm.Simulate();
            
		}
	}

    public void LineUp()
    {
        for (int i = 0; i < _sperms.Count; i++)
        {
            Sperm sperm = _sperms[i];
            sperm.SetFormPosition(_form.GetFormPositionByIndex(i));
            sperm.Form();
            sperm.Steer(_form.GetMoveDirection(), 1f);
        }
    }

	public void UpdateSperm(Sperm sperm)
	{
        Vector3 force = Vector3.zero;
        Sperm leader = GetLeader();
        float angle = Vector3.Angle(_form.GetMoveDirection(), leader.GetDirection());
        //Debug.Log("Before " + _form.GetMoveDirection());
        Debug.Log("After " + leader.GetDirection());
        for (int i = 0; i < _sperms.Count; i++)
        {
            Sperm candidate = _sperms[i];
            if (candidate != null)
            {
                
                //candidate.SetFormPosition(RotateVector(candidate.GetFormPosition(), angle));
                    
                //Debug.Log("After " + candidate.GetFormPosition());
                //steerDirection += candidate.GetDirection();
                //positionDirection = (positionDirection + candidate.transform.localPosition) * .5f;
            }
        }
        
        //sperm.Steer(steerDirection,1f);
        
    }
    public Vector3 RotateVector(Vector3 vec, float angle)
    {
        float delta = Mathf.Atan2(vec.y, vec.x);
        float distance = vec.magnitude;
        delta += Mathf.PI * (angle / 180f);
        vec.x = Mathf.Cos(delta);
        vec.y = Mathf.Sin(delta);
        return vec * distance;
    }
    public Sperm GetLeader()
    {
        if (_sperms.Count > 0)
        {
            return _sperms[0];
        }
        return null;
    }
    public bool InSight(Sperm a, Sperm b)
    {
        Vector3 distance = b.transform.localPosition - a.transform.localPosition;
        if (distance.magnitude < 3f)
        {
            float angle = Vector3.Angle(distance.normalized, a.GetDirection());
            //Debug.Log("InSight!!!!!!!!!!!!!" + angle);
            return angle < 90f;
        }
        return false;
    }
    

	public Flock Split()
	{	
		return null;
	}

	public void Follow(Vector3 position)
	{
        
        _targetPosition.x = position.x;
        _targetPosition.y = position.y;
        _targetPosition.z = 0;

       // Debug.Log( _targetPosition.x);
            
        if (_sperms.Count>0)
        {
            //_sperms[0].Steer(_targetPosition - _sperms[0].transform.position, .9f);
        }
	}

}
