using System.Collections;
using UnityEngine;

public interface IGameEngine
{
    IInputDevice GetInputDevice();
    Coroutine StartCoroutine(IEnumerator routine);
    void StopCoroutine(Coroutine routine);
}