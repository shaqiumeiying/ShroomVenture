using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Piggy piggy;

    private void Awake()
    {
        piggy = GetComponentInParent<Piggy>();
    }

    public void FinishRespawn()
    {
        piggy.RespawnFinished(true);
    }
}
