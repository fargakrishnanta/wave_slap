using System;
using UnityEngine;

public class CollisionTrigger2D : MonoBehaviour
{
    public event Action<CollisionTrigger2D, Collision2D> onCollisionEnter2D = delegate { };
    public event Action<CollisionTrigger2D, Collision2D> onCollisionExit2D = delegate { };
    public event Action<CollisionTrigger2D, Collision2D> onCollisionStay2D = delegate { };
    public event Action<CollisionTrigger2D, Collider2D> onTriggerEnter2D = delegate { };
    public event Action<CollisionTrigger2D, Collider2D> onTriggerExit2D = delegate { };
    public event Action<CollisionTrigger2D, Collider2D> onTriggerStay2D = delegate { };


    void OnCollisionEnter2D(Collision2D collision) {
        onCollisionEnter2D(this, collision);
    }

    void OnTriggerEnter2D(Collider2D coll) {
        onTriggerEnter2D(this, coll);
    }

    void OnTriggerExit2D(Collider2D coll) {
        onTriggerExit2D(this, coll);
    }

    void OnTriggerStay2D(Collider2D coll) {
        onTriggerStay2D(this, coll);
    }


}