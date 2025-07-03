using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ReturnToOriginalPosition", story: "[Im] return to [originalPosition]", category: "Action", id: "bba9c696a15787b107b7609a913fd76d")]
public partial class ReturnToOriginalPositionAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterInBattle> Im;
    [SerializeReference] public BlackboardVariable<Vector3> OriginalPosition;

    private Vector3 startPos;
    private Vector3 targetPos;
    private float timer;
    private float distance;
    private float speed = 11f;
    private Animator animator;

    protected override Status OnStart()
    {
        startPos = Im.Value.transform.position;
        targetPos = OriginalPosition.Value;
        distance = Vector3.Distance(startPos, targetPos);
        timer = 0f;

        animator = Im.Value.transform.GetComponent<Animator>();
        animator.Play("Move");

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01((timer * speed) / distance);

        Im.Value.transform.position = Vector3.Lerp(startPos, targetPos, t);

        if (t >= 1f)
        {
            Im.Value.transform.position = targetPos;
            animator.Play("Idle");
            Im.Value.OnAttackEnd();
            Im.Value.EndAllWalkSound();
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

