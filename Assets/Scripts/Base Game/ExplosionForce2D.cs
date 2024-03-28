using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExplosionForce2D
{
    public static void Explosion2D(this Rigidbody2D rb, float explosionForce, GameObject gameObject, float fieldOFImpact, LayerMask layerToHit)
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(gameObject.transform.position, fieldOFImpact, layerToHit);

        foreach(Collider2D obj in objects)
        {
            Vector2 dir = obj.transform.position - gameObject.transform.position;
            rb.AddForce(dir.normalized * explosionForce, ForceMode2D.Impulse);
        }
    }
}
