using UnityEngine;

public class BillData : MonoBehaviour
{
    Rigidbody2D rbody;
    public int itemNum; //�A�C�e���̎��ʔԍ�

    
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>(); //Rigidbody2D�R���|�[�l���g�̎擾
        rbody.bodyType = RigidbodyType2D.Static; //RigidBody�̋�����Î~
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.bill++; //1���₷
            //�Y������擾�t���O��ON
            GameManager.itemsPickedState[itemNum] = true;

            //�A�C�e���擾���o
            //�@�R���C�_�[�𖳌���
            GetComponent<CircleCollider2D>().enabled = false;

            //�ARigidbody2D�̕����iDynamic�ɂ���j
            rbody.bodyType = RigidbodyType2D.Dynamic;

            //�B��ɑł��グ�i�����5�̗́j
            rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

            //�C�������g�𖕏��i0.5�b��j
            Destroy(gameObject,0.5f);
        }
    }
}
