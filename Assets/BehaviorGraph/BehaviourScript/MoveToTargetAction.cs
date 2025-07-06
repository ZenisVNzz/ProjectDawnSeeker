using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToTarget", story: "[Im] move to [target]", category: "Action", id: "0f2de51d1db18faff83fb14d2c5f86b6")]
public partial class MoveToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> Im;
    [SerializeReference] public BlackboardVariable<CharacterRuntime> Target;
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
        OriginalPosition.Value = startPos;

        if (Im.Value.characterType == characterType.Player)
            targetPos = Target.Value.transform.position + new Vector3(-1.8f, 0, 0);
        else
            targetPos = Target.Value.transform.position + new Vector3(1.8f, 0, 0);

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
            Im.Value.EndAllWalkSound();
            return Status.Success;      
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

