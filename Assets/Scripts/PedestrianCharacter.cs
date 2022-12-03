using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianCharacter : Character
{
    Transform[] waypoints;
    int nextWaypointIndex = 0;
    public bool IsFollowing { get; private set; }
    public int FollowerIndex { get; set; }

    protected override void Start()
    {
        base.Start();
        SetColor(CharacterColor.White);
        animator.SetBool("Walking", true);
    }
    public void SetWaypoints(Transform[] _waypoints)
    {
        waypoints = _waypoints;
        SetNextWaypoint();
    }

    private void SetNextWaypoint()
    {
        nextWaypointIndex++;

        if (nextWaypointIndex >= waypoints.Length)
        {
            Destroy(gameObject);
            return;
        }

        moveDirection = TargetPosToDir(waypoints[nextWaypointIndex].position);
    }

    private Vector3 TargetPosToDir(Vector3 targetPos)
    {
        return (targetPos - transform.position).normalized;
    }

    protected override void Move()
    {
        base.Move();

        if (!IsFollowing)
        {
            float remainingDist = Vector3.Distance(transform.position, waypoints[nextWaypointIndex].position);
            if (remainingDist < 0.1f)
            {
                SetNextWaypoint();
            }
        }
        else
        {
            Vector3 targetPos = GameManager.Instance.player.GetTargetPos(FollowerIndex);
            Vector3 targetDir = (targetPos - transform.position).normalized;

            //Vector3 cross = Vector3.Cross(targetDir, GameManager.Instance.player.moveDirection);
            moveDirection = (targetDir + GameManager.Instance.player.moveDirection).normalized;
            Debug.Log(targetDir);
        }
    }

    public void ConvertToPlayer(CharacterColor color)
    {
        animator.SetBool("Walking", false);
        animator.SetBool("Running", true);
        SetColor(color);
        IsFollowing = true;
    }
    
}
