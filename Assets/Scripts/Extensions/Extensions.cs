using Unity.VisualScripting;
using UnityEngine;

public static class Vector2Extensions
{
    /// <summary>
    /// Adds to any x y values of a Vector2
    /// </summary>
    public static Vector2 Add(this Vector2 vector2, float x = 0, float y = 0)
    {
        return new Vector2(vector2.x + x, vector2.y + y);
    }

    /// <summary>
    /// Sets any x y values of a Vector2
    /// </summary>
    public static Vector2 With(this Vector2 vector2, float? x = null, float? y = null)
    {
        return new Vector2(x ?? vector2.x, y ?? vector2.y);
    }
}
public static class Vector3Extensions
{
    /// <summary>
    /// Adds to any x y z values of a Vector3
    /// </summary>
    public static Vector3 Add(this Vector3 vector3, float x = 0, float y = 0, float z = 0)
    {
        return new Vector3(vector3.x + x, vector3.y + y, vector3.z + z);
    }

    /// <summary>
    /// Sets any x y z values of a Vector3
    /// </summary>
    public static Vector3 With(this Vector3 vector3, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? vector3.x, y ?? vector3.y, z ?? vector3.z);
    }
}

