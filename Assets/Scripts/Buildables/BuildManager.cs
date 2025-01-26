using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    public GameObject buildEffect;

    private Node selectedNode;
    public NodeUI nodeUI;

    private TurretBluePrint turretToBuild;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("More then one Build Managers in scene!");
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



    public void BuildTurretOn(Node node)
    {
        if(PlayerStats.money < turretToBuild.cost)
        {
            Debug.Log("You're broke af");
            return;
        }

        PlayerStats.money -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition() , Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret built. Money: " + PlayerStats.money);
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
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        selectedNode = null;

        DeselectNode();
    }
}
