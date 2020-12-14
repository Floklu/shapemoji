using UnityEngine;
using UnityEngine.Events;

public class ItemDefect : Item
{
    private Bubble _bubble;
    private Vector3 _bubbleStartPos;
    private FireSpot[] _fireSpots;
    private int _numberOfFireSpots;
    private bool _fireActive;
    private float _nextSpawnTime;
    [SerializeField] private float spawnRate;
    
    /**
     * Unity event, which will be invoked on ignition of the harpoon 
     */
    public UnityEvent OnIgnite { get { if (onIgnite == null) onIgnite = new UnityEvent(); return onIgnite; } } 
    [SerializeField] private UnityEvent onIgnite;
    
    /**
     * Unity event, which will be invoked when the fire is extinguished
     */
    public UnityEvent OnExtinguish { get { if (onExtinguish == null) onExtinguish = new UnityEvent(); return onExtinguish; } } 
    [SerializeField] private UnityEvent onExtinguish;

    /**
     * init bubble and DefectItem
     */
    void Start()
    {
        _bubble = GetComponentsInChildren<Bubble>()[0]; //only one exists
        _bubbleStartPos = _bubble.InitBubble();
        _fireSpots = GetComponentsInChildren<FireSpot>();
        _numberOfFireSpots = _fireSpots.Length;
        _nextSpawnTime = Time.time + spawnRate;
        _fireActive = true;
        foreach (var fireSpot in _fireSpots)
        {    
            fireSpot.gameObject.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (_fireActive)
        {
            if (Time.time > _nextSpawnTime)
            {
                int toSpawn = Random.Range(0, _numberOfFireSpots-1);
                SetFireActive(_fireSpots[toSpawn]);
                _nextSpawnTime = Time.time + spawnRate;
            }
        }
        else
        {
            EndEvent();
        }

    }

    // Unity OnEnable Event
    private void OnEnable()
    {
        _fireActive = true;
        if (_fireSpots != null)
            foreach (var fireSpot in _fireSpots)
            {
                fireSpot.gameObject.SetActive(true);
            }
    }

    // Unity On Disable Event
    private void OnDisable()
    {
        _fireActive = false;
    }

    /**
     * deactivate component and set bubble to starting position
     */
    private void EndEvent()
    {
        _bubble.SetPosition(_bubbleStartPos);
        gameObject.SetActive(false);
        OnExtinguish.Invoke();
    }

    /**
     * (re)spawn fire
     *
     * @param fireSpot FireSpot to set active
     */
    private void SetFireActive(FireSpot fireSpot)
    {
        fireSpot.gameObject.SetActive(true);
        OnIgnite.Invoke();
    }

    /**
     * check if all fires are put out (triggered by collision)
     */
    public void CheckFireActive()
    {
        bool isActive = false;
        foreach (var fireSpot in _fireSpots)
        {
            isActive  = isActive || fireSpot.isActiveAndEnabled;
        }
        _fireActive = isActive;
    }
    
}
