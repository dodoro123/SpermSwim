using UnityEngine;
using System.Collections;

public class Form  {
    /*
	public int mask = 0;
	public void Mask(byte index)
	{
		mask |= (1 << index);
	}
	public bool IsMasked(byte index)
	{
		return ((mask >> index) & 1) == 1;
	}
	public void Reset()
	{
		mask = 0;
	}
     * */
    public virtual Vector3 GetMoveDirection()
    {
        return Vector3.zero;
    }
    public virtual Vector3 GetFormPositionByIndex(int index)
    {
        return Vector3.zero;
    }
}
