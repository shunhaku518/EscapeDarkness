using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//�Q�[����Ԃ��Ǘ�����񋓌^
public enum GameState
{
    playing, 
    talk, 
    gameover, 
    gameclear, 
    ending
};

public class GameManager : MonoBehaviour
{

    public static GameState gameState; //�Q�[���̃X�e�[�^�X
    public static bool[] doorsOpenedState = {false,false,false}; //�h�A�̊J��
    public static int key1;
    public static int key2;
    public static int key3;
    public static bool[] keysPickedState = {false,false,false}; //���̎擾��

    public static int bill = 0; //���D�̎c��
    public static bool[] itemsPickedState = {false,false,false,false,false}; //�A�C�e���̎擾��

    static public bool hasSpotLight; //�X�|�b�g���C�g�������Ă��邩�ǂ���

    public static int playerHP = 3; //�v���C���[��HP

    void Start()
    {
        //�܂��̓Q�[���͊J�n��Ԃɂ���
        gameState = GameState.playing;

        //�V�[�����̎擾
        Scene currentScene = SceneManager.GetActiveScene();
        // �V�[���̖��O���擾
        string sceneName = currentScene.name;

        switch (sceneName)
        {
            case "Title":
                SoundManager.instance.PlayBgm(BGMType.Title);
                break;
            case "Boss":
                SoundManager.instance.PlayBgm(BGMType.InBoss);
                break;
            case "Opening":
            case "Ending":
                SoundManager.instance.StopBgm();
                break;
            default:
                SoundManager.instance.PlayBgm(BGMType.InGame);
                break;
        }
    }

    private void Update()
    {
        //�Q�[���I�[�o�[�ɂȂ�����^�C�g���ɖ߂�
        if (gameState == GameState.gameover)
        {
            //���ԍ��ŃV�[���؂�ւ�
            StartCoroutine(TitleBack());

            //Invoke���\�b�h�ł���
        }
    }

    //�Q�[���I�[�o�[�̍ۂɔ�������R���[�`��
    IEnumerator TitleBack()
    {
        yield return new WaitForSeconds(5); //5�b�҂�
        SceneManager.LoadScene("Title"); //�^�C�g���ɖ߂�
    }

}
