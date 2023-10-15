using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class Ticker : MonoBehaviour
{
    public TickerItem tickerItemPrefab;
    [Range(1f, 10f)]
    public float itemDuration = 3.0f;
    public string[] fillerItems;

    private float width;
    private float pixelsPerSecond;
    private TickerItem currentItem;

    // Start is called before the first frame update
    void Start()
    {
        width = GetComponent<RectTransform>().rect.width;
        pixelsPerSecond = width / itemDuration;
        AddTickerItem(fillerItems[0]);
    }

    // Update is called once per frame
    void Update()
    {
     if (currentItem.GetXPosition <= -currentItem.GetWidth)
        {
            AddTickerItem(fillerItems[Random.Range(0, fillerItems.Length)]);
        }
    }

    void AddTickerItem(string message)
    {
        currentItem = Instantiate(tickerItemPrefab, transform);
        currentItem.Initialize(width, pixelsPerSecond, message);
    }
}
