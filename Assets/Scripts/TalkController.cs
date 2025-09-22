using TMPro;
using UnityEngine;
using System.Collections;

public class TalkController : MonoBehaviour
{
    public MessageData message; //ScriptableObject�ł���N���X
    bool isPlayerInRange; //�v���C���[���̈�ɓ��������ǂ���
    bool isTalk; //�g�[�N���J�n���ꂽ���ǂ���
    GameObject canvas; //�g�[�NUI���܂�Canvas�I�u�W�F�N�g
    GameObject talkPanel; //�ΏۂƂȂ�g�[�NUI�p�l��
    TextMeshProUGUI nameText; //�ΏۂƂȂ�g�[�NUI�p�l���̖��O
    TextMeshProUGUI messageText; //�ΏۂƂȂ�g�[�NUI�p�l���̃��b�Z�[�W

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        talkPanel = canvas.transform.Find("TalkPanel").gameObject;
        nameText = talkPanel.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        messageText = talkPanel.transform.Find("MessageText").GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
        if (isPlayerInRange && !isTalk && Input.GetKeyDown(KeyCode.E))
        {
            StartConversation(); //�g�[�N�J�n
        }
    }

    //�g�[�N���J�n���ăQ�[���X�s�[�h���X�g�b�v�����郁�\�b�h
    void StartConversation()
    {
        isTalk = true; //�g�[�N���t���O�𗧂Ă�
        GameManager.gameState = GameState.talk; //�X�e�[�^�X��talk
        talkPanel.SetActive(true); //�g�[�NUI�p�l����\��
        Time.timeScale = 0; //�Q�[���i�s�X�s�[�h��0

        //TalkProcess�R���[�`���̔���
        StartCoroutine(TalkProcess());
    }

    //TalkProcess�R���[�`���̍쐬
    IEnumerator TalkProcess()
    {
        //�ΏۂƂ���ScriptbleObject(�ϐ�message)�������Ă���z��msgArray�̐������J��Ԃ�
        for (int i = 0; i < message.msgArray.Length; i++)
        {
            nameText.text = message.msgArray[i].name;
            messageText.text = message.msgArray[i].message;

            //yield return new WaitForSeconds(0.1f); //0.1�b�҂�
            yield return new WaitForSecondsRealtime(0.1f); //0.1�b�҂�

            while (!Input.GetKeyDown(KeyCode.E))
            {   //E�L�[���������܂�
                yield return null; //�������Ȃ�
            }
        }

        EndConversation(); //�g�[�N�I���̏���
    }

    void EndConversation()
    {
        talkPanel.SetActive(false); //�p�l�����\��
        GameManager.gameState = GameState.playing; //�Q�[���X�e�[�^�X��playing�ɖ߂�
        isTalk = false; //�g�[�N��������
        Time.timeScale = 1.0f; //�Q�[���X�s�[�h�����Ƃɖ߂�
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
