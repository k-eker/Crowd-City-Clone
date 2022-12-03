using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    [SerializeField] FloatingJoystick joystick;

    List<PedestrianCharacter> followers = new List<PedestrianCharacter>();
    List<Vector3> followerTargets = new List<Vector3>();

    protected override void Start()
    {
        base.Start();
        SetColor(CharacterColor.Blue);
    }

    protected override void Update()
    {
        base.Update();
        followerTargets = new List<Vector3>(GetPositionListAround(transform.position, 1.5f, followers.Count));
    }

    protected override void Move()
    {
        base.Move();

        if (joystick.Direction != Vector2.zero)
        {
            animator.SetBool("Running", true);
            moveDirection = new Vector3(joystick.Direction.normalized.x, 0, joystick.Direction.normalized.y);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pedestrian"))
        {
            PedestrianCharacter ped = other.GetComponent<PedestrianCharacter>();
            if (ped.IsFollowing) { return; }
            followers.Add(ped);
            ped.FollowerIndex = followers.Count - 1;
            ped.ConvertToPlayer(characterColor);
            ped.movementSpeed = movementSpeed;
        }
    }
    private List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
    {
        List<Vector3> positionList = new List<Vector3>();
        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360f / positionCount);
            Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position = startPosition + dir * distance;
            Debug.Log(position);
            position.y = 0;
            positionList.Add(position);
        }
        return positionList;
    }

    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }
    public Vector3 GetTargetPos(int followerIndex)
    {
        return followerTargets[followerIndex];
    }
}
