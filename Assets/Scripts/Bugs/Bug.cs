using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField] GameObject areaObject;
    [SerializeField, Range(1, 3)] public float defaulSpeed = 2f;
    [SerializeField] public Vector2 speedRange = new Vector2(0.5f, 2f);
    [SerializeField] public Vector2 scaleRange = new Vector2(0.4f, 1f);

    private Vector2 targetPlace;
    private Collider2D areaCollider;
    private float dynamicSpeed;

    private void Awake()
    {
        defaulSpeed *= Random.Range(speedRange.x, speedRange.y);
        transform.localScale *= Random.Range(scaleRange.x, scaleRange.y);
        areaObject = GameObject.Find("BugsArea");
        areaCollider = areaObject.GetComponentInParent<Collider2D>();
    }

    void Start()
    {
        targetPlace = GenerateNewTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        Rotating();
    }

    void Moving()
    {
        // Для ситуации, когда обьем перемещения сдвигается
        if (Vector2.Distance(transform.position, areaCollider.bounds.center) > Mathf.Max(areaCollider.bounds.size.x / 2, areaCollider.bounds.size.y / 2) + 1)
            dynamicSpeed = defaulSpeed * 5;
        else
            dynamicSpeed = defaulSpeed;

        // Поиск новой точки остановки
        if (transform.position == new Vector3(targetPlace.x, targetPlace.y, 0))
            targetPlace = GenerateNewTargetPosition();

        // Перемещение к точки остановки
        transform.position = Vector2.MoveTowards(transform.position, targetPlace, dynamicSpeed * Time.deltaTime);
    }

    void Rotating()
    {
        //Ориентация на точку в двумерном простанстве
        Vector2 direction = (targetPlace - (Vector2)transform.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var offset = 90f;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    Vector2 GenerateNewTargetPosition()
    {
        Vector2 rangeX = new(areaCollider.bounds.center.x - areaCollider.bounds.size.x / 2, areaCollider.bounds.center.x + areaCollider.bounds.size.x / 2);
        Vector2 rangeY = new(areaCollider.bounds.center.y - areaCollider.bounds.size.y / 2, areaCollider.bounds.center.y + areaCollider.bounds.size.y / 2);

        return new Vector2(Random.Range(rangeX.x, rangeX.y), Random.Range(rangeY.x, rangeY.y));
    }
}
