using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectableType type;
    public Sprite itemIcon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            bool added = player.inventory.Add(this);

            if (added)
            {
                Destroy(gameObject);
            }
        }
    }
}

public enum CollectableType
{
    NONE, CARROT_SEED, WHEAT_SEED
}
