using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneFOVCone : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public NavMeshAgent agent;
    public Player player;
    public GameObject path;
    public bool viewedFlag = false;
    public bool coneFlag = true;
    public Transform projSpawn;
    public float speed = 4.0f;
    public Transform bullet;

    public DroneController droneController;
    public float watchDelay;
    public float shootDelay;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    private float meshResolution = 1.0f;
    private int edgeResolveIterations = 4;
    private float edgeDistThreshold = 0.5f;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    IEnumerator FindTargetsWithDelay (float watchDelay)
    {
        while (!droneController.dead)
        {
            yield return new WaitForSeconds(watchDelay);
            FindVisibleTargets();
        }
    }

    IEnumerator ShootwithDelay (float shootDelay)
    {
        while (!droneController.dead)
        {
            yield return new WaitForSeconds(shootDelay);
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        Transform bulletTransform = Instantiate(bullet, projSpawn.position, Quaternion.identity);
        Vector3 aimPosition = player.transform.position;
        aimPosition += player.velocity;
        aimPosition.y += 1.5f;
        Vector3 shootDir = aimPosition - droneController.transform.position;
         
        bulletTransform.GetComponent<Bullet>().Setup(shootDir);
    }

    void FindVisibleTargets()
    {
        int i;
        float distToTarget;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (i = 0; i < targetsInViewRadius.Length; i++)
        {
            visibleTargets.Clear();
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if ((Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2))
            {
                distToTarget = Vector3.Distance(transform.position, target.position);

                // If statement for if there are no objects in between the target and the script utilizer.
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    if (viewedFlag == false)
                    {
                        viewedFlag = true;
                    }
                    // Player is within sight
                    agent.speed = 3;
                    agent.SetDestination(player.transform.position);

                    if (droneController.attackTimer >= droneController.attackCooldown)
                    {
                        ShootProjectile();
                        agent.speed = 3;
                    }

                    visibleTargets.Add (target);
                }
            }
        }
    }

    void DrawFieldOfView()
    {
        int vertexCount, i;
        bool edgeDistThresholdExceeded;
        int rayCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / rayCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();

        for (i = 0; i <= rayCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            if (i > 0)
            {
                edgeDistThresholdExceeded = Mathf.Abs(oldViewCast.dist - newViewCast.dist) > edgeDistThreshold;
                if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDistThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add (edge.pointA);
                    }

                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add (edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        // Angle is relative to character
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3 (Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        int i;
        bool edgeDistThresholdExceeded;
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            edgeDistThresholdExceeded = Mathf.Abs(minViewCast.dist - newViewCast.dist) > edgeDistThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDistThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }
        return new EdgeInfo(minPoint, maxPoint);
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dist;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dist, float _angle)
        {
            hit = _hit;
            point = _point;
            dist = _dist;
            angle = _angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        StartCoroutine ("FindTargetsWithDelay", watchDelay);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        if(!droneController.dead && coneFlag == true)
        {
            DrawFieldOfView();
        }
    }
}