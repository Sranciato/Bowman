using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for moving arrow over the server
public class MoveServerArrow : MonoBehaviour
{
    private float speed, timeStart;
    int damage;
    SpawnArrow spawnArrow;
    public Rigidbody rigidbody;
    private bool hitscan;
    void Start()
    {
        timeStart = Time.time;
    }
    void Update()
    {
        if (Time.time - timeStart > 15)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
    public void UpdateSpeed(float newSpeed, bool _hitscan, GameObject _playerGameObject)
    {
        hitscan = _hitscan;
        rigidbody.AddForce(transform.forward * (newSpeed * 2f), ForceMode.Impulse);
        speed = newSpeed;
    }
}
