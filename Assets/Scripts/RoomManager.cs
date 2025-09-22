using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static int[] doorsPositionNumber = { 0, 0, 0 }; //�e�����̔z�u�ԍ�
    public static int key1PositionNumber; //��1�̔z�u�ԍ�
    public static int[] itemsPositionNumber = { 0, 0, 0, 0, 0 }; //�A�C�e���̔z�u�ԍ�

    public GameObject[] items = new GameObject[5]; //5�̃A�C�e���v���n�u�̓���

    public GameObject room; //�h�A�̃v���n�u
    public MessageData[] messages; //�z�u�����h�A�Ɋ���U��Script

    public GameObject dummyDoor; //�_�~�[�̃h�A�v���n�u
    public GameObject key; //�L�[�̃v���n�u

    public static bool positioned; //����z�u���ς݂��ǂ���
    public static string toRoomNumber = "fromRoom1"; //Player���z�u�����ׂ��ʒu

    GameObject player; //�v���C���[�̏��

    void Awake()
    {
        //�v���C���[���̎擾
        player = GameObject.FindGameObjectWithTag("Player");

        if (!positioned) //�����z�u���܂�
        {
            StartKeysPosition(); //�L�[�̏���z�u
            StartItemsPosition(); //�A�C�e���̏���z�u
            positioned = true; //����z�u�͍ς�
        }
    }

    void StartKeysPosition()
    {
        //�SKey1�̃X�|�b�g�̎擾
        GameObject[] keySpots = GameObject.FindGameObjectsWithTag("KeySpot");

        //�����_���ɔԍ����擾 (�������ȏ� �����������j
        int rand = Random.Range(1, (keySpots.Length + 1));

        //�S�X�|�b�g���`�F�b�N���ɂ���
        foreach (GameObject spots in keySpots)
        {
            //�ЂƂЂƂ�spotNum�̒��g���m�F����rand�Ɠ������`�F�b�N
            if (spots.GetComponent<KeySpot>().spotNum == rand)
            {
                //�L�[1�𐶐�
                Instantiate(key, spots.transform.position, Quaternion.identity);
                //�ǂ̃X�|�b�g�ԍ��ɃL�[��z�u�������L�^
                key1PositionNumber = rand;
            }
        }

        //Key2�����Key3�̑ΏۃX�|�b�g
        GameObject keySpot;
        GameObject obj; //��������Key2�A�����Key3������\��

        //Key2�X�|�b�g�̎擾
        keySpot = GameObject.FindGameObjectWithTag("KeySpot2");
        //Key�̐�����obj�ւ̊i�[
        obj = Instantiate(key, keySpot.transform.position, Quaternion.identity);
        //��������Key�̃^�C�v��key2�ɕύX
        obj.GetComponent<KeyData>().keyType = KeyType.key2;

        //Key3�X�|�b�g�̎擾
        keySpot = GameObject.FindGameObjectWithTag("KeySpot3");
        //Key�̐�����onj�ւ̊i�[
        obj = Instantiate(key, keySpot.transform.position, Quaternion.identity);
        //��������Key�̃^�C�v��key3�ɕύX
        obj.GetComponent<KeyData>().keyType = KeyType.key3;


    }


    void StartItemsPosition()
    {
        //�S���̃A�C�e���X�|�b�g���擾
        GameObject[] itemSpots = GameObject.FindGameObjectsWithTag("ItemSpot");

        for(int i = 0; i < items.Length; i++)
        {
            //�����_���Ȑ����̎擾
            //���������A�C�e������U��ς݂̔ԍ�����������A�����_����������

            //�X�|�b�g�̑S�`�F�b�N�i�����_���l�ƃX�|�b�g�ԍ��̈�v�j
            //��v���Ă���΁A�����ɃA�C�e���𐶐�

            //�ǂ̃X�|�b�g�ԍ����ǂ̃A�C�e���Ɋ���U��ꂢ��̂����L�^

            //���������A�C�e���Ɏ��ʔԍ�������U���Ă���


        }
    }

    void StartDoorsPosition()
    {
        //�S�X�|�b�g�̎擾
        GameObject[] roomSpots = GameObject.FindGameObjectsWithTag("RoomSpot");

        //�o�����(��1�`��3��3�̏o������j�̕������J��Ԃ�
        for (int i = 0; i < doorsPositionNumber.Length; i++)
        {
            int rand; //�����_���Ȑ��̎󂯎M
            bool unique; //�d�����Ă��Ȃ����̃t���O

            do
            {
                unique = true; //���Ȃ���΂��̂܂܃��[�v�𔲂���\��
                rand = Random.Range(1, (roomSpots.Length + 1)); //1�Ԃ���X�|�b�g���̔ԍ��������_���Ŏ擾

                //���łɃ����_���Ɏ擾�����ԍ����ǂ����̃X�|�b�g�Ƃ��Ċ��蓖�Ă��Ă��Ȃ����AdoorsPositionNumber�z��̏󋵂�S�_��
                foreach (int numbers in doorsPositionNumber)
                {
                    //���o�������ƃ����_���ԍ�����v���Ă�����d�����������Ƃ������ƂɂȂ�
                    if (numbers == rand)
                    {
                        unique = false; //�B��̃��j�[�N�Ȃ��̂ł͂Ȃ�
                        break;
                    }
                }
            } while (!unique);

            //�S�X�|�b�g������肵��rand�Ɠ����̃X�|�b�g��T��
            foreach (GameObject spots in roomSpots)
            {
                if (spots.GetComponent<RoomSpot>().spotNum == rand)
                {
                    //���[���𐶐�
                    GameObject obj = Instantiate(
                        room,
                        spots.transform.position,
                        Quaternion.identity
                        );

                    //���ԃX�|�b�g���I�΂ꂽ�̂�static�ϐ��ɋL�����Ă���
                    doorsPositionNumber[i] = rand;
                }
            }
        }
    }


}