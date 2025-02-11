using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

//ToDo: Create system to place single build square
public class SelectObject : MonoBehaviour
{
    Camera camera;
    [SerializeField]
    public RectTransform selectionBox;
    public Rect actualSelectionBox;


    public Vector2 startPostion;
    public Vector2 endPostion;
    public Vector2 mouseStartPostion;
    public Vector2 mouseEndPostion;
    public string meterial = "";


    public List<GameObject> selectedObjects = new List<GameObject>();
    public enum Selectors
    {
        noSelector,
        InRectangle
    }
    public enum ActionSelector
    {
        noAction,
        ChopWood,
        BuildWall,
        Mine,
        BuildObjectBed
    }
    private Selectors selectors = Selectors.InRectangle;
    public ActionSelector actionSelector = ActionSelector.BuildObjectBed;
    public bool actionButtonPressed = false;
    public int buildObjectId = 5;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        startPostion = Vector2.zero;
        endPostion = Vector2.zero;
        mouseStartPostion = Vector2.zero;
        mouseEndPostion = Vector2.zero;
        DrawVisual();
    }

    // Update is called once per frame
    void Update()
    {
        PeformKeyActions();
        if (Input.GetMouseButtonDown(0))
        {
            selectedObjects.Clear();
            if (actionSelector == ActionSelector.BuildWall || actionSelector == ActionSelector.BuildObjectBed)
            {
                startPostion = Utils.GetMouseWorldPostion();
                startPostion = new Vector2(Mathf.Floor(startPostion.x / (float).7) * (float).7, Mathf.Floor(startPostion.y / (float).7) * (float).7);
                mouseStartPostion = Utils.GetMouseWorldPostion();
                actualSelectionBox = new Rect();
            }
            else
            {
                startPostion = Utils.GetMouseWorldPostion();
                mouseStartPostion = Utils.GetMouseWorldPostion();
                actualSelectionBox = new Rect();
            }

        }
        if (Input.GetMouseButton(0))
        {
            if (actionSelector == ActionSelector.BuildWall || actionSelector == ActionSelector.BuildObjectBed)
            {
                endPostion = Utils.GetMouseWorldPostion();
                endPostion = new Vector2((Mathf.Floor(endPostion.x / (float).7) * (float).7) + (float).7, (Mathf.Floor(endPostion.y / (float).7) * (float).7) + (float).7);
                var temp = startPostion.x - endPostion.x / .4;
                var tempy = startPostion.y - endPostion.y / .4;

                mouseEndPostion = Utils.GetMouseWorldPostion();
            }
            else
            {
                endPostion = Utils.GetMouseWorldPostion();
                mouseEndPostion = Utils.GetMouseWorldPostion();
            }
            DrawVisual();
            DrawSelection();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (mouseStartPostion == mouseEndPostion)
            {
                GameObject selectedObject = Utils.GetGameObjectAtMousePossition();
                if (selectedObject)
                {

                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        selectedObjects.Add(selectedObject);
                    }
                    else
                    {
                        selectedObjects.Clear();
                        selectedObjects.Add(selectedObject);
                    }
                }

            }
            SelectObjects();
            if (actionSelector == ActionSelector.BuildWall)
            {
                CreateWall();
            }
            if(actionSelector == ActionSelector.BuildObjectBed)
            {
                CreateBuildObjectSquare();
            }
            startPostion = Vector2.zero;
            endPostion = Vector2.zero;
            mouseStartPostion = Vector2.zero;
            mouseEndPostion = Vector2.zero;
            DrawVisual();

            SetSelector();
            CheckUISelection();
        }
    }

    public void SetActionSelector(ActionSelector actionSelector)
    {
        this.actionSelector = actionSelector;
    }

    public void SetSelector( Selectors selector)
    {
        this.selectors = selector;
    }
    private void CheckUISelection() 
    {
        List<GameObject> selectedObjectUIObjects = selectedObjects.FindAll(g => g.tag == "UI");
        if (selectedObjectUIObjects.Count > 0)
        {
            if (selectedObjectUIObjects.Find(so => so.name == "Chop Tree Menu"))
            {
                selectors = Selectors.InRectangle;
                actionSelector = ActionSelector.ChopWood;
            } else if (selectedObjectUIObjects.Find(so => so.name == "Build Menu"))
            {
                selectors = Selectors.InRectangle;
                actionSelector = ActionSelector.BuildWall;
            }
        }
    }

        private void DrawVisual()
        {

            Vector2 boxStart = startPostion;
            Vector2 boxEnd = endPostion;
            Vector2 boxCenter = (boxStart + boxEnd) / 2;
            selectionBox.position = boxCenter;
            Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
            selectionBox.sizeDelta = boxSize;
        }

        private void DrawSelection()
        {
            if (Utils.GetMouseWorldPostion().x < mouseStartPostion.x)
            {
                actualSelectionBox.xMin = Utils.GetMouseWorldPostion().x;
                actualSelectionBox.xMax = mouseStartPostion.x;
            }
            else
            {
                actualSelectionBox.xMin = mouseStartPostion.x;
                actualSelectionBox.xMax = Utils.GetMouseWorldPostion().x;
            }

            if (Utils.GetMouseWorldPostion().y < mouseStartPostion.y)
            {
                actualSelectionBox.yMin = Utils.GetMouseWorldPostion().y;
                actualSelectionBox.yMax = mouseStartPostion.y;
            }
            else
            {
                actualSelectionBox.yMin = mouseStartPostion.y;
                actualSelectionBox.yMax = Utils.GetMouseWorldPostion().y;
            }
        }

        private void SelectObjects()
        {
            List<GameObject> gameObjects = new List<GameObject>();
            gameObjects = Utils.GetAllGameObjects();
            foreach (GameObject gameObject in gameObjects)
            {

                if (actualSelectionBox.xMax > gameObject.transform.position.x && actualSelectionBox.xMin < gameObject.transform.position.x && actualSelectionBox.yMax > gameObject.transform.position.y && actualSelectionBox.yMin < gameObject.transform.position.y && gameObject.name != "BoxSelectorGraphic")
                {
                    selectedObjects.Add(gameObject);
                }
            }
        }

        private void CreateMenuNodes()
        {
            GameObject gameObject = new GameObject();
            Texture2D texture = new Texture2D(10, 10);
            gameObject.name = "Menu Node";
            gameObject.AddComponent<SpriteRenderer>();
            var sprite = Resources.Load<Sprite>("Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/Square.png");
            gameObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(new Texture2D(1024, 256), new Rect(0, 0, 1024, 256), new Vector2(2, 2));
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
            gameObject.AddComponent<SelectMenuNode>();
            Vector3 position = Utils.GetMouseWorldPostion();
            position.x = position.x + 20;
            gameObject.transform.position = position;
            gameObject.layer = 5;
        }

        private void SetSelector()
        {
            switch (actionSelector)
            {
                case ActionSelector.ChopWood:
                    SetWoodToHarvest();
                    break;
                case ActionSelector.Mine:
                SetMineNodeToHarvest();
                break;
            }
        }

        private void SetWoodToHarvest()
        {
            if (selectedObjects.Count > 0)
            {
                List<GameObject> selectedTrees = selectedObjects.FindAll(g => g.GetComponent<Harvestable>() != null && g.GetComponent<Harvestable>().harvestType == Harvestable.HarvestType.tree);
                foreach (GameObject treeObject in selectedTrees)
                {
                    treeObject.GetComponent<Harvestable>().IsMarkedForHarvest = true;
                    selectedObjects.Remove(treeObject);
                }

            }
        }

        private void SetMineNodeToHarvest()
        {
        if (selectedObjects.Count > 0)
        {
            List<GameObject> selectedTrees = selectedObjects.FindAll(g => g.GetComponent<Harvestable>() != null && g.GetComponent<Harvestable>().harvestType == Harvestable.HarvestType.rock);
            foreach (GameObject treeObject in selectedTrees)
            {
                treeObject.GetComponent<Harvestable>().IsMarkedForHarvest = true;
                selectedObjects.Remove(treeObject);
            }

        }
    }

        private void PeformKeyActions()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                this.selectedObjects.Clear();
                if (!actionButtonPressed && actionSelector == ActionSelector.ChopWood)
                {
                    actionButtonPressed = true;
                    actionSelector = ActionSelector.noAction;
                }
                if (!actionButtonPressed)
                {
                    actionButtonPressed = true;
                    actionSelector = ActionSelector.ChopWood;
                }
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                actionButtonPressed = false;
            }
        }

    private void CreateBuildObjectSquare()
    {

        int buildSquaresX = (int)Mathf.Round((endPostion.x / (float).7));
        int buildSquaresY = (int)Mathf.Round((endPostion.y / (float).7));
        int startBuildSquaresX = (int)Mathf.Round((startPostion.x / (float).7));
        int startBuildSquaresY = (int)Mathf.Round((startPostion.y / (float).7));

        GameObject BuildObject = new GameObject();
        BuildObject.AddComponent<SpriteRenderer>();
        BuildObject.AddComponent<BuildObject>();
        BuildObject.GetComponent<BuildObject>().buildType = global::BuildObject.BuildType.BuildObject;
        BuildObject.GetComponent<BuildObject>().objectId = buildObjectId;
        BuildObject.GetComponent<BuildObject>().meterial = this.meterial;
        BuildObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player_Box");
        BuildObject.AddComponent<BoxCollider2D>();
        BuildObject.name = "BuildObject_Template";
        BuildObject.transform.localScale = new Vector3((float)1.1, (float)1.1, 1);

        switch(actionSelector)
        {
            case ActionSelector.BuildObjectBed:
                BuildObject.GetComponent<BuildObject>().recipe.Add(1);
                break;
        }

        if (startBuildSquaresX < buildSquaresX && startBuildSquaresY < buildSquaresY)
        {
            for (int i = startBuildSquaresX; i < buildSquaresX; i++)
            {
                for (int j = startBuildSquaresY; j < buildSquaresY; j++)
                {
                    Vector2 position = new Vector2();
                    if (j == 0 && i == 0)
                    {
                        position = new Vector2((float).34, (float).34);
                    }
                    else
                    {
                        position = new Vector2((float)(.34 + (float).7 * i), (float)(.34 + (float).7 * j));
                    }
                    Debug.Log(position);

                    BuildObject.transform.position = position;

                }
            }
        }
        else if (startBuildSquaresX > buildSquaresX && startBuildSquaresY < buildSquaresY)
        {
            for (int i = startBuildSquaresX; i > buildSquaresX; i--)
            {
                for (int j = startBuildSquaresY; j < buildSquaresY; j++)
                {
                    Vector2 position = new Vector2();
                    if (j == 0 && i == 0)
                    {
                        position = new Vector2((float).34, (float).34);
                    }
                    else
                    {
                        position = new Vector2((float)(.34 + (float).7 * i), (float)(.34 + (float).7 * j));
                    }
                    BuildObject.transform.position = position;
                }
            }
        }
        else if (startBuildSquaresX < buildSquaresX && startBuildSquaresY > buildSquaresY)
        {
            for (int i = startBuildSquaresX; i < buildSquaresX; i++)
            {
                for (int j = startBuildSquaresY; j > buildSquaresY; j--)
                {
                    Vector2 position = new Vector2();
                    if (j == 0 && i == 0)
                    {
                        position = new Vector2((float).34, (float).34);
                    }
                    else
                    {
                        position = new Vector2((float)(.34 + (float).7 * i), (float)(.34 + (float).7 * j));
                    }
                    BuildObject.transform.position = position;

                }
            }
        }
        else if (startBuildSquaresX > buildSquaresX && startBuildSquaresY > buildSquaresY)
        {
            for (int i = startBuildSquaresX; i > buildSquaresX; i--)
            {
                for (int j = startBuildSquaresY; j > buildSquaresY; j--)
                {
                    Vector2 position = new Vector2();
                    if (j == 0 && i == 0)
                    {
                        position = new Vector2((float).34, (float).34);
                    }
                    else
                    {
                        position = new Vector2((float)(.34 + (float).7 * i), (float)(.34 + (float).7 * j));
                    }
                    BuildObject.transform.position = position;
                }
            }
        }
    }


    private void CreateWall()
    {

        int buildSquaresX = (int)Mathf.Round((endPostion.x / (float).7));
        int buildSquaresY = (int)Mathf.Round((endPostion.y / (float).7));
        int startBuildSquaresX = (int)Mathf.Round((startPostion.x / (float).7));
        int startBuildSquaresY = (int)Mathf.Round((startPostion.y / (float).7));


        if (startBuildSquaresX < buildSquaresX && startBuildSquaresY < buildSquaresY)
        {
            for (int i = startBuildSquaresX; i < buildSquaresX; i++)
            {
                for (int j = startBuildSquaresY; j < buildSquaresY; j++)
                {
                    Vector2 position = new Vector2();
                    if (j == 0 && i == 0)
                    {
                        position = new Vector2((float).34, (float).34);
                    }
                    else
                    {
                        position = new Vector2((float)(.34 + (float).7 * i), (float)(.34 + (float).7 * j));
                    }
                    Debug.Log(position);


                    GameObject wall = new GameObject();
                    wall.AddComponent<SpriteRenderer>();
                    wall.AddComponent<BuildObject>();
                    wall.GetComponent<BuildObject>().buildType = 0;
                    wall.GetComponent<BuildObject>().meterial = this.meterial;
                    wall.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player_Box");
                    wall.AddComponent<BoxCollider2D>();
                    wall.name = "Wall_Template";
                    wall.transform.position = position;
                    wall.transform.localScale = new Vector3((float)1.1, (float)1.1, 1);
                }
            }
        } 
        else if(startBuildSquaresX > buildSquaresX && startBuildSquaresY < buildSquaresY)
        {
            for (int i = startBuildSquaresX; i > buildSquaresX; i--)
            {
                for (int j = startBuildSquaresY; j < buildSquaresY; j++)
                {
                    Vector2 position = new Vector2();
                    if (j == 0 && i == 0)
                    {
                        position = new Vector2((float).34, (float).34);
                    }
                    else
                    {
                        position = new Vector2((float)(.34 + (float).7 * i), (float)(.34 + (float).7 * j));
                    }
                    Debug.Log(position);


                    GameObject wall = new GameObject();
                    wall.AddComponent<SpriteRenderer>();
                    wall.AddComponent<BuildObject>();
                    wall.GetComponent<BuildObject>().buildType = 0;
                    wall.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player_Box");
                    wall.AddComponent<BoxCollider2D>();
                    wall.name = "Wall_Template";
                    wall.transform.position = position;
                    wall.transform.localScale = new Vector3((float)1.1, (float)1.1, 1);
                }
            }
        }
        else if (startBuildSquaresX < buildSquaresX && startBuildSquaresY > buildSquaresY)
        {
            for (int i = startBuildSquaresX; i < buildSquaresX; i++)
            {
                for (int j = startBuildSquaresY; j > buildSquaresY; j--)
                {
                    Vector2 position = new Vector2();
                    if (j == 0 && i == 0)
                    {
                        position = new Vector2((float).34, (float).34);
                    }
                    else
                    {
                        position = new Vector2((float)(.34 + (float).7 * i), (float)(.34 + (float).7 * j));
                    }
                    Debug.Log(position);


                    GameObject wall = new GameObject();
                    wall.AddComponent<SpriteRenderer>();
                    wall.AddComponent<BuildObject>();
                    wall.GetComponent<BuildObject>().buildType = 0;
                    wall.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player_Box");
                    wall.AddComponent<BoxCollider2D>();
                    wall.transform.position = position;
                    wall.name = "Wall_Template";
                    wall.transform.localScale = new Vector3((float)1.1, (float)1.1, 1);
                }
            }
        }
        else if (startBuildSquaresX > buildSquaresX && startBuildSquaresY > buildSquaresY)
        {
            for (int i = startBuildSquaresX; i > buildSquaresX; i--)
            {
                for (int j = startBuildSquaresY; j > buildSquaresY; j--)
                {
                    Vector2 position = new Vector2();
                    if (j == 0 && i == 0)
                    {
                        position = new Vector2((float).34, (float).34);
                    }
                    else
                    {
                        position = new Vector2((float)(.34 + (float).7 * i), (float)(.34 + (float).7 * j));
                    }
                    Debug.Log(position);


                    GameObject wall = new GameObject();
                    wall.AddComponent<SpriteRenderer>();
                    wall.AddComponent<BuildObject>();
                    wall.GetComponent<BuildObject>().buildType = 0;
                    wall.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player_Box");
                    wall.AddComponent<BoxCollider2D>();
                    wall.transform.position = position;
                    wall.name = "Wall_Template";
                    wall.transform.localScale = new Vector3((float)1.1, (float)1.1, 1);
                }
            }
        }
    }
}


