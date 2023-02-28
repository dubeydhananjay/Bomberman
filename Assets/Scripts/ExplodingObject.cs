using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingObject : MonoBehaviour {
    public virtual void SetPos(Vector3 pos) {
        pos.z = -1;
        transform.position = pos;
    }

    public virtual void DestroyAfterAnim() {
        Destroy(gameObject);
    }
}
