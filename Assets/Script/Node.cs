using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hovercolor;
    public Vector3 positionOffset;

    GameObject turret = null;
    Renderer rend;
    Color startColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseEnter()
    {
        rend.material.color = hovercolor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Cant build here - TPDO display on screenn\n TODO sell\\upgrade menu");
            return;
        }

        GameObject turretToBuild = BuildManager.Instance.GetTorretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }
}