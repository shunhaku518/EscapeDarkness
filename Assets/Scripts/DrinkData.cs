using UnityEngine;

public class DrinkData : MonoBehaviour
{
    Rigidbody2D rbody;
    public int itemNum; //�A�C�e���̎��ʔԍ�

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Player��HP���ő�Ȃ�Ȃɂ����Ȃ�
            if (GameManager.playerHP < 3)
            {
                GameManager.playerHP++;
            }

            //�Y�����鎯�ʔԍ����擾�ς�
            GameManager.itemsPickedState[itemNum] = true;

            //�A�C�e���擾�̉��o
            GetComponent<CircleCollider2D>().enabled = false;
            rbody.bodyType = RigidbodyType2D.Dynamic;
            rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
            Destroy(gameObject, 0.5f);
        }
    }
}