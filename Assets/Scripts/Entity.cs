using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum FacingDir 
    {
        East,
        West,
        North,
        South
    }

    public FacingDir facingDir = FacingDir.East;

    public Vector2 GetDirection()
    {
        int xValue = 0;
        int yValue = 0;
        
        if (facingDir == FacingDir.East)
        {
            xValue = 1;
            yValue = 0;
        }
        else if (facingDir == FacingDir.West)
        {
            xValue = -1;
            yValue = 0;
        }
        else if (facingDir == FacingDir.North) 
        {
            xValue = 0;
            yValue = 1;
        }
        else if (facingDir == FacingDir.South)
        {
            xValue = 0;
            yValue = -1;
        }

        return new Vector2(xValue, yValue);
    }

    public virtual void SetDirection(Vector2 _input)
    {
        if (_input == Vector2.right)
        {
            facingDir = FacingDir.East;
        }
        else if (_input == Vector2.left)
        {
            facingDir = FacingDir.West;
        }
        else if (_input == Vector2.up)
        {
            facingDir = FacingDir.North;
        }
        else if (_input == Vector2.down)
        {
            facingDir = FacingDir.South;
        }
    }
}
