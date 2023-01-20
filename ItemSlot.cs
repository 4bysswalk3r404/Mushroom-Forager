using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemSlot : MonoBehaviour
{

    public Item item;
    public int count = 0;

    TextMeshProUGUI text;

    SpriteRenderer sprite;

    private void Start()
    {
        text    = transform.GetComponentInChildren<TextMeshProUGUI>();
        sprite  = GetComponent<SpriteRenderer>();
        item    = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (item != null)
        {
            text.text = item.name + count.ToString();
            sprite.sprite = item.sprite;
        }
    }
}
