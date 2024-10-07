using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private GameObject[] deadWalls;
    [SerializeField] private Vector3[] deadWallsPositions = new Vector3[4];
    [SerializeField] private Vector3[] deadWallsScales = new Vector3[4];

    private void Awake()
    {
        deadWalls = new GameObject[4];
        for (int i = 0; i < deadWalls.Length; i++)
        {
            deadWalls[i] = new GameObject();
            deadWalls[i].transform.SetParent(transform, true);
            deadWalls[i].layer = LayerMask.NameToLayer("Borders");
            deadWalls[i].name = "Dead Wall_" + i.ToString();
            deadWalls[i].AddComponent<DeadWall>();
            deadWalls[i].transform.position = deadWallsPositions[i];
            deadWalls[i].transform.localScale = deadWallsScales[i];
        }
    }
}