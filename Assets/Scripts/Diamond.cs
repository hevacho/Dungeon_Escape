using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{

    [SerializeField]
    private int gems=1;

    public int Gems
    {
        get { return gems; }
        set { gems = value; }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.addDiamond(gems);
                Destroy(gameObject);
            }
            
        }
        
    }
}
