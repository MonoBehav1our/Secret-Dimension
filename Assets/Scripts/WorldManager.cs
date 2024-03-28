using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;

    [Header("Player")]
    [SerializeField] private Player _player;
    [SerializeField] private float _rotateTime;
    private Vector3 _spawnPoint;

    private float _currentRotation = 0;
    private bool rotated; // если повернули относительно стартового положения
    private bool rotating;

    [Header("Colliders")]
    [SerializeField] private bool _canRotate;
    [SerializeField] private Platform[] _colliders;

    private void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);

        _currentRotation = 0;
        rotated = false;
        ChangeColliders();
        SetSpawnPoint();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !rotating && _canRotate)
        {
            _currentRotation -= 90;
            RotateWorld();
        }
        if (Input.GetKeyDown(KeyCode.Q) && !rotating && _canRotate)
        {
            _currentRotation += 90;
            RotateWorld();
        }      
    }

    private void RotateWorld()
    {
        StartCoroutine(PlayerRotate());
        ChangeColliders();
    }

    private IEnumerator PlayerRotate()
    {
        float time = 0;
        float targetRotationY = _currentRotation;
        rotated = !rotated;

        rotating = true;
        Quaternion startRotation = _player.transform.rotation;

        while ( time <= _rotateTime)
        {
            time += Time.deltaTime;
            _player.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(_player.transform.rotation.x, _currentRotation, _player.transform.rotation.z), time / _rotateTime);
            yield return null;
        }
        rotating = false;
        
    }

    private void ChangeColliders()
    {
        Debug.Log(rotated);
        foreach (Platform collider in _colliders) 
        {
            if (rotated)
                collider.Collider.size = new Vector3(1000, collider.ColliderBaseSize.y, collider.ColliderBaseSize.z);
            else 
                collider.Collider.size = new Vector3(collider.ColliderBaseSize.x, collider.ColliderBaseSize.y, 1000);
        }
    }

    public void PlayerEnterOnBox(Transform boxCollider)
    {
        if (!rotated)
            _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y,  boxCollider.position.z);
        else
            _player.transform.position = new Vector3(boxCollider.position.x, _player.transform.position.y, _player.transform.position.z);

    }

    public void RestartPlayer()
    {
        _player.transform.position = _spawnPoint;
    }

    public void SetSpawnPoint()
    {
        _spawnPoint = _player.transform.position;
    }
}
