using UnityEngine;

public interface IBuffable
{
    void ApplyBuff(string buffName, int duration);
    void RemoveBuff(string buffName);
}
