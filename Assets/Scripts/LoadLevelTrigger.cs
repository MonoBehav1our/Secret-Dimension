using UnityEngine;

public class LoadLevelTrigger : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private string message;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
            SplachScreen.Instance.LoadScene(level, message);
    }
}
