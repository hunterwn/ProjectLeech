using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]
public class RaycastController : MonoBehaviour {

	public LayerMask collisionMask;
	
	public const float skinWidth = .015f;
	const float dstBetweenRays = .25f;
	[HideInInspector]
	public int horizontalRayCount;
	[HideInInspector]
	public int verticalRayCount;

	[HideInInspector]
	public float horizontalRaySpacing;
	[HideInInspector]
	public float verticalRaySpacing;

	[HideInInspector]
	public BoxCollider collider;
	public RaycastOrigins raycastOrigins;

	public virtual void Awake() {
		collider = GetComponent<BoxCollider> ();
	}

	public virtual void Start() {
		CalculateRaySpacing ();
	}

	public void UpdateRaycastOrigins() {
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);

        Vector3 size = collider.size;
        Vector3 center = collider.bounds.center;

        Vector3 xoffset = (gameObject.transform.forward * size.x) / 4;
        Vector3 yoffset = (Vector3.up * size.y) / 6;

        //blue
		raycastOrigins.bottomLeft = center - xoffset - yoffset;
        //white
        raycastOrigins.bottomRight = center + xoffset - yoffset;
        //green
		raycastOrigins.topLeft = center - xoffset + yoffset;
        //yellow
		raycastOrigins.topRight = center + xoffset + yoffset;
	}
	
	public void CalculateRaySpacing() {
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);

		float boundsWidth = bounds.size.x;
		float boundsHeight = bounds.size.y;
		
		horizontalRayCount = Mathf.RoundToInt (boundsHeight / dstBetweenRays);
		verticalRayCount = Mathf.RoundToInt (boundsWidth / dstBetweenRays);
		
		horizontalRaySpacing = (horizontalRayCount > 1)? (bounds.size.y / (horizontalRayCount - 1)) : bounds.size.y;
		verticalRaySpacing = (verticalRayCount > 1)? bounds.size.x / (verticalRayCount - 1) : bounds.size.x;
	}
	
	public struct RaycastOrigins {
		public Vector3 topLeft, topRight;
		public Vector3 bottomLeft, bottomRight;
	}
}
