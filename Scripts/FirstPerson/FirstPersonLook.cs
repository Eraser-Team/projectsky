﻿using UnityEngine;
using Mirror;

public class FirstPersonLook : NetworkBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;


    void Reset()
    {
		if (isLocalPlayer)
		{
			character = GetComponentInParent<FirstPersonMovement>().transform;
		}
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
		if (isLocalPlayer)
		{
			Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
			Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
			frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
			velocity += frameVelocity;
			velocity.y = Mathf.Clamp(velocity.y, -90, 90);
			transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
			character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
		}
    }
}
