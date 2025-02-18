using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Needs : MonoBehaviour
{
    public enum NeedType
    {
        HUNGER,
        THIRST,
        BLADDER,
        SOCIAL,
        SLEEP,
        FUN,
        HYGIENE
    }
    public float hunger = 35;
    public float thrist = 100;
    public float bladder = 100;
    public float social = 100;
    public float sleep = 100;
    public float fun = 100;
    public float hygiene = 100;
    bool fulfillingThirst = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        hunger = hunger - (float).005;
        thrist = thrist - (float).014;
        bladder = bladder - (float).014;
        social = social - (float).0005;
        sleep = sleep - (float).001;
        fun = fun - (float).005;
        hygiene = hygiene - (float).001;

        if (hunger < 0)
        {
            hunger = 0;
        }
        if (thrist < 0)
        {
            thrist = 0;
        }
        if (social < 0)
        {
            social = 0;
        }
        if (sleep < 0)
        {
            sleep = 0;
        }
        if (fun < 0)
        {
            fun = 0;
        }

        if(hunger < 30)
        {
            SeekNearestFood();
        }
        
        if(thrist < 30 && !fulfillingThirst)
        {
            //SeekNearestWaterSouce();
        }

        if(bladder <=0)
        {
            bladder = 100;
        }
        if(bladder < 30)
        {
            SeekNearestToilet();
        }
    }

    private void SeekNearestToilet()
    {
        List<GameObject> allActiveObject = Utils.GetAllGameObjects();
        List<GameObject> toilets = allActiveObject.FindAll(o => o.GetComponent<Properties>() != null && o.GetComponent<Properties>().objectId == 7 && o.GetComponent<ActionObject>() != null && o.GetComponent<ActionObject>().pawnUsing == null);
        float dis = int.MaxValue;
        GameObject closetToilet = null;
        Vector3 playerPosition = this.gameObject.transform.position;
        foreach (GameObject toilet in toilets)
        {
            float tempDis = Vector3.Distance(playerPosition, toilet.transform.position);
            if (dis > tempDis)
            {
                dis = tempDis;

                closetToilet = toilet;

            }
        }
        if(closetToilet != null)
        {
            Action moveToToilet = new Action
            {
                actionType = Action.ActionType.Movement,
                completeType = Action.CompleteType.StoppedMovment,
                ObjectTo = closetToilet
            };
            bool actionInUse = false;
            this.gameObject.GetComponent<ActionQueue>().actions.ForEach(a =>
            {
                if (a.actionType == Action.ActionType.Movement && a.ObjectTo.GetComponent<Properties>().objectId == 7)
                {
                    actionInUse = true;
                }
            });
            if(!actionInUse)
            {
                this.GetComponent<ActionQueue>().actions.Add(moveToToilet);
                closetToilet.GetComponent<ActionObject>().pawnUsing = this.gameObject;
            }
        }
    }

    private void SeekNearestFood()
    {
        List<GameObject> allActiveObject = Utils.GetAllGameObjects();
        List<GameObject> foods = allActiveObject.FindAll(o => o.GetComponent<Properties>() != null && o.GetComponent<Properties>().objectType == Properties.ObjectType.FOOD && !o.GetComponent<Properties>().claimedBy);
        float dis = int.MaxValue;
        GameObject closetFood = null;
        Vector3 playerPosition = this.gameObject.transform.position;
        foreach (GameObject food in foods)
        {
            float tempDis = Vector3.Distance(playerPosition, food.transform.position);
            if (dis > tempDis)
            {
                dis = tempDis;

                closetFood = food;
         
            }
        }

        if(closetFood)
        {
            closetFood.GetComponent<Properties>().claimedBy = this.gameObject;
            Action moveToFood = new Action()
            {
                actionType = Action.ActionType.Movement,
                completeType = Action.CompleteType.StoppedMovment,
                ObjectTo = closetFood
            };
            Action grabFood = new Action()
            {
                actionType = Action.ActionType.GrabObject,
                completeType = Action.CompleteType.Instant,
                ObjectTo = closetFood
            };
            Action eatFood = new Action()
            {
                actionType = Action.ActionType.EatFood,
                completeType = Action.CompleteType.Instant,
                ObjectTo = closetFood
            };
            grabFood.AndDo = eatFood;
            moveToFood.AndDo = grabFood;
            this.GetComponent<ActionQueue>().actions.Add(moveToFood);
        }
    }

    private void SeekNearestWaterSouce()
    {
        foreach(Action action in this.GetComponent<ActionQueue>().actions)
        {
            if(Action.SearchActionNodeForActionType(action, Action.ActionType.DrinkFromContainerInInv))
            {
                return;
            }
            if (Action.SearchActionNodeForActionType(action, Action.ActionType.DrinkFromSource))
            {
                return;
            }
        }
        List<GameObject> allActiveObject = Utils.GetAllGameObjects();
        List<GameObject> waterSources = allActiveObject.FindAll(g => g.name == "WaterTile" || (g.GetComponent<Bottle>() != null && g.GetComponent<Bottle>().contains == Fluids.FluidSets.WATER && !g.GetComponent<Properties>().claimedBy));
        float dis = int.MaxValue;
        GameObject closetWaterSource = null;
        Vector3 playerPosition = this.gameObject.transform.position;
        if (waterSources != null)
        {
            foreach (GameObject waterSource in waterSources)
            {
                float tempDis = Vector3.Distance(playerPosition, waterSource.transform.position);
                if (dis > tempDis)
                {
                    dis = tempDis;

                    closetWaterSource = waterSource;

                }
            }

            if (closetWaterSource)
            {
                if (closetWaterSource.GetComponent<Bottle>() != null)
                {
                    closetWaterSource.GetComponent<Properties>().claimedBy = this.gameObject;
                    Action moveToContainer = new Action()
                    {
                        actionType = Action.ActionType.Movement,
                        completeType = Action.CompleteType.StoppedMovment,
                        ObjectTo = closetWaterSource
                    };
                    Action grabContainer = new Action()
                    {
                        actionType = Action.ActionType.GrabObject,
                        completeType = Action.CompleteType.Instant,
                        ObjectTo = closetWaterSource
                    };
                    Action drinkFromContainer = new Action()
                    {
                        actionType = Action.ActionType.DrinkFromContainerInInv,
                        completeType = Action.CompleteType.Instant,
                    };
                    grabContainer.AndDo = drinkFromContainer;
                    moveToContainer.AndDo = grabContainer;
                    this.GetComponent<ActionQueue>().actions.Add(moveToContainer);
                }
                else
                {
                    Action moveToWaterSource = new Action()
                    {
                        actionType = Action.ActionType.MoveNextTo,
                        completeType = Action.CompleteType.StoppedMovment,
                        NextTo = closetWaterSource.transform.position
                    };
                    Action drinkFromWaterSource = new Action()
                    {
                        actionType = Action.ActionType.DrinkFromSource,
                        completeType = Action.CompleteType.Instant
                    };
                    moveToWaterSource.AndDo = drinkFromWaterSource;
                    this.GetComponent<ActionQueue>().actions.Add(moveToWaterSource);
                }
            }
        }
    }
}
