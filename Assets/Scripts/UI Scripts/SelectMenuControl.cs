using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenuControl : MonoBehaviour
{
    public GameObject selector = null;
    public GameObject player = null;
    public enum SelectMenuControlType
    {
        NONE,
        PICKUPITEM,
    }
    public SelectMenuControlType selectMenuControlType = SelectMenuControlType.NONE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        switch(selectMenuControlType)
        {
            case SelectMenuControlType.PICKUPITEM:
                AddPickUpItemToPlayerActionQueue();
                break;
        }
        this.transform.parent.GetComponent<BuildSelectMenu>().CloseMenu();
    }
    private void AddPickUpItemToPlayerActionQueue()
    {
        Action moveToObject = new Action
        {
            actionType = Action.ActionType.Movement,
            completeType = Action.CompleteType.StoppedMovment,
            ObjectTo = selector.GetComponent<SelectObject>().selectMenuObject
        };
        Action pickUpObject = new Action
        {
            actionType = Action.ActionType.GrabObject,
            completeType = Action.CompleteType.Instant,
            ObjectTo = selector.GetComponent<SelectObject>().selectMenuObject
        };

        moveToObject.AndDo = pickUpObject;
        if (!Utils.CheckActionListForActionType(player.GetComponent<ActionQueue>().actions, Action.ActionType.GrabObject))
        {

            player.GetComponent<ActionQueue>().actions.Add(moveToObject);
        }
    }
}
