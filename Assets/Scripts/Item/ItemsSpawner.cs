using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    [Header("- - - Ключевые параметры - - -")]
    [Description("Допустимый интервал движения спавнера в стороны"), SerializeField] private Vector2 rangeSliding = new Vector2(-8f, 8f);
    [Description("Максимальное количество повтрений подряд одного элемента"), Range(1, 10), SerializeField] private int maxCountRepeat = 1;
    [Description("Задержка между спавном предметов"), Range(0.1f, 3f), SerializeField] private float spawnDelay;
    [Description("Отображать прицел сброса?"), SerializeField] private bool _canUseAim = true;

    private ItemsContainer ItemsContainer;
    private InputController InputController;

    private int _currentCountRepeat = 0;
    private int _oldIndex = 0;
    private GameObject _nextSpawnGameObject;
    private bool _canUseDrop = true;

    private LineRenderer _aimLine;

    void Awake()
    {
        InputController = FindObjectOfType<InputController>();
        _aimLine = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        // Спавн первого элемента сцены
        ItemsContainer = GameLevelManager.Instance.ItemsContainer;
        SpawnNextItem(Random.Range(ItemsContainer.indexSmallestSpawnItem, ItemsContainer.indexBiggestSpawnItem + 1));
        TakeAim(false);
    }

    void SpawnNextItem(int index)
    {
        _oldIndex = index;
        _nextSpawnGameObject = Instantiate(ItemsContainer[index].prefab, transform.position, Quaternion.identity);
        _nextSpawnGameObject.transform.SetParent(transform, true);

        // Инициализация необходимых значений
        _nextSpawnGameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        _nextSpawnGameObject.GetComponent<Collider2D>().enabled = false;
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
        int newIndex = Random.Range(ItemsContainer.indexSmallestSpawnItem, ItemsContainer.indexBiggestSpawnItem + 1);
        
        if (newIndex == _oldIndex)
            _currentCountRepeat++;
        else
            _currentCountRepeat = 0;

        if (_currentCountRepeat <= maxCountRepeat)
        {
            return newIndex;
        }
        else
        {
            //Debug.Log("<color=magenta>Слишком большое количество повторений!</color>");
            while (_currentCountRepeat > maxCountRepeat)
            {
                newIndex = Random.Range(ItemsContainer.indexSmallestSpawnItem, ItemsContainer.indexBiggestSpawnItem + 1);
                
                if (newIndex == _oldIndex)
                    _currentCountRepeat++;
                else
                {
                    _currentCountRepeat = 0;
                    break;
                }
                //Debug.Log("<color=purple>Рассчет нового, непоторяющегося числа!</color>");
            }
            return newIndex;
        }
    }

    void Update()
    {
        MovePannel();

        if (InputController.drop && _canUseDrop)
        {
            DropNextItem();
        }

        if (_canUseAim)
        {
            TakeAim(InputController.aim);
        }
    }


    void MovePannel()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x + InputController.inputMouse.x * Time.deltaTime, rangeSliding.x, rangeSliding.y), transform.position.y, 0);
    }

    void TakeAim(bool state)
    {
        _aimLine.enabled = state;

        if (state)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, -transform.up, Mathf.Infinity, LayerMask.GetMask("Default", "Borders"));

            _aimLine.SetPosition(0, Vector3.zero);
            _aimLine.SetPosition(1, new Vector3(0, -rayHit.distance, 0));
        }
    }
}