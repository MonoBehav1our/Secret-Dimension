using UnityEngine;

public class SetterSpawnPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>()) WorldManager.Instance.SetSpawnPoint();

    }
}
