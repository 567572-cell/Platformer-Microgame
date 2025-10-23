using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    // Reference to the ArrayListManager
    [SerializeField] private ArrayListManager arrayListManager;

    // Public variable to assign item name in the Inspector
    [SerializeField] public string itemName;

    // Detect collision with a 2D box collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has a specific tag
        if (collision.CompareTag("Player"))
        {
            // Use the reference to ArrayListManager to add the item
            arrayListManager.AddItem(itemName);
            arrayListManager.DisplayItems();

            Destroy(this.gameObject);
        }
    }
}
