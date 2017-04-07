using UnityEngine;
using System.Collections.Generic;

public static class LineOperations
{

.
.
.
.
.
    static Vector3 a, b, c;

    public static bool LineIntersection(Line first, Line second)
    {
        a = first.a; b = first.b; c = second.a; d = second.b;

        float ax = a.x, ay = a.y, bx = b.x, by = b.y, cx = c.x, cy = c.y, dx = d.x, dy = d.y;

        float m1 = (b.y - a.y) / (b.x - a.x);
        float m2 = (d.y - c.y) / (d.x - c.x);

        float ex = (m1 * ax - m2 * cx + cy - ay) / (m1 - m2);

        float xMin, xMax;

        if (ax > bx)
        {
            xMin = bx; xMax = ax;
        }
        else
        {
            xMin = ax; xMax = bx;
        }


        if (ex < xMax && ex > xMin)
        {

        }
        else
        {
            return false;
        }

        if (cx > dx)
        {
            xMin = dx; xMax = cx;
        }
        else
        {
            xMin = cx; xMax = dx;
        }

        if (ex < xMax && ex > xMin)
        {
            return true;
        }

        return false;
    }

.
.
.
.
.

}

