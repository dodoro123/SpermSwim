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
            sperm.UpdateAxis(GetLeader().GetReverseDirection());
            AddRepulsionForce(sperm);
            sperm.SteerTo(GetLeader().transform.localPosition - GetLeader().GetFormPosition());
            sperm.Simulate();
            
		}
	}

    public void LineUp()
    {
        for (int i = 0; i < _sperms.Count; i++)
        {
            Sperm sperm = _sperms[i];
            sperm.SetAxisPosition(_form.GetMoveDirection() * -1f, _form.GetFormPositionByIndex(i));
            sperm.UpdateWorldOriginalPosition(GetLeader().transform.localPosition - GetLeader().GetFormPosition());
            sperm.Steer(_form.GetMoveDirection(), 1f);
        }
    }

	public Sperm GetLeader()
    {
        if (_sperms.Count > 0)
        {
            return _sperms[0];
        }
        return null;
    }
    public void AddRepulsionForce(Sperm sperm)
    {
        for (int i = 0; i < _sperms.Count; i++)
        {
            Sperm candidate = _sperms[i];
            if (candidate != sperm)
            {
                Vector3 delta = candidate.transform.position - sperm.transform.position;
                if (delta.magnitude < .4f)
                {
                    sperm.Steer(delta*-1f,.05f);
                    candidate.Steer(delta,.05f);
                }
            }
        }
    }
    public bool InSight(Sperm a, Sperm b)
    {
        Vector3 distance = b.transform.localPosition - a.transform.localPosition;
        if (distance.magnitude < 3f)
        {
            float angle = Vector3.Angle(distance.normalized, a.GetDirection());
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
        
        _targetPosition = position;
        _targetPosition.z = 0;

        if (_sperms.Count>0)
        {
            GetLeader().Steer(_targetPosition - GetLeader().transform.position, .4f);
        }
	}

}
