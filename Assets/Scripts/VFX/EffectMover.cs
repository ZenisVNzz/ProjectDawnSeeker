using DG.Tweening;
using System;
using UnityEngine;

public class EffectMover : MonoBehaviour
{
    public Vector3 target;
    public float speed = 10f;
    public Action onHit;

    public void MoveToTarget()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        float distance = Vector3.Distance(transform.position, target);
        float duration = distance / speed;

        transform.DOMove(target + Vector3.left * 0.2f, duration)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                onHit?.Invoke();
            });
    }
}
