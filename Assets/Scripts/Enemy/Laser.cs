using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Start()
    {
        DisableLaser(); 
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLaser();
        UpdateGunPos();
    }
    protected virtual void EnableLaser()
    {
        line.enabled = true;   
    }
    protected virtual void UpdateLaser()
    {
        RaycastHit2D hit;
        line.SetPosition(0, firePoint.transform.position);
        hit = Physics2D.Raycast(firePoint.position, transform.TransformDirection(Vector2.right));
        if (!hit)
        {
            DisableLaser();
            return;
        }
        EnableLaser();
        line.SetPosition(1, hit.point);
    }
    protected virtual void DisableLaser()
    {
        line.enabled = false;
    }
    protected virtual void UpdateGunPos()
    {
        transform.Translate(Vector2.down * 2.5f * Time.deltaTime);

        //Vector2 direction = target.position;
        //direction.Normalize();
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        //transform.rotation = Quaternion.Euler(0, 180, angle);
    }
}
