using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public GameObject ObjectTo = null;
    public Vector3 PostionTo;
    public Vector3 NextTo;
    
    public enum ActionType
    {
        NoAction,
        Movement,
        HarvestObject,
        BuildObject,
        MoveNextTo,
        GrabObject,
        EatFood,
        DrinkFromContainerInInv,
        DrinkFromSource,
        AddToBuildTemplate,
        BuildObjectTemplate
    }

    public enum CompleteType
    {
        Instant,
        ObjectDestoryed,
        StoppedMovment
    }

    public ActionType actionType = ActionType.NoAction;
    public CompleteType completeType = CompleteType.Instant;

    public bool IsCompleted = false;
    public bool IsPreforming = false;
    public Action? AndDo = null;
    public float timer = 0.0f;



    public Action GetNextAction()
    {
        return AndDo;
    }

    public static bool SearchActionNodeForActionType(Action action, ActionType actionType)
    {
        while(action.AndDo != null)
        {
            if(action.actionType == actionType)
            {
                return true;
            }
            action = action.AndDo;
        }
        if (action.actionType == actionType)
        {
            return true;
        }
        return false;
    }
}
