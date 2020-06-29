
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject _acid;

    public void Fire()
    {
        float x = transform.parent.localScale.x;
        GameObject go = Instantiate(_acid, transform.position, Quaternion.identity);
        Acid ac = go.GetComponent<Acid>();
        ac.direction = x;
        
    }
}
