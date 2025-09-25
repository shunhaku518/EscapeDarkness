using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public float deleteTime = 5.0f; //Á–Å‚·‚é‚Ü‚Å‚ÌŠÔ

    void Start()
    {
        SoundManager.instance.SEPlay(SEType.Barrier); //ƒoƒŠƒA‚ª”­¶‚µ‚½‰¹

        //deleteTime•bŒã‚ÉÁ–Å
        Destroy(gameObject,deleteTime);
        
    }

    
    
}
