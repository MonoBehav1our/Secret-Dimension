using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Platform : MonoBehaviour
{
    public BoxCollider Collider;
    public Vector3 ColliderBaseSize;

    private void Awake()
    {
        Collider = GetComponent<BoxCollider>();
        ColliderBaseSize = GetComponent<BoxCollider>().size;
    }
}
