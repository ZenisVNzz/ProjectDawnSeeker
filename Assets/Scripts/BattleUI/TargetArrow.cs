using System.Collections.Generic;
using UnityEngine;

public class TargetArrow : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public GameObject arrow;
    public GameObject arrowInstances;

    public int segmentCount = 20;
    public float curveHeight = 3f;

    void Start()
    {
        lineRenderer = transform.GetComponentInChildren<LineRenderer>();
        lineRenderer.positionCount = segmentCount + 1;
    }

    public void MakeArrow(Transform attacker, Transform target)
    {
        if (attacker != null && target != null)
        {
            lineRenderer.enabled = true;

            Vector3 startPosition = attacker.position + Vector3.up * 0.85f + Vector3.right * 0.6f;
            Vector3 endPosition = target.position + Vector3.up * 1.15f + Vector3.left * 0.6f;

            Vector3 mid = (startPosition + endPosition) / 2 + Vector3.up * curveHeight;

            for (int i = 0; i <= segmentCount; i++)
            {
                float t = i / (float)segmentCount;
                Vector3 point = CalculateQuadraticBezierPoint(t, startPosition, mid, endPosition);
                lineRenderer.SetPosition(i, point);
            }

            if (arrow != null)
            {
                if (arrowInstances != null)
                {
                    Destroy(arrowInstances);
                }

                GameObject arrowInstance = Instantiate(arrow);

                Vector3 pointA = lineRenderer.GetPosition(segmentCount - 1);
                Vector3 pointB = lineRenderer.GetPosition(segmentCount);

                Vector3 dir = (pointB - pointA).normalized;

                arrowInstance.transform.position = pointB;
                arrowInstance.transform.right = dir;
                arrowInstances = arrowInstance;
            }

        }
        else
        {
            lineRenderer.enabled = false;
            return;
        }    
    }

    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }
}
