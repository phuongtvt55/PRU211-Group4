using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject bowPrefab;
    [SerializeField]
    private Transform launchPoint;
   

    public void FireProjectile()                
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, transform.localScale.x > 0 ? -135f : 135f);
        GameObject projectile = Instantiate(bowPrefab, launchPoint.position, rotation);
        Vector3 origScale = projectile.transform.localScale;    
        projectile.transform.localScale = new Vector3(origScale.x * transform.localScale.x > 0 ? 1: -1,
            origScale.y, origScale.z);
    }

    

}
