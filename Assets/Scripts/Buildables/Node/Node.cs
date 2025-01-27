using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBluePrint turretBluePrint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;
    public Vector3 positionOffset;

    BuildManager buildManager;
    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.Instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());

    }

    void BuildTurret(TurretBluePrint bluePrint)
    {
        if (PlayerStats.money < bluePrint.cost)
        {
            Debug.Log("You're broke af");
            return;
        }

        PlayerStats.money -= bluePrint.cost;

        GameObject _turret = (GameObject)Instantiate(bluePrint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBluePrint = bluePrint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret built.");
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        return;
        
        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    public void SellTurret()
    {
        PlayerStats.money += turretBluePrint.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);
        turretBluePrint = null;

    }

    public void UpgradeTurret()
    {
        if (PlayerStats.money < turretBluePrint.upgradeCost)
        {
            Debug.Log("Cannot Upgrade, Not enough funds");
            return;
        }

        PlayerStats.money -= turretBluePrint.upgradeCost;
        Destroy(turret);

        GameObject _turret = (GameObject)Instantiate(turretBluePrint.upgradePrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;

        Debug.Log("Turret upgraded.");
    }
}
