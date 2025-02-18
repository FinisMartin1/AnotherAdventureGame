using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionObject : MonoBehaviour
{

    public enum ActionObjectType
    {
        NONE,
        RELIEFBLADDER
    }

    public ActionObjectType actionObjectType = ActionObjectType.NONE;

    public int multiplier = 1;
    public GameObject pawnUsing = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pawnUsing != null)
        {
            ActivateActionObject();
        }
    }

    public void SetActionObjectType(ActionObjectType actionObjectType)
    {
        this.actionObjectType = actionObjectType;
    }

    public void SetMultiplier(int multiplier)
    {
        this.multiplier = multiplier;
    }

    private void ActivateActionObject()
    {
        switch(actionObjectType)
        {
            case ActionObjectType.RELIEFBLADDER:
                Action waitForBladderToBeFufilled = new Action
                {
                    actionType = Action.ActionType.Wait,
                    completeType = Action.CompleteType.WaitTillNeedIsFulfield,
                    needType = Needs.NeedType.BLADDER,
                    ObjectTo = this.gameObject
                };
                bool actionInUse = false;
                pawnUsing.GetComponent<ActionQueue>().actions.ForEach(a =>
                {
                    if (a.completeType == Action.CompleteType.WaitTillNeedIsFulfield && a.needType == Needs.NeedType.BLADDER)
                    {
                        actionInUse = true;
                    }
                });
                if (!actionInUse)
                {
                    pawnUsing.GetComponent<ActionQueue>().actions.Add(waitForBladderToBeFufilled);
                }
                PeformReliefBladder();
                break;
        }
    }

    private void PeformReliefBladder()
    {
        if(pawnUsing.GetComponent<Needs>()==null || pawnUsing.GetComponent<ActionQueue>() == null)
        {
            pawnUsing = null;
            return;
        }

        if (pawnUsing.GetComponent<ActionQueue>().actions[0] != null && pawnUsing.GetComponent<ActionQueue>().actions[0].completeType == Action.CompleteType.WaitTillNeedIsFulfield && pawnUsing.GetComponent<ActionQueue>().actions[0].needType == Needs.NeedType.BLADDER)
        {
            pawnUsing.GetComponent<Needs>().bladder = pawnUsing.GetComponent<Needs>().bladder + multiplier;
        }

    }
}
