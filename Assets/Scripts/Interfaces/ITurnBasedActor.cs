using UnityEngine;

public interface ITurnBasedActor
{
    bool IsAlive { get; }
    void StartTurn();
    void EndTurn();
}
