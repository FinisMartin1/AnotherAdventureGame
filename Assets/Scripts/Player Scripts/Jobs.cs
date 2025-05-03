using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Jobs : MonoBehaviour
{

    public List<GameObject> InteractingWithObjects = new List<GameObject>();


    public bool IsWoodCutter = false;
    public bool IsBuilder = false;
    public bool IsMiner = false;
    public bool IsHualer = false;
    public bool IsFarmer = false;
    public bool idle = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

       if(IsWoodCutter)
        {
            FindMarkedCuttableTrees();
        }
       if (IsBuilder)
        {
            FindWallTemplate();
            FindCompleteBuildTemplates();
        }
       if(IsMiner)
        {
            FindHarvestableMiningNode();
        }
        if(IsHualer)
        {
            FindBuildTemp();
        }
        if(IsFarmer)
        {
            FindMarkedHarvrestableFarmPlot();
            FindPlantableFarmPlot();
        }
       if(this.GetComponent<ActionQueue>().actions.Count <= 0)
        {
          //  WalkIdle();
   
        }
    }

    private void FindCompleteBuildTemplates()
    {
        List<GameObject> buildTemplates = Utils.GetAllGameObjects().FindAll(o => o.GetComponent<BuildObject>() != null);
        if (buildTemplates != null && buildTemplates.Count > 0)
        {
            buildTemplates = buildTemplates.FindAll(o => o.GetComponent<BuildObject>().buildType == BuildObject.BuildType.BuildObject && o.GetComponent<BuildObject>().isBuilding == false && o.GetComponent<BuildObject>().isReadyToBuild && o.GetComponent<BuildObject>().builder == null);
        }
        foreach(GameObject buildTemplate in buildTemplates)
        {
            buildTemplate.GetComponent<BuildObject>().builder = this.gameObject;
            Action movetoBuildTemplate = new Action()
            {
                actionType = Action.ActionType.Movement,
                completeType = Action.CompleteType.StoppedMovment,
                ObjectTo = buildTemplate
            };
            movetoBuildTemplate.AndDo = new Action()
            {
                actionType = Action.ActionType.BuildObjectTemplate,
                completeType = Action.CompleteType.Instant,
                ObjectTo = buildTemplate
            };
            bool foundObject = false;
            this.GetComponent<ActionQueue>().actions.ForEach(a =>
            {
                if (GameObject.ReferenceEquals(a.ObjectTo, buildTemplate))
                {
                    foundObject = true;
                }
            });
            if (!foundObject)
            {

                if (movetoBuildTemplate != null)
                {

                    this.GetComponent<ActionQueue>().actions.Add(movetoBuildTemplate);
                }

            }
        }
    }
    private void FindBuildTemplate()
    {
        List<GameObject> buildTemplates = Utils.GetAllGameObjects().FindAll(o => o.GetComponent<BuildObject>() != null);
        if(buildTemplates != null && buildTemplates.Count > 0)
        {
            buildTemplates = buildTemplates.FindAll(o => o.GetComponent<BuildObject>().buildType == BuildObject.BuildType.BuildObject && o.GetComponent<BuildObject>().isBuilding == false);
        }
        foreach(GameObject buildTemplate in buildTemplates)
        {
            SetUpActionQueueForObject(buildTemplate);
        }
    }

    private void FindBuildTemp()
    {
        List<GameObject> buildTemplates = Utils.GetAllGameObjects().FindAll(o => o.GetComponent<BuildObject>() != null);
        if (buildTemplates != null && buildTemplates.Count > 0)
        {
            buildTemplates = buildTemplates.FindAll(o=>o.GetComponent<BuildObject>().isBuilding == false);
        }
        foreach(GameObject buildTemplate in buildTemplates)
        {
            SetUpActionQueueToCarryObjectToBuildTemplate(buildTemplate);
        }
        
    }

    private void FindPlantableFarmPlot()
    {
        List<GameObject> farmplots = Utils.GetAllGameObjects().FindAll(o => o.GetComponent<Farmplot>() != null && o.GetComponent<Properties>().claimedBy == null && !o.GetComponent<Farmplot>().hasSeed);
        foreach (GameObject farmplot in farmplots)
        {
            SetUpActionQueueToPlantSeed(farmplot);
        }
    }

    private void SetUpActionQueueToPlantSeed(GameObject farmplot)
    {
        GameObject closetSeed = FindClosestsObjectId(farmplot.GetComponent<Farmplot>().GetSeedId(farmplot.GetComponent<Farmplot>().seed));
        if(closetSeed != null)
        {
            closetSeed.GetComponent<Properties>().claimedBy = this.gameObject;
            farmplot.GetComponent<Properties>().claimedBy = this.gameObject;
            Action moveToSeed = new Action
            {
                actionType = Action.ActionType.Movement,
                completeType = Action.CompleteType.StoppedMovment,
                ObjectTo = closetSeed
            };
            Action pickUpSeed = new Action

            {
                actionType = Action.ActionType.GrabObject,
                completeType = Action.CompleteType.Instant,
                ObjectTo = closetSeed
            };
            Action movetoFarmplot = new Action
            {
                actionType = Action.ActionType.MoveNextTo,
                completeType = Action.CompleteType.StoppedMovment,
                NextTo = farmplot.transform.position
            };
            Action plantSeed = new Action
            {
                actionType = Action.ActionType.PlantSeed,
                completeType = Action.CompleteType.Instant,
                ObjectTo = farmplot
            };
            movetoFarmplot.AndDo = plantSeed;
            pickUpSeed.AndDo = movetoFarmplot;
            moveToSeed.AndDo = pickUpSeed;
            if (moveToSeed != null && !Utils.CheckActionListForActionType(this.GetComponent<ActionQueue>().actions, Action.ActionType.PlantSeed))
            {

                this.GetComponent<ActionQueue>().actions.Add(moveToSeed);
            }
        }
    }

    private void SetUpActionQueueToCarryObjectToBuildTemplate(GameObject buildTemplate)
    {
        foreach(int meterial in buildTemplate.GetComponent<BuildObject>().recipe)
        {
            if(buildTemplate.GetComponent<BuildObject>().contains.Count != buildTemplate.GetComponent<BuildObject>().recipe.Count)
            {
                GameObject closetMeterial = FindClosestsObjectId(meterial);
                if (closetMeterial)
                {
                    closetMeterial.GetComponent<Properties>().claimedBy = this.gameObject;
                    Action moveToMeterial = new Action
                    {
                        actionType = Action.ActionType.Movement,
                        completeType = Action.CompleteType.StoppedMovment,
                        ObjectTo = closetMeterial
                    };
                    //Todo: add way to remove the obeject form contains when action is canceled

                    Action pickUpMeterial = new Action

                    {
                        actionType = Action.ActionType.GrabObject,
                        completeType = Action.CompleteType.Instant,
                        ObjectTo = closetMeterial
                    };

                    Action movetoBuildTemplate = new Action
                    {
                        actionType = Action.ActionType.MoveNextTo,
                        completeType = Action.CompleteType.StoppedMovment,
                        NextTo = buildTemplate.transform.position
                    };

                    Action addObjectToBuildTemplate = new Action
                    {
                        actionType = Action.ActionType.AddToBuildTemplate,
                        completeType = Action.CompleteType.Instant,
                        ObjectTo = buildTemplate
                    };
                    moveToMeterial.AndDo = pickUpMeterial;
                    pickUpMeterial.AndDo = movetoBuildTemplate;
                    movetoBuildTemplate.AndDo = addObjectToBuildTemplate;
   
                        if (moveToMeterial != null && !Utils.CheckActionListForActionType(this.GetComponent<ActionQueue>().actions, Action.ActionType.AddToBuildTemplate))
                        {

                            this.GetComponent<ActionQueue>().actions.Add(moveToMeterial);
                        }

                    
                }
            }
        }
    }

    private void SetUpActionQueueForObject(GameObject buildTemplate)
    {
        foreach(int objectId in buildTemplate.GetComponent<BuildObject>().recipe)
        {
            Action moveToMetrial = null;
            if (!CheckInvatoryForBuildMetrial(1))
            {
                GameObject buildMetrial = FindClosestsObjectId(objectId);
                if (buildMetrial != null)
                {
                    moveToMetrial = new Action
                    {
                        actionType = Action.ActionType.Movement,
                        completeType = Action.CompleteType.StoppedMovment,
                        ObjectTo = buildMetrial
                    };

                    Action grabMetrial = new Action
                    {
                        actionType = Action.ActionType.GrabObject,
                        completeType = Action.CompleteType.Instant,
                        ObjectTo = buildMetrial
                    };

                    moveToMetrial.AndDo = grabMetrial;
                }
                else
                {
                    break;
                }
            }
            Action action = new Action
            {
                actionType = Action.ActionType.MoveNextTo,
                completeType = Action.CompleteType.StoppedMovment,
                NextTo = buildTemplate.transform.position
            };
            Action BuildAction = new Action
            {
                actionType = Action.ActionType.BuildObject,
                completeType = Action.CompleteType.ObjectDestoryed,
                timer = 30.0f - (this.GetComponent<Stats>().constructing),
                ObjectTo = buildTemplate
            };
            if (BuildAction.timer < 0)
            {
                BuildAction.timer = 0;
            }
            action.AndDo = BuildAction;
            buildTemplate.GetComponent<BuildObject>().isBuilding = true;
            Debug.Log(action);
            bool foundObject = false;
            this.GetComponent<ActionQueue>().actions.ForEach(a =>
            {
                if (GameObject.ReferenceEquals(a.ObjectTo, buildTemplate))
                {
                    foundObject = true;
                }
            });
            if (!foundObject)
            {

                if (moveToMetrial != null)
                {
                    moveToMetrial.AndDo.AndDo = action;
                    this.GetComponent<ActionQueue>().actions.Add(moveToMetrial);
                }
                else
                {
                    this.GetComponent<ActionQueue>().actions.Add(action);
                }

            }
        }
    }

    public void FindWallTemplate()
    {
        List<GameObject> wallTemplateObjects = Utils.GetAllGameObjects().FindAll(o => o.GetComponent<BuildObject>() != null);
        if (wallTemplateObjects != null && wallTemplateObjects.Count > 0)
        {
            wallTemplateObjects = wallTemplateObjects.FindAll(o => o.GetComponent<BuildObject>().buildType == 0 && o.GetComponent<BuildObject>().isBuilding == false && o.GetComponent<BuildObject>().objectId==0);
        }
        foreach (GameObject wallTemplete in wallTemplateObjects)
        {
            Action moveToMetrial = null;
            if (!CheckInvatoryForBuildMetrial(1))
            {
                GameObject buildMetrial = FindClosetsMeterial(wallTemplete.GetComponent<BuildObject>().meterial);
                if(buildMetrial != null)
                {
                    moveToMetrial = new Action
                    {
                        actionType = Action.ActionType.Movement,
                        completeType = Action.CompleteType.StoppedMovment,
                        ObjectTo = buildMetrial
                    };

                    Action grabMetrial = new Action
                    {
                        actionType = Action.ActionType.GrabObject,
                        completeType = Action.CompleteType.Instant,
                        ObjectTo = buildMetrial
                    };

                    moveToMetrial.AndDo = grabMetrial;
                }
                else
                {
                    break;
                }
            }
            Action action = new Action
            {
                actionType = Action.ActionType.MoveNextTo,
                completeType = Action.CompleteType.StoppedMovment,
                NextTo = wallTemplete.transform.position
            };
            Action BuildAction = new Action
            {
                actionType = Action.ActionType.BuildObject,
                completeType = Action.CompleteType.ObjectDestoryed,
                timer = 30.0f - (this.GetComponent<Stats>().constructing),
                ObjectTo = wallTemplete
            };
            if(BuildAction.timer < 0)
            {
                BuildAction.timer = 0;
            }
            action.AndDo = BuildAction;
            wallTemplete.GetComponent<BuildObject>().isBuilding = true;
            Debug.Log(action);
            bool foundObject = false;
            this.GetComponent<ActionQueue>().actions.ForEach(a =>
            {
                if (GameObject.ReferenceEquals(a.ObjectTo, wallTemplete))
                {
                    foundObject = true;
                }
            });
            if (!foundObject)
            {

                if (moveToMetrial != null)
                {
                    moveToMetrial.AndDo.AndDo = action;
                    this.GetComponent<ActionQueue>().actions.Add(moveToMetrial);
                }
                else
                {
                    this.GetComponent<ActionQueue>().actions.Add(action);
                }
                
            }
        }
    }

    public void FindMarkedHarvrestableFarmPlot()
    {
        List<GameObject> harvestableFarmPlots = Utils.GetAllGameObjects().FindAll(g => g.GetComponent<Harvestable>() != null && g.GetComponent<Harvestable>().harvestType == Harvestable.HarvestType.Farmplot);
        if(harvestableFarmPlots != null && harvestableFarmPlots.Count > 0)
        {
            harvestableFarmPlots = harvestableFarmPlots.FindAll(g => g.GetComponent<Harvestable>().IsMarkedForHarvest);
        }
        foreach (GameObject harvestableFarmPlot in harvestableFarmPlots)
        {
            Action action = new Action
            {
                actionType = Action.ActionType.Movement,
                completeType = Action.CompleteType.StoppedMovment,
                ObjectTo = harvestableFarmPlot
            };
            Action HarvestAction = new Action
            {
                actionType = Action.ActionType.HavestObjectPlant,
                completeType = Action.CompleteType.Instant,
                timer = 30.0f - (this.GetComponent<Stats>().plantCutting * (int)(this.GetComponent<Stats>().Strength * .25)),
                ObjectTo = harvestableFarmPlot
            };

            action.AndDo = HarvestAction;
            AddActionToActionQueue(action, harvestableFarmPlot);
        }
    }


    public void FindMarkedCuttableTrees()
    {
        List<GameObject> allActiveObject = Utils.GetAllGameObjects();
        List<GameObject> cutableTrees = allActiveObject.FindAll(o => o.GetComponent<Harvestable>() != null && o.GetComponent<Harvestable>().harvestType == Harvestable.HarvestType.tree);
        if (cutableTrees != null &&cutableTrees.Count > 0)
        {
            cutableTrees = cutableTrees.FindAll(o => o.GetComponent<Harvestable>().IsMarkedForHarvest == true);
        }
        foreach (GameObject treeObject in cutableTrees)
        {
            Action action = new Action
            {
                actionType = Action.ActionType.Movement,
                completeType = Action.CompleteType.StoppedMovment,
                ObjectTo = treeObject
            };
            Action HarvestAction = new Action
            {
                actionType = Action.ActionType.HarvestObject,
                completeType = Action.CompleteType.ObjectDestoryed,
                timer = 30.0f - (this.GetComponent<Stats>().plantCutting * (int)(this.GetComponent<Stats>().Strength * .25)),
                ObjectTo = treeObject
            };
            if(HarvestAction.timer < 0)
            {
                HarvestAction.timer = 0;
            }
            action.AndDo = HarvestAction;
            Debug.Log(action);
            bool foundObject = false;
            this.GetComponent<ActionQueue>().actions.ForEach(a =>
            {
                if(GameObject.ReferenceEquals(a.ObjectTo, treeObject))
                {
                    foundObject = true;
                }
            });
            if (!foundObject)
            {
                this.GetComponent<ActionQueue>().actions.Add(action);
            }
            
        }
    }

    private void FindHarvestableMiningNode()
    {
        List<GameObject> allActiveObject = Utils.GetAllGameObjects();
        List<GameObject> minableNode = allActiveObject.FindAll(o => o.GetComponent<Harvestable>() != null && o.GetComponent<Harvestable>().harvestType == Harvestable.HarvestType.rock);
        if (minableNode != null && minableNode.Count > 0)
        {
            minableNode = minableNode.FindAll(o => o.GetComponent<Harvestable>().IsMarkedForHarvest == true);
        }
        foreach (GameObject mineNode in minableNode)
        {
            Action action = new Action
            {
                actionType = Action.ActionType.Movement,
                completeType = Action.CompleteType.StoppedMovment,
                ObjectTo = mineNode
            };
            Action HarvestAction = new Action
            {
                actionType = Action.ActionType.HarvestObject,
                completeType = Action.CompleteType.ObjectDestoryed,
                timer = 30.0f - 1f*(this.GetComponent<Stats>().mining * (int)(this.GetComponent<Stats>().Strength * .25)),
                ObjectTo = mineNode
            };
            if (HarvestAction.timer < 0)
            {
                HarvestAction.timer = 0;
            }
            action.AndDo = HarvestAction;
            Debug.Log(action);
            bool foundObject = false;
            this.GetComponent<ActionQueue>().actions.ForEach(a =>
            {
                if (GameObject.ReferenceEquals(a.ObjectTo, mineNode))
                {
                    foundObject = true;
                }
            });
            if (!foundObject)
            {
                this.GetComponent<ActionQueue>().actions.Add(action);
            }

        }
    }

    private void AddActionToActionQueue(Action action, GameObject gameObject)
    {
        bool foundObject = false;
        this.GetComponent<ActionQueue>().actions.ForEach(a =>
        {
            if (GameObject.ReferenceEquals(a.ObjectTo, gameObject))
            {
                foundObject = true;
            }
        });
        if (!foundObject)
        {
            this.GetComponent<ActionQueue>().actions.Add(action);
        }
    }
    private bool CheckInvatoryForBuildMetrial(int objectId)
    {
        GameObject metrial = this.gameObject.GetComponent<Inventory>().invetory.FirstOrDefault(a => a.GetComponent<Properties>().objectId == objectId);
        if(metrial)
        {
            return true;
        }
        return false;
    }

    private GameObject FindClosetsMeterial(string meterial="")
    {
        List<GameObject> meterialGameObjects = Utils.GetAllGameObjects().FindAll(g => g.name== "Wood Logs(Clone)");
        GameObject closetMetrial = null;
        Vector3 playerPosition = this.gameObject.transform.position;
        List<Action> actions = this.GetComponent<ActionQueue>().actions;
        if(meterialGameObjects.Count > 0 && meterialGameObjects != null)
        {
            float dis = int.MaxValue;
            foreach(GameObject metrial in meterialGameObjects)
            {
                float tempDis = Vector3.Distance(playerPosition, metrial.transform.position);
                if(dis>tempDis)
                {
                    dis = tempDis;
                    if (!CheckIfMetrialIsCliamed(metrial))
                    {
                        closetMetrial = metrial;
                    }
                }
            }
            if(closetMetrial != null)
            {
                return closetMetrial;
            }
        }
        return null;
    }

    private GameObject FindClosestsObjectId(int objectId)
    {
        List<GameObject> meterialGameObjects = Utils.GetAllGameObjects().FindAll(g => g.GetComponent<Properties>() != null);
        meterialGameObjects = meterialGameObjects.FindAll(g => g.GetComponent<Properties>().objectId == objectId && (g.GetComponent<Properties>().claimedBy == null || g.GetComponent<Properties>().claimedBy == this.gameObject));
        GameObject closetMetrial = null;
        Vector3 playerPosition = this.gameObject.transform.position;
        List<Action> actions = this.GetComponent<ActionQueue>().actions;
        if (meterialGameObjects.Count > 0 && meterialGameObjects != null)
        {
            float dis = int.MaxValue;
            foreach (GameObject metrial in meterialGameObjects)
            {
                float tempDis = Vector3.Distance(playerPosition, metrial.transform.position);
                if (dis > tempDis)
                {
                    dis = tempDis;
                    if (!CheckIfMetrialIsCliamed(metrial))
                    {
                        closetMetrial = metrial;
                    }
                }
            }
            if (closetMetrial != null)
            {
                return closetMetrial;
            }
        }
        return null;
    }

    private bool CheckIfMetrialIsCliamed(GameObject gameObject)
    {
        List<Action> actions = this.GetComponent<ActionQueue>().actions;
        foreach(Action action in actions)
        {
            Action tempAction = action;
            while(tempAction.AndDo != null)
            {
                if(action.ObjectTo != null)
                {
                    if(action.ObjectTo == gameObject)
                    {
                        return true;
                    }
                }
                tempAction = tempAction.AndDo;
            }
        }
        return false;
    }

    private void WalkIdle()
    {
        float x = this.gameObject.transform.position.x;
        float y = this.gameObject.transform.position.y;

        x = Random.Range(x - 3, x + 3);
        y = Random.Range(y - 3, y + 3);

        Action idleMove = new Action()
        {
            actionType = Action.ActionType.Movement,
            completeType = Action.CompleteType.StoppedMovment,
            PostionTo = new Vector3(x, y)
        };

        this.GetComponent<ActionQueue>().actions.Add(idleMove);
    }
}
