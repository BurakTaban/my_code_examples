using UnityEngine;
using System.Collections.Generic;

public static class MeshOperations
{

.
.
.
.
.

   public static void SubdivideMesh(Mesh mesh, int subdivision)
    {

        for (; subdivision > 0; subdivision--)
        {
            List<Vector3> verts = new List<Vector3>(mesh.vertices);
            List<int> tris = new List<int>(mesh.triangles);

            int triCount = tris.Count / 3;

            Vector3 v1, v2, v3, newV1, newV2, newV3;

            int a, b, c, k, l, m;


            for (int i = 0; i < triCount; i++)
            {
                a = tris[i * 3];
                b = tris[i * 3 + 1];
                c = tris[i * 3 + 2];

                v1 = verts[a];
                v2 = verts[b];
                v3 = verts[c];

                newV1 = (v1 + v2) * 0.5f;
                newV2 = (v2 + v3) * 0.5f;
                newV3 = (v3 + v1) * 0.5f;

                k = IndexOfVector3(verts, newV1);
                if (k == -1)
                {
                    k = verts.Count;
                    verts.Add(newV1);
                }
                l = IndexOfVector3(verts, newV2);
                if (l == -1)
                {
                    l = verts.Count;
                    verts.Add(newV2);
                }
                m = IndexOfVector3(verts, newV3);
                if (m == -1)
                {
                    m = verts.Count;
                    verts.Add(newV3);
                }

                tris[i * 3] = a; tris[i * 3 + 1] = k; tris[i * 3 + 2] = m;
                tris.Add(k); tris.Add(b); tris.Add(l);
                tris.Add(l); tris.Add(c); tris.Add(m);
                tris.Add(k); tris.Add(l); tris.Add(m);
 
            }
           
            mesh.vertices = verts.ToArray();
            mesh.triangles = tris.ToArray();
        }
    }

.
.
.
.
.


}

