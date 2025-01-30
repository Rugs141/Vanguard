using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    public GameObject buildEffect;
    public GameObject sellEffect;
    public GameObject rangeIndicatorPrefab; // Assign this in the inspector

    private GameObject rangeIndicatorInstance;
    private Node selectedNode;
    public NodeUI nodeUI;
    private TurretBluePrint turretToBuild;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("More than one Build Manager in scene!");
            return;
        }
        Instance = this;
    }

    public bool CanBuild
    {
        get { return turretToBuild != null; }
    }

    public bool HasMoney
    {
        get { return PlayerStats.money >= turretToBuild.cost; }
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);

        if (node.turret != null)
        {
            ShowTurretRange(node.turret);
        }
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
        HideTurretRange();
    }

    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        selectedNode = null;

        DeselectNode();

        if (rangeIndicatorInstance == null)
        {
            rangeIndicatorInstance = Instantiate(rangeIndicatorPrefab);
        }
        rangeIndicatorInstance.transform.localScale = new Vector3(turret.prefab.GetComponent<Turret>().range * 2, 0.1f, turret.prefab.GetComponent<Turret>().range * 2);
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }

    private void ShowTurretRange(GameObject turret)
    {
        if (rangeIndicatorInstance == null)
        {
            rangeIndicatorInstance = Instantiate(rangeIndicatorPrefab);
        }

        float range = turret.GetComponent<Turret>().range;
        rangeIndicatorInstance.transform.position = turret.transform.position;
        rangeIndicatorInstance.transform.localScale = new Vector3(range * 2, 0.1f, range * 2);
        rangeIndicatorInstance.SetActive(true);
    }

    private void HideTurretRange()
    {
        if (rangeIndicatorInstance != null)
        {
            rangeIndicatorInstance.SetActive(false);
        }
    }
}
