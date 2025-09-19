using UnityEngine;

public class DrinkData : MonoBehaviour
{
    Rigidbody2D rbody;
    public int itemNum; //アイテムの識別番号

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //PlayerのHPが最大ならなにもしない
            if (GameManager.playerHP < 3)
            {
                GameManager.playerHP++;
            }

            //該当する識別番号を取得済み
            GameManager.itemsPickedState[itemNum] = true;

            //アイテム取得の演出
            GetComponent<CircleCollider2D>().enabled = false;
            rbody.bodyType = RigidbodyType2D.Dynamic;
            rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
            Destroy(gameObject, 0.5f);
        }
    }
}