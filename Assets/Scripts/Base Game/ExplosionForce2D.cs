using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForce2D : MonoBehaviour
{
    public static void Explosion2D(float explosionForce, GameObject gameObject, float fieldOFImpact, LayerMask layerToHit)
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(gameObject.transform.position, fieldOFImpact, layerToHit);

        foreach (Collider2D obj in objects)
        {

            Vector2 dir = obj.transform.position - gameObject.transform.position;
            obj.attachedRigidbody.AddForce(dir.normalized * explosionForce, ForceMode2D.Impulse);
            obj.gameObject.layer = 6;
        }
    }
}
