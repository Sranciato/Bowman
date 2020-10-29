using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class for moving arrow 
public class MoveArrow : MonoBehaviour
{
    private float speed, timeStart;
    int damage;
    SpawnArrow spawnArrow;
    public Rigidbody rigidbody;
    [HideInInspector]
    public string nameTag;
    private bool hitscan;
    GameObject playerGameObject;
    [SerializeField]
    private LayerMask mask;
    void Start()
    {
        timeStart = Time.time;
    }
    // Checks in front of arrow for a raycast hit
    void Update()
    {
        RaycastHit _hit;
        if (Physics.Raycast(transform.position, transform.forward, out _hit, 2f, mask))
        {
            if (hitscan)
                return;
            bool _headshot = false;
            if (_hit.collider.tag == "Player")
            {
                if (_hit.point.y > _hit.collider.bounds.center.y + .5f)
                {
                    _headshot = true;
                    damage *= 2;
                }
                string playerName = _hit.collider.name;
                playerGameObject.GetComponent<SpawnArrow>().PlayerShot(playerName, damage, nameTag, _headshot);
            }
            Destroy(gameObject);
        }
        if (Time.time - timeStart > 15)
        {
            Destroy(gameObject);
        }
    }
    // called from player object when arrow is shot
    public void UpdateSpeed(float newSpeed, bool _hitscan, GameObject _playerGameObject)
    {
        playerGameObject = _playerGameObject;
        hitscan = _hitscan;
        rigidbody.AddForce(transform.forward * (newSpeed * 2f), ForceMode.Impulse);
        speed = newSpeed;
        // Set damage from 0 - 75
        damage = (int)speed * 75 / 125;
    }
}
