using System.Collections;
using TMPro;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public RoomData roomData; //�e�I�u�W�F�N�g�̎����Ă���X�N���v�g���擾
    MessageData message; //�e�I�u�W�F�N�g�̎����Ă���ScriptableObject�����擾

    bool isPlayerInRange; //�v���C���[���̈�ɓ��������ǂ���
    bool isTalk; //�g�[�N���J�n���ꂽ���ǂ���
    GameObject canvas; //�g�[�NUI���܂�Canvas�I�u�W�F�N�g
    GameObject talkPanel; //�ΏۂƂȂ�g�[�NUI�p�l��
    TextMeshProUGUI nameText; //�ΏۂƂȂ�g�[�NUI�p�l���̖��O
    TextMeshProUGUI messageText; //�ΏۂƂȂ�g�[�NUI�p�l���̃��b�Z�[�W

    void Start()
    {
        message = roomData.message; //�g�[�N�f�[�^�͐e�I�u�W�F�N�g�̃X�N���v�g�ɂ���ϐ����Q��

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
        //�v���C���[���̈�ɓ�������
        if (collision.gameObject.CompareTag("Player"))
        {
            //�t���O��ON
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //�v���C���[���̈悩��o����
        if (collision.gameObject.CompareTag("Player"))
        {
            //�t���O��OFF
            isPlayerInRange = false;
        }
    }
}
