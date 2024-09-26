using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemsSpawner : MonoBehaviour
{
    [Header("- - - Параметры спавнера предметов - - -")]
    public ItemsTypes typeSpawnItems;
    [Description("Допустимый интервал движения спавнера в стороны")] public Vector2 rangeSliding = new Vector2(-8f, 8f);
    [Description("Максимальное количество повтрений подряд одного элемента"), Range(1, 10)] public int maxCountRepeat = 1;
    [Description("Задержка между спавном предметов"), Range(0.1f, 3f)] public float spawnDelay;

    private TableItemsMerges TableMerges;
    //private ItemsManager ItemsManager;
    private InputController InputController;

    private int _currentCountRepeat = 0;
    private GameObject _nextSpawnGameObject;
    private bool _canUseDrop = true;

    private LineRenderer _aimLine;


    void Awake()
    {
        TableMerges = Resources.Load("Table Merges") as TableItemsMerges;
        //ItemsManager = FindObjectOfType<ItemsManager>();
        InputController = FindObjectOfType<InputController>();
        _aimLine = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        // Спавн первого элемента сцены
        SpawnNextItem(Random.Range(TableMerges.ItemClass[((int)typeSpawnItems)].indexSmallestSpawnItem, TableMerges.ItemClass[(int)typeSpawnItems].indexBiggestSpawnItem + 1));
    }

    void SpawnNextItem(int index)
    {
        _nextSpawnGameObject = Instantiate(TableMerges.ItemClass[(int)typeSpawnItems].ItemParameters[index].prefab, transform.position, Quaternion.identity);
        _nextSpawnGameObject.transform.SetParent(transform, true);

        // Инициализация необходимых значений
        _nextSpawnGameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        _nextSpawnGameObject.GetComponent<Collider2D>().enabled = false;
        Item item = _nextSpawnGameObject.GetComponent<Item>();
        //prevIndex
        //item.ItemsManager = ItemsManager;
    }

    void DropNextItem()
    {
        if (_canUseDrop)
        {
            _nextSpawnGameObject.transform.parent = null;
            _nextSpawnGameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            _nextSpawnGameObject.GetComponent<Collider2D>().enabled = true;
        }
        StartCoroutine(CoroutineDelaySpawn(spawnDelay));
    }

    IEnumerator CoroutineDelaySpawn(float delay)
    {
        _canUseDrop = false;
        yield return new WaitForSeconds(delay);
        SpawnNextItem(GenerateNextIndex());
        _canUseDrop = true;
    }

    private int GenerateNextIndex()
    {
        int index = Random.Range(TableMerges.ItemClass[((int)typeSpawnItems)].indexSmallestSpawnItem, TableMerges.ItemClass[(int)typeSpawnItems].indexBiggestSpawnItem + 1);

        try
        {
            if (index == _nextSpawnGameObject.GetComponent<Item>().Index)
                _currentCountRepeat++;
            else
                _currentCountRepeat = 0;
        }
        catch
        {
            _currentCountRepeat++;
        }
        

        if (_currentCountRepeat <= maxCountRepeat)
        {
            return index;
        }
        else
        {
            Debug.Log("<color=magenta>Слишком большое количество повторений!</color>");
            int countRecalculation = 0;
            while (_currentCountRepeat > maxCountRepeat && countRecalculation < 100)
            {
                countRecalculation++;
                index = Random.Range(TableMerges.ItemClass[((int)typeSpawnItems)].indexSmallestSpawnItem, TableMerges.ItemClass[(int)typeSpawnItems].indexBiggestSpawnItem + 1);
                if (index == _nextSpawnGameObject.GetComponent<Item>().Index)
                    _currentCountRepeat++;
                else
                    _currentCountRepeat = 0;
                Debug.Log("<color=purple>Рассчет нового, непоторяющегося числа!</color>");
            }
            return index;
        }
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x + InputController.inputMouse.x * Time.deltaTime, rangeSliding.x, rangeSliding.y), transform.position.y, 0);

        if (InputController.drop && _canUseDrop)
        {
            DropNextItem();
        }
        TakeAim(InputController.aim);

        // Временная затычка для приложения
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKey(KeyCode.Tab))
        {
            string scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        }

    }

    void TakeAim(bool state)
    {
        _aimLine.enabled = state;

        if (state)
        {
            //LayerMask.GetMask("Default", "Borders");
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, -transform.up, Mathf.Infinity, LayerMask.GetMask("Default", "Borders"));
            //Debug.DrawRay(transform.position, -transform.up * rayHit.distance, Color.black);

            _aimLine.SetPosition(0, Vector3.zero);
            _aimLine.SetPosition(1, new Vector3(0, -rayHit.distance, 0));
        }
    }
}