using UnityEngine;
using System.Collections.Generic;

public class CurvedEdgeCollider2D : MonoBehaviour
{
    [Range(0, 120)]
    public int softness;

    public bool isTrigger;

    public PhysicsMaterial2D physicMaterial;

    public int edgesPerSegment
    {
        get { return softness + 1; }
    }

    public int segmentCount
    {
        get { return points.Count - 1; }
    }

    public int pointCount
    {
        get { return points.Count; }
    }

    public float scaleX
    {
        get { return transform.localScale.x; }
    }

    public float scaleY
    {
        get { return transform.localScale.y; }
    }

    public List<Vector2> points;

    [HideInInspector]
    public List<Vector2> scaledPoints;

    [HideInInspector]
    public List<Vector2> softPoints;
    
    public List<TangentInfo> tangents;

    [HideInInspector]
    public List<TangentInfo> scaledTangents;
    
    [HideInInspector]
    public bool initialized;


    [HideInInspector]
    public Color leftTangentColor, rightTangentColor;
    
    
    [HideInInspector]
    public EdgeCollider2D edgeCollider;


    void Awake()
    {

        edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        edgeCollider.isTrigger = isTrigger;
        edgeCollider.sharedMaterial = physicMaterial;

        edgeCollider.points = softPoints.ToArray();
        

        Vector2 v;

        Vector2[] p = edgeCollider.points;

        for (int i = 0; i < p.Length; i++)
        {
            v = p[i];
            v.x /= scaleX;
            v.y /= scaleY;
            p[i] = v;
        }

        edgeCollider.points = p;

    }


    

    public void CalculateSofPoints()
    {
        softPoints = new List<Vector2>();

        float u;

        Vector2 p1, p2, p3, p4;
        Vector2 result;

        for (int i = 0; i < segmentCount; i++)
        {
            softPoints.Add(scaledPoints[i]);

            p1 = scaledPoints[i];
            p2 = scaledTangents[i].second.target * 2 - scaledTangents[i].second.source;
            p3 = scaledPoints[i + 1];
            p4 = scaledTangents[i + 1].first.target * 2 - scaledTangents[i + 1].first.source;

            for (int j = 0; j < softness; j++)
            {
                u = (j + 1) / (float)edgesPerSegment;

                result = (1 - u) * (1 - u) * (1 - u) * p1 + 3 * (1 - u) * (1 - u) * u * p2 + 3 * (1 - u) * u * u * p4 + u * u * u * p3;

                softPoints.Add(result);
            }
        }

        softPoints.Add(scaledPoints[scaledPoints.Count - 1]);



        edgeCollider.points = softPoints.ToArray();


        Vector2 v;

        Vector2[] p = edgeCollider.points;

        for (int i = 0; i < p.Length; i++)
        {
            v = p[i];
            v.x /= scaleX;
            v.y /= scaleY;
            p[i] = v;
        }
    }


}
