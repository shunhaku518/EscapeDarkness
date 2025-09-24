using System.Collections;
using TMPro;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public RoomData roomData; //親オブジェクトの持っているスクリプトを取得
    MessageData message; //親オブジェクトの持っているScriptableObject情報を取得

    bool isPlayerInRange; //プレイヤーが領域に入ったかどうか
    bool isTalk; //トークが開始されたかどうか
    GameObject canvas; //トークUIを含んだCanvasオブジェクト
    GameObject talkPanel; //対象となるトークUIパネル
    TextMeshProUGUI nameText; //対象となるトークUIパネルの名前
    TextMeshProUGUI messageText; //対象となるトークUIパネルのメッセージ

    void Start()
    {
        message = roomData.message; //トークデータは親オブジェクトのスクリプトにある変数を参照

        canvas = GameObject.FindGameObjectWithTag("Canvas");
        talkPanel = canvas.transform.Find("TalkPanel").gameObject;
        nameText = talkPanel.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        messageText = talkPanel.transform.Find("MessageText").GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
        if (isPlayerInRange && !isTalk && Input.GetKeyDown(KeyCode.E))
        {
            StartConversation();
        }
    }

    void StartConversation()
    {
        isTalk = true;
        GameManager.gameState = GameState.talk;
        talkPanel.SetActive(true);
        nameText.text = message.msgArray[0].name;
        messageText.text = message.msgArray[0].message;
        Time.timeScale = 0;
        StartCoroutine(TalkProcess());
    }

    IEnumerator TalkProcess()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        while(!Input.GetKeyDown(KeyCode.E))
        {
            yield return null;
        }

        bool nextTalk = false;

        switch(roomData.roomName)
        {
            case "fromRoom1":
                if(GameManager.key1 > 0)
                {
                    GameManager.key1--;
                    nextTalk = true;
                    GameManager.doorsOpenedState[0] = true;
                }
                break;
            case "fromRoom2":
                if(GameManager.key2 > 0)
                {
                    GameManager.key2--;
                    nextTalk = true;
                    GameManager.doorsOpenedState[1] = true;
                }
                break;
            case "fromRoom3":
                if(GameManager.key3 > 0)
                {
                    GameManager.key3--;
                    nextTalk = true;
                    GameManager.doorsOpenedState[2] = true;
                }
                break;
        }

        if(nextTalk)
        {
            nameText.text = message.msgArray[1].name;
            messageText.text = message.msgArray[1].message;

            yield return new WaitForSecondsRealtime(0.1f);

            while(!Input.GetKeyDown(KeyCode.E))
            {
                yield return null;
            }

            roomData.openedDoor = true;
            roomData.DoorOpenCheck();

        }

        EndConversation();

    }

    void EndConversation()
    {
        talkPanel.SetActive(false);
        GameManager.gameState = GameState.playing;
        isTalk = false;
        Time.timeScale = 1.0f;

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーが領域に入ったら
        if (collision.gameObject.CompareTag("Player"))
        {
            //フラグがON
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //プレイヤーが領域から出たら
        if (collision.gameObject.CompareTag("Player"))
        {
            //フラグがOFF
            isPlayerInRange = false;
        }
    }
}
