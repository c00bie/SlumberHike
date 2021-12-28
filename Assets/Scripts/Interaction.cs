using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    public virtual bool IsAsync { get; private set; } = false;

    public abstract void DoAction();
    public abstract IEnumerator DoActionAsync();
}
