using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D body;

    public float speed = 75f;
    public float sprintMultiplyer = 1.5f;
    float sprinting = 1;

    List<GameObject> mushrooms;

    public GameObject hotbar;

    public ItemSlot[] inventory;
    int inventorySize = 1;

    //List<> inventory;

    public GameObject mushroomType;
    Animator anm;


    SpriteRenderer sprite;

    int mushroomCount = 0;
    //public Canvas mushroomCounter;
    //public Text mushroomCounter;
    public TextMeshProUGUI mushroomCounter;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        mushrooms = new List<GameObject>(GameObject.FindGameObjectsWithTag("mushroom"));
        //mushroomCounter.text = mushroomCount.ToString();
        anm = GetComponent<Animator>();

        for (int i = 0; i < inventorySize; i++)
        {
            //hotbar.GetComponentsInChildren
            inventory[i] = hotbar.transform.GetChild(i).GetComponent<ItemSlot>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprinting = sprintMultiplyer;
        } 
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprinting = 1;
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            pos.y += Time.deltaTime * speed * sprinting;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.y -= Time.deltaTime * speed * sprinting;
        }
        if (Input.GetKey(KeyCode.D))
        {
            sprite.flipX = false;
            pos.x += Time.deltaTime * speed * sprinting;
        }
        if (Input.GetKey(KeyCode.A))
        {
            sprite.flipX = true;
            pos.x -= Time.deltaTime * speed * sprinting;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("clicked");
            Vector3 myPos = transform.position;
            //Vector3 myPos = Input.mousePosition;
            for (int i = 0; i < mushrooms.Count; i++)
            {
                Vector3 mushroomPos = mushrooms[i].transform.position;
                Vector3 mushroomDim = mushrooms[i].transform.localScale;
                if (mushroomPos.x + mushroomDim.x >= myPos.x && mushroomPos.x - mushroomDim.x <= myPos.x
                    && mushroomPos.y + mushroomDim.y >= myPos.y && mushroomPos.y - mushroomDim.y <= myPos.y)
                {
                    if (addItemToInventory(mushrooms[i].GetComponent<ItemHolder>().item))
                    {
                        Debug.Log(inventory[0].count);

                        updateHotbar(0);
                        Destroy(mushrooms[i]);
                        mushrooms.RemoveAt(i);

                        spawnNewMushroom(1);
                    }
                }
            }
        }

        transform.position = pos;
    }


    void spawnNewMushroom(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float cameraRadX = 140;
            float cameraRadY = 100;

            Vector3 spawnPoint = new Vector3((Random.value * 2 - 1) * cameraRadX + transform.position.x, (Random.value * 2 - 1) * cameraRadY + transform.position.y, 0f);

            GameObject newMushroom = Instantiate(mushroomType, spawnPoint, new Quaternion());
            mushrooms.Add(newMushroom);
        }
    }

    bool addItemToInventory(Item item)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventory[i].item != null && inventory[i].item.id == item.id)
            {
                inventory[i].count++;
                return true;
            }
        }
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventory[i].item == null)
            {
                inventory[i].item = item;
                return true;
            }
        }
        return false;
    }

    void updateHotbar(int i)
    {
    }
}
