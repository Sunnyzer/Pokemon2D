using UnityEngine;

public class Grass : TileSprite
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("fouille");
    }
}
