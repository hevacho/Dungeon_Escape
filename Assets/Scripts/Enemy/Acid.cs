using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    public float direction { get;  set; }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * direction * 3 * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable dam = other.GetComponent<IDamageable>();
            if (dam != null)
            {
                dam.Damage();
                Destroy(gameObject);
            }
        }
        
    }
}
