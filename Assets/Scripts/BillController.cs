using UnityEngine;

public class BillController : MonoBehaviour
{
    public float deleteTime = 2.0f; //���������܂ł̎���
    public GameObject barrierPrefab; //���ȏ��łƈ��������ɐ�������v���n�u





    void Start()
    {
        //deleteTime�b��Ɂu�o���A�W�J���ď��Łv
        Invoke("FieldExpansion",deleteTime);

    }

   
    //�o���A�W�J�Ǝ��ȏ��ł��s�����\�b�h
    void FieldExpansion()
    {
        Instantiate(barrierPrefab,transform.position,Quaternion.identity); //���D�Ɠ����ꏊ�Ƀo���A����
        Destroy(gameObject); //���D�͏���

    }

    //�G�ƂԂ�������o���A����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            FieldExpansion();
        }
    }

}
