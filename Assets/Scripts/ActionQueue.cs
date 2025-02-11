using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionQueue : MonoBehaviour
{
    public List<Action> actions = new List<Action>();
    public float timer = 0;
    private GameObject objectCreator = new GameObject();

    

    // Start is called before the first frame update
    void Start()
    {
        objectCreator = Utils.GetAllGameObjects().First(g => g.name == "Object Creator");
    }

    // Update is called once per frame
    void Update()
    {

        if(actions.Count>0)
        {
            if (!actions[0].IsPreforming)
            {
                if (actions[0].IsCompleted)
                {

                    if (actions[0].AndDo != null)
                    {
                        actions.Insert(1, actions[0].AndDo);
                    }
                    actions.RemoveAt(0);
                }
                else 
                {
                    preformAction(actions[0]);
                }
            }
            else
            {
                if(actions[0].completeType== Action.CompleteType.StoppedMovment)
                {
                    if(this.GetComponent<PlayerPathfinding>().pathVectorList==null)
                    {

                        actions[0].IsCompleted = true;
                        actions[0].IsPreforming = false;
                    }
                }
                if(actions[0].actionType== Action.ActionType.HarvestObject)
                {
                    timer -= Time.deltaTime;
                    if(timer <= 0.0f)
                    {
                        Destroy(actions[0].ObjectTo);
                        actions[0].IsCompleted = true;
                        actions[0].IsPreforming = false;
                    }
                }
                if(actions[0].completeType == Action.CompleteType.ObjectDestoryed)
                {
                    if (actions[0].ObjectTo == null)
                    {
                        actions[0].IsCompleted = true;
                        actions[0].IsPreforming = false;
                    }
                }
                if (actions[0].completeType == Action.CompleteType.Instant)
                {
                    actions[0].IsCompleted = true;
                    actions[0].IsPreforming = false;
                }
            }
        }
    }

    private void preformAction(Action action)
    {
        if (action.IsPreforming == false)
        {

            switch (action.actionType)
            {
                case Action.ActionType.NoAction:
                    break;
                case Action.ActionType.Movement:
                    this.PreformMovementAction(action);
                    break;
                case Action.ActionType.HarvestObject:
                    timer = action.timer;
                    break;
                case Action.ActionType.BuildObject:
                    PreformBuildAction(action);
                    break;
                case Action.ActionType.MoveNextTo:
                    this.PreformMoveNextToAction(action);
                    break;
                case Action.ActionType.GrabObject:
                    this.PreformGrabAction(action);
                    break;
                case Action.ActionType.EatFood:
                    this.PreformEatAction(action);
                    break;
                case Action.ActionType.DrinkFromContainerInInv:
                    this.PreformDrinkFromInventoryAction(action);
                    break;
                case Action.ActionType.DrinkFromSource:
                    this.PreformDrinkFromSourceAction(action);
                    break;
                case Action.ActionType.AddToBuildTemplate:
                    this.PreformAddToBuildTemplateAction(action);
                    break;
                case Action.ActionType.BuildObjectTemplate:
                    this.PreformBuildBuildTemplateAction(action);
                    break;
                default:
                    Debug.LogError("Action type was not found");
                    actions[0].IsCompleted = true;
                    break;

            }
        }
        actions[0].IsPreforming = true;
    }

    private void PreformMovementAction(Action action)
    {
        Debug.Log(action.NextTo);
        if (action.ObjectTo != null)
        {
            this.gameObject.GetComponent<PlayerPathfinding>().SetTargetPostionToObject(action.ObjectTo);
        }
        else if (action.PostionTo != null)
        {
            this.gameObject.GetComponent<PlayerPathfinding>().SetTargetPosition(action.PostionTo);
        }

    }

    private void PreformMoveNextToAction(Action action)
    {
        if (action.NextTo != null)
        {
            this.gameObject.GetComponent<PlayerPathfinding>().SetTargetPostionNexToPostion(action.NextTo);
        }
    }

    private void PreformGrabAction(Action action)
    {
        this.gameObject.GetComponent<Inventory>().invetory.Add(action.ObjectTo);
        action.ObjectTo.SetActive(false);
    }

    private void PreformBuildAction(Action action)
    {
        objectCreator.GetComponent<ObjectCreator>().createWall(action.ObjectTo.transform.position);
        GameObject meterial;
        switch (action.ObjectTo.GetComponent<BuildObject>().meterial)
        {
            case "Wood Logs(Clone)":
                meterial = this.GetComponent<Inventory>().invetory.FirstOrDefault(g => g.name == "Wood Logs(Clone)");
                if(meterial != null)
                {
                    this.GetComponent<Inventory>().invetory.Remove(meterial);
                }
                break;
        }
        Destroy(action.ObjectTo);
    }

    private void PreformEatAction(Action action)
    {
        List<GameObject> foodsInInvitory = this.GetComponent<Inventory>().invetory.FindAll(f => f.GetComponent<Properties>() != null && f.GetComponent<Properties>().objectType == Properties.ObjectType.FOOD);
        if (foodsInInvitory != null || foodsInInvitory.Count >0)
        {
            GameObject food = foodsInInvitory.FirstOrDefault();
            this.GetComponent<Needs>().hunger = this.GetComponent<Needs>().hunger + food.GetComponent<Properties>().nutrition;
            this.GetComponent<Inventory>().invetory.Remove(food);
            Destroy(food);

        }
        
    }

    private void PreformDrinkFromInventoryAction(Action action)
    {
        List<GameObject> drinksInInvitory = this.GetComponent<Inventory>().invetory.FindAll(d => d.GetComponent<Bottle>() != null && d.GetComponent<Bottle>().contains == Fluids.FluidSets.WATER);
        if(drinksInInvitory != null || drinksInInvitory.Count > 0)
        {
            GameObject drink = drinksInInvitory.FirstOrDefault();
            this.GetComponent<Needs>().hunger = this.GetComponent<Needs>().thrist = 100;
            drink.transform.position = this.gameObject.transform.position;
            drink.GetComponent<Bottle>().contains = Fluids.FluidSets.EMPTY;
            drink.SetActive(true);
            this.GetComponent<Inventory>().invetory.Remove(drink);
        }
    }

    private void PreformAddToBuildTemplateAction(Action action)
    {
        if(action.ObjectTo.GetComponent<BuildObject>()!=null)
        {
            foreach(int rec in action.ObjectTo.GetComponent<BuildObject>().recipe)
            {
                if(!action.ObjectTo.GetComponent<BuildObject>().contains.Contains(rec))
                {
                    GameObject meterial = this.gameObject.GetComponent<Inventory>().invetory.Find(g => g.GetComponent<Properties>().objectId == rec);
                    if(meterial != null)
                    {
                        action.ObjectTo.GetComponent<BuildObject>().contains.Add(rec);
                        this.gameObject.GetComponent<Inventory>().invetory.Remove(meterial);
                    }
                }
            }
       
        }
    }

    private void PreformBuildBuildTemplateAction(Action action)
    {
        objectCreator.GetComponent<ObjectCreator>().createObject(5, action.ObjectTo.transform.position);
        Destroy(action.ObjectTo);
    }
    private void PreformDrinkFromSourceAction(Action action)
    {
        this.GetComponent<Needs>().hunger = this.GetComponent<Needs>().thrist = 100;
    }
}
