using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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

    

    /**
     * deactivate component and set bubble to starting position
     */
    private void EndEvent()
    {
        _bubble.SetPosition(_bubbleStartPos);
        gameObject.SetActive(false);
    }

    /**
     * (re)spawn fire
     *
     * @param fireSpot FireSpot to set active
     */
    private void SetFireActive(FireSpot fireSpot)
    {
        fireSpot.gameObject.SetActive(true);
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
