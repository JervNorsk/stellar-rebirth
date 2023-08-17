using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlanetController : MonoBehaviour
{
    private GameObject _surface;

    private Rigidbody _surfaceRigidbody;
    public Rigidbody player;

    public float gravity = -9.81f;

    public KeyCode keyCode = KeyCode.DownArrow;
    public float torque = 0.1f;
    public ForceMode torqueMode = ForceMode.VelocityChange;

    // Start is called before the first frame update
    void Start()
    {
        _surface = GameObject.Find("Surface");
        _surfaceRigidbody = _surface.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            var gravityDirection = (player.transform.position - _surfaceRigidbody.transform.position).normalized;

            // Debug.Log("Gravity: " + gravityDirection * gravity);

            player.AddForce(gravityDirection * gravity, ForceMode.Force);
        }

        if (_surfaceRigidbody != null && Input.GetKey(keyCode))
        // if (_planet != null)
        {
            // _planet.freezeRotation = false;
            _surfaceRigidbody.AddTorque(new Vector3(torque, 0, 0), torqueMode);
        }
        else
        {
            // _planet.freezeRotation = true;
            _surfaceRigidbody.angularVelocity = Vector3.zero;
        }
    }
}
