using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsScript : MonoBehaviour
{
    [SerializeField] PlayerMovement pM;

    public void OnAttackEnd()
    {
        pM.canMove = true;
    }
}
