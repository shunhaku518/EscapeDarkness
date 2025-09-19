using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public float deleteTime = 5.0f; //è¡ñ≈Ç∑ÇÈÇ‹Ç≈ÇÃéûä‘

    void Start()
    {
        //deleteTimeïbå„Ç…è¡ñ≈
        Destroy(gameObject,deleteTime);
        
    }

    
    
}
