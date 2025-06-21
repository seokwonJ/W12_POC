using System;
using UnityEngine;

public abstract class ArtifactBaseTemplate
{
    public Sprite icon;
    public string title;
    public string explain;
    public bool isPurchased = false;
}

public abstract class ArtifactTemplate : ArtifactBaseTemplate
{
    public Action effect;
    public virtual void Subscribe()
    {
        if (isPurchased)
        {
            Debug.Log("������ ��Ƽ��Ʈ �� ���ŵ�");
            return;
        }
        isPurchased = true;
        Managers.Artifact.ArtifactCounts++;
        effect += Effect;
    }

    public virtual void Effect()
    {
        throw new Exception();
    }
}

public class ArtifactTemplate<T> : ArtifactBaseTemplate
{
    public Action<T> effect;
    public virtual void Subscribe()
    {
        if (isPurchased)
        {
            Debug.Log("������ ��Ƽ��Ʈ �� ���ŵ�");
            return;
        }
        isPurchased = true;
        Managers.Artifact.ArtifactCounts++;
        effect += Effect;
    }

    public virtual void Effect(T data)
    {
        throw new Exception();
    }
}