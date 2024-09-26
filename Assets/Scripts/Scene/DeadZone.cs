using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private GameObject[] deadWalls = new GameObject[4];
    [SerializeField] private Vector3[] deadWallsPositions = new Vector3[4];
    [SerializeField] private Vector3[] deadWallsScales = new Vector3[4];

    private void Awake()
    {
        for (int i = 0; i < deadWalls.Length; i++)
        {
            deadWalls[i] = Instantiate(new GameObject(), transform.position, Quaternion.identity);
            deadWalls[i].transform.SetParent(transform, true);
            deadWalls[i].layer = LayerMask.NameToLayer("Borders");
            deadWalls[i].name = "Dead Wall_" + i.ToString();
            deadWalls[i].AddComponent<DeadWall>();
            deadWalls[i].transform.position = deadWallsPositions[i];
            deadWalls[i].transform.localScale = deadWallsScales[i];
        }
    }
}