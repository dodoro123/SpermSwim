using UnityEngine;
using System.Collections;

public class TriangleForm : Form {
    private float gap = .5f;
    public override Vector3 GetMoveDirection()
    {
        return Vector3.left;
    }
    public override Vector3 GetFormPositionByIndex(int index)
    {
        int iPosition = 0;
        int seg = 1;
        int iGrid = 0;
        while (iPosition <= index)
        {
            iPosition += seg;
            ++seg;
            
        }
        iGrid = (seg - iPosition + index);
        float x = (float)seg * gap;
        float y = ((float)seg / 2f) * gap - (float)iGrid * gap;
        return new Vector3(x, y, 0);
    }
}
