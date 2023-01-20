using UnityEngine;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

	public string id = "";
	new public string name = "";
	public Sprite sprite = null;
	public GameObject preFab = null;
	public int count = -1;

	public Item(GameObject prefab)
    { 
		this.preFab = prefab;
    }

	public Item()
    {

    }
}