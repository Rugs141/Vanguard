using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    public TurretBluePrint turret1;
    public TurretBluePrint turret2;

    private void Start()
    {
        buildManager = BuildManager.Instance;
    }
    public void SelectPurchaseStandardTurret()
    {
        Debug.Log("Turret Purchased!");
        buildManager.SelectTurretToBuild(turret1);
    }

    public void SelectPurchaseAnotherTurret()
    {
        Debug.Log("Another Turret Bought!");
        buildManager.SelectTurretToBuild(turret2);
    }
}
