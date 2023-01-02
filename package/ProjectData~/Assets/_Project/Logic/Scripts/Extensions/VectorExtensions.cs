using UnityEngine;

public static class VectorExtensions
{
    public static Vector3 SetX(this Vector3 vec, float newX)
    {
        return new Vector3(newX, vec.y, vec.z);
    }
    
    public static Vector3 SetY(this Vector3 vec, float newY)
    {
        return new Vector3(vec.x, newY, vec.z);
    }
    
    public static Vector3 SetZ(this Vector3 vec, float newZ)
    {
        return new Vector3(vec.x, vec.y, newZ);
    }
    
    public static Vector2 SetX(this Vector2 vec, float newX)
    {
        return new Vector2(newX, vec.y);
    }
    
    public static Vector2 SetY(this Vector2 vec, float newY)
    {
        return new Vector2(vec.x, newY);
    }
}
