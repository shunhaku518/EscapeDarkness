using UnityEngine;

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
    public static bool[] doorOpenedState; //�h�A�̊J��
    public static int key1;
    public static int key2;
    public static int key3;
    public static bool[] keyssPickedState; //���̎擾��

    public static int bill = 10; //���D�̎c��
    public static bool[] itemsPickedState; //�A�C�e���̎擾��

    static public bool hasSpotLight; //�X�|�b�g���C�g�������Ă��邩�ǂ���

    public static int playerHP = 3; //�v���C���[��HP

    void Start()
    {
        //�܂��̓Q�[���͊J�n��Ԃɂ���
        gameState = GameState.playing;
    }

    
}
