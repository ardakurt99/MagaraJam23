using UnityEngine;

public class PlayerControl : CharacterControl
{
    void Start()
    {
        Speed = 10;
        RotationSpeed = 15;
    }

    private void Update()
    {
        transform.Translate(new Vector3(0,0, Input.GetAxis("Vertical")) * Speed * Time.deltaTime);

        transform.Rotate(new Vector3(transform.rotation.x, Input.GetAxis("Horizontal"), transform.rotation.z) * RotationSpeed * Time.deltaTime);
    }
}
