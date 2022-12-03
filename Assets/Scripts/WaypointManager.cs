using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WaypointLane
{
    public Transform[] transforms;
}
public class WaypointManager : MonoBehaviour
{
    [SerializeField] private WaypointLane[] waypoints;
    [SerializeField] private PedestrianCharacter pedestrianPrefab;


    private void Start()
    {
        StartCoroutine(Spawning());
        // for (int i = 0; i < length; i++)
        // {
        //     GetPositionListAround(GameManager.Instance.player.transform.position, 1, 15);
        // }
        
    }

    // private List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
    // {
    //     List<Vector3> positionList = new List<Vector3>();
    //     for (int i = 0; i < positionCount; i++)
    //     {
    //         float angle = i * (360f / positionCount);
    //         //Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
    //         Vector3 position = startPosition + dir * distance;
    //         Debug.Log(position);
    //         position.y = 0;
    //         positionList.Add(position);
    //     }
    //     return positionList;
    // }
    IEnumerator Spawning()
    {
        while (true)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                PedestrianCharacter pedestrian = SpawnPedestrian(waypoints[i].transforms[0].position);
                pedestrian.SetWaypoints(waypoints[i].transforms);

                yield return new WaitForSeconds(Random.Range(0f, 2f));
            }
        }
    }

    private PedestrianCharacter SpawnPedestrian(Vector3 pos)
    {
        return Instantiate(pedestrianPrefab, pos, Quaternion.identity);
    }
}
