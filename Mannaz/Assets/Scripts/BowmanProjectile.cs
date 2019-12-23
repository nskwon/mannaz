using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowmanProjectile : MonoBehaviour
{

    public Vector3 vertLeftTopFront = new Vector3(-1, 1, 1);
    public Vector3 vertRightTopFront = new Vector3(1, 1, 1);
    public Vector3 vertRightTopBack = new Vector3(1, 1, -1);
    public Vector3 vertLeftTopBack = new Vector3(-1, 1, -1);

    private Transform target;
    public GameObject BowmanImpactEffect;
    public int shooting = 0;
    public int damage = 10;

    public void Seek (Transform _target)
    {
        target = _target;
    }
    void Start()
    {
        transform.Rotate(0, 270, 270);

        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh;

        // vertices
        Vector3[] vertices = new Vector3[]
        {

            // front face

            // left top front, 0
            vertLeftTopFront,
            // right top front, 1
            vertRightTopFront,
            // left bottom front, 2
            new Vector3(-1,-1,1),
            // right bottom front, 3
            new Vector3(1,-1,1),

            // back face

            // right top back, 4
            vertRightTopBack,
            // left top back, 5
            vertLeftTopBack,
            // right bottom back, 6
            new Vector3(1,-1,-1),
            // left bottom back, 7
            new Vector3(-1,-1,-1),

            // left face

            // left top back, 8
            vertLeftTopBack,
            // left top front, 9
            vertLeftTopFront,
            // left bottom back, 10
            new Vector3(-1,-1,-1),
            // left bottom front, 11
            new Vector3(-1,-1,1),

            // right face

            // right top front, 12
            vertRightTopFront,
            // right top back, 13
            vertRightTopBack,
            // right bottom front, 14
            new Vector3(1,-1,1),
            // right bottom back, 15
            new Vector3(1,-1,-1),

            // top face

            // left top back, 16
            vertLeftTopBack,
            // right top back, 17
            vertRightTopBack,
            // left top front, 18
            vertLeftTopFront,
            // right top front, 19
            vertRightTopFront,

            // bottom face

            // left bottom front, 20
            new Vector3(-1,-1,1),
            // right bottom front, 21
            new Vector3(1,-1,1),
            // left bottom back, 22
            new Vector3(-1,-1,-1),
            // right bottom back, 23
            new Vector3(1,-1,-1)

        };

        // triangles
        int[] triangles = new int[]
        {

            // front face

            // first triangle
            0, 2, 3,
            // second triangle
            3, 1, 0,

            // back face

            // first triangle
            4, 6, 7,
            // second triangle
            7, 5, 4,

            // left face

            // first triangle
            8, 10, 11,
            // second triangle
            11, 9, 8,

            // right face

            // first triangle
            12, 14, 15,
            // second triangle
            15, 13, 12,

            // top face

            // first triangle
            16, 18, 19,
            // second triangle
            19, 17, 16,

            // bottom face

            // first triangle
            20, 22, 23,
            // second triangle
            23, 21, 20

        };

        // UVs
        Vector2[] uvs = new Vector2[]
        {

            // front face 
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            // back face 
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            // left face 
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            // right face 
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            // top face 
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            // bottom face 
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0)

        };

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

    }

    private void Update()
    {
        
        if ( target == null )
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = Time.deltaTime * 10;

        if ( dir.magnitude <= distanceThisFrame ) {
            HitTarget();
            Damage(target);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.Rotate(0, 10, 0);

    }

    void Damage (Transform enemy)
    {
        DummyEnemy e = enemy.GetComponent<DummyEnemy>();

        if ( e != null )
        {
            e.TakeDamage(damage);
        }

    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(BowmanImpactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 0.8f);
        Destroy(gameObject);
        return;
    }

}
