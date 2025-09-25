using System.Collections;
using TMPro;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public RoomData roomData; //�e�I�u�W�F�N�g�̎����Ă���X�N���v�g���擾
    MessageData message;//�e�I�u�W�F�N�g������ScriptableObject�����擾

    bool isPlayerInRange; //�v���C���[���̈�ɓ��������ǂ���
    bool isTalk; //�g�[�N���J�n���ꂽ���ǂ���
    GameObject canvas; //�g�[�NUI���܂�Canvas�I�u�W�F�N�g
    GameObject talkPanel; //�ΏۂƂȂ�g�[�NUI�p�l��
    TextMeshProUGUI nameText; //�ΏۂƂȂ�g�[�NUI�p�l���̖��O
    TextMeshProUGUI messageText; //�ΏۂƂȂ�g�[�NUI�p�l���̃��b�Z�[�W

    void Start()
    {
        message = roomData.message; //�g�[�N�f�[�^�͐e�I�u�W�F�N�g�̃X�N���v�g�ɂ���ϐ����Q��

        //�g�[�NUI�I�u�W�F�N�g�Ȃǂ̏��擾
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        talkPanel = canvas.transform.Find("TalkPanel").gameObject;
        nameText = talkPanel.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        messageText = talkPanel.transform.Find("MessageText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //�h�A�̗̈���ɂ��� ���@�g�[�N���łȂ��@���@E�L�[�������ꂽ��
        if (isPlayerInRange && !isTalk && Input.GetKeyDown(KeyCode.E))
        {
            //�g�[�N�̎n�܂�
            StartConversation();
        }
    }

    //�g�[�N�̎n�܂�ƂȂ郁�\�b�h
    void StartConversation()
    {
        isTalk = true; //�g�[�N���t���O��ON
        GameManager.gameState = GameState.talk; //�Q�[���X�e�[�^�X��talk
        talkPanel.SetActive(true); //�g�[�NUI��\��
        nameText.text = message.msgArray[0].name; //�e�I�u�W�F�N�g����擾����message�̔z��̐擪�̖��O��\��
        messageText.text = message.msgArray[0].message; //�e�I�u�W�F�N�g����擾����message�̔z��̐擪�̃��b�Z�[�W��\��
        Time.timeScale = 0; //�Q�[���̐i�s���X�g�b�v
        StartCoroutine(TalkProcess()); //TalkProcess�R���[�`���̔���
    }

    //TalkProcess�R���[�`���̐݌v
    IEnumerator TalkProcess()
    {
        SoundManager.instance.SEPlay(SEType.Door); //�h�A�̉�

        //�t���b�V�����͑j�~�̂��߁A�����������~�߂�
        yield return new WaitForSecondsRealtime(0.1f);

        //E�L�[���������܂�
        while (!Input.GetKeyDown(KeyCode.E))
        {
            yield return null; //E�L�[���������܂ŉ������Ȃ�
        }

        bool nextTalk = false; //�g�[�N������ɓW�J���邩�ǂ���

        switch (roomData.roomName)
        {
            case "fromRoom1":
                if (GameManager.key1 > 0) //�Y�����錮�������Ă�����
                {
                    GameManager.key1--; //���̏���
                    nextTalk = true; //���̃g�[�N�W�J��������
                    GameManager.doorsOpenedState[0] = true; //�L�^�p�̎{���󋵂�true
                }
                break;
            case "fromRoom2":
                if (GameManager.key2 > 0) //�Y�����錮�������Ă�����
                {
                    GameManager.key2--; //���̏���
                    nextTalk = true; //���̃g�[�N�W�J��������
                    GameManager.doorsOpenedState[1] = true; //�L�^�p�̎{���󋵂�true
                }
                break;
            case "fromRoom3":
                if (GameManager.key3 > 0) //�Y�����錮�������Ă�����
                {
                    GameManager.key3--; //���̏���
                    nextTalk = true; //���̃g�[�N�W�J��������
                    GameManager.doorsOpenedState[2] = true; //�L�^�p�̎{���󋵂�true
                }
                break;
        }

        if (nextTalk)
        {
            SoundManager.instance.SEPlay(SEType.DoorOpen); //�h�A���J���鉹

            //�J�������Ƃ����ނ̃��b�Z�[�W��\��
            nameText.text = message.msgArray[1].name;
            messageText.text = message.msgArray[1].message;

            //�t���b�V�����͖h�~
            yield return new WaitForSecondsRealtime(0.1f);

            //E�L�[���������܂ł܂�
            while (!Input.GetKeyDown(KeyCode.E))
            {
                yield return null;
            }

            roomData.openedDoor = true; //�e�̃X�N���v�g�̃h�A�J���t���O��ON
            roomData.DoorOpenCheck(); //�J���t���O�ɉ����ăh�A�̕\��/��\��

        }

        EndConversation(); //�R���[�`�����I�����ăQ�[���i�s��߂����\�b�h
    }

    //��b�C�x���g�̏I���ƃQ�[���i�s�̍ĊJ
    void EndConversation()
    {
        talkPanel.SetActive(false); //�g�[�NUI���\��
        GameManager.gameState = GameState.playing; //�Q�[���X�e�[�^�X��playing
        isTalk = false; //�g�[�N���t���O��OFF
        Time.timeScale = 1.0f; //�Q�[���i�s�����Ƃɖ߂�
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
