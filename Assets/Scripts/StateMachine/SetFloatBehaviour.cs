using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFloatBehaviour : StateMachineBehaviour
{
    public string FloatName;
    public bool UpdateOnStateEnter, UpdateOnStateExit;
    public bool UpdateOnStateMachineEnter, UpdateOnStateMachineExit;
    public float ValueOnEnter, ValueOnExit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        if (UpdateOnStateEnter)
        {
            animator.SetFloat(FloatName, ValueOnEnter);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (UpdateOnStateExit)
        {
            animator.SetFloat(FloatName, ValueOnExit);
        }
    }

    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (UpdateOnStateMachineEnter)
        {
            animator.SetFloat(FloatName, ValueOnExit);
        }
    }

    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (UpdateOnStateMachineExit)
        {
            animator.SetFloat(FloatName, ValueOnExit);
        }
    }
}
