using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI / State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Transition[] transitions;
    public void UpdateState(StateController controller)
    {
        DoAction(controller);
        CheckTransition(controller);
    }

    private void DoAction(StateController controlller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controlller);
        }
    }
    public void CheckTransition(StateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSucceeded = transitions[i].decision.Decide(controller);

            if (decisionSucceeded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }

        }
    }
}
