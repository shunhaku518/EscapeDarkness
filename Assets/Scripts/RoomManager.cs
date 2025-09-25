using System.Xml;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static int[] doorsPositionNumber = { 0, 0, 0 }; //�e�����̔z�u�ԍ�
    public static int key1PositionNumber; //��1�̔z�u�ԍ�
    public static int[] itemsPositionNumber = { 0, 0, 0, 0, 0 }; //�A�C�e���̔z�u�ԍ�

    public GameObject[] items = new GameObject[5]; //5�̃A�C�e���v���n�u�̓���

    public GameObject room; //�h�A�̃v���n�u
    public MessageData[] messages; //�z�u�����h�A�Ɋ���U��ScriptableObject

    public GameObject dummyDoor; //�_�~�[�̃h�A�v���n�u
    public GameObject key; //�L�[�̃v���n�u

    public static bool positioned; //����z�u���ς��ǂ���
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
            StartDoorsPosition(); //�h�A�̏���z�u
            positioned = true; //����z�u�͍ς�
        }
        else //�����z�u�ς݂������ꍇ�͔z�u���Č�
        {
            LoadKeysPosition(); //�L�[�̔z�u�̍Č�
            LoadItemsPosition(); //�A�C�e���̔z�u�̍Č�
            LoadDoorsPosition(); //�h�A�̔z�u�̍Č�

            PlayerPosition(); //�v���C���[�̔z�u
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
        //Key�̐�����onj�ւ̊i�[
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

        for (int i = 0; i < items.Length; i++)
        {
            //�����_���Ȑ����̎擾
            //���������A�C�e������U��ς݂̔ԍ�����������A�����_����������
            int rand; //�����_���Ȑ��̎󂯎M
            bool unique; //�d�����Ă��Ȃ����̃t���O

            do
            {
                unique = true; //���Ȃ���΂��̂܂܃��[�v�𔲂���\��
                rand = Random.Range(1, (itemSpots.Length + 1)); //1�Ԃ���X�|�b�g���̔ԍ��������_���Ŏ擾

                //���łɃ����_���Ɏ擾�����ԍ����ǂ����̃X�|�b�g�Ƃ��Ċ��蓖�Ă��Ă��Ȃ����AdoorsPositionNumber�z��̏󋵂�S�_��
                foreach (int numbers in itemsPositionNumber)
                {
                    //���o�������ƃ����_���ԍ�����v���Ă�����d�����������Ƃ������ƂɂȂ�
                    if (numbers == rand)
                    {
                        unique = false; //�B��̃��j�[�N�Ȃ��̂ł͂Ȃ�
                        break;
                    }
                }
            } while (!unique);

            //�X�|�b�g�̑S�`�F�b�N�i�����_���l�ƃX�|�b�g�ԍ��̈�v�j
            //��v���Ă���΁A�����ɃA�C�e���𐶐�
            foreach (GameObject spots in itemSpots)
            {
                if (spots.GetComponent<ItemSpot>().spotNum == rand)
                {
                    GameObject obj = Instantiate(
                        items[i],
                        spots.transform.position,
                        Quaternion.identity
                        );

                    //�ǂ̃X�|�b�g�ԍ����ǂ̃A�C�e���Ɋ��蓖�Ă��Ă���̂����L�^
                    itemsPositionNumber[i] = rand;

                    //���������A�C�e���Ɏ��ʔԍ�������U���Ă���
                    if (obj.CompareTag("Bill"))
                    {
                        obj.GetComponent<BillData>().itemNum = i;
                    }
                    else
                    {
                        obj.GetComponent<DrinkData>().itemNum = i;
                    }
                }
            }

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

                    //���������h�A�̃Z�b�e�B���O
                    DoorSetting(
                        obj, //�ΏۃI�u�W�F�N�g
                        "fromRoom" + (i + 1), //���������h�A�̎��ʖ�
                        "Room" + (i + 1), //�����̏o������ɐG�ꂽ�Ƃ��ǂ��ɍs���̂�
                        "Main", //�s����ƂȂ�V�[����
                        false, //�h�A�̊J���̏�
                        DoorDirection.down, //���̏o������ɖ߂������̃v���C���[�̔z�u
                        messages[i]
                        );
                }
            }
        }

        //�_�~�[���̐���
        foreach(GameObject spots in roomSpots)
        {
            //���łɔz�u�ς݂��ǂ���
            bool match = false;

            foreach(int doorsNum in doorsPositionNumber)
            {
                if(spots.GetComponent<RoomSpot>().spotNum == doorsNum)
                {
                    match = true; //���̃X�|�b�g�ԍ��ɂ͂��łɔz�u�ς�
                    break;
                }
            }

            //�������}�b�`���Ă��Ȃ���΂���܂ŉ����z�u����Ă��Ȃ��Ƃ������ƂȂ̂Ń_�~�[�h�A��ݒu
            if(!match) Instantiate(dummyDoor,spots.transform.position, Quaternion.identity);
        }

    }

    //���������h�A�̃Z�b�e�B���O
    void DoorSetting(GameObject obj, string roomName, string nextRoomName, string sceneName, bool openedDoor, DoorDirection direction, MessageData message)
    {
        RoomData roomData = obj.GetComponent<RoomData>();
        //�������Ɏw�肵���I�u�W�F�N�g��RoomData�X�N���v�g�̊e�ϐ���
        //�������ȍ~�Ŏw�肵���l����
        roomData.roomName = roomName;
        roomData.nextRoomName = nextRoomName;
        roomData.nextScene = sceneName;
        roomData.openedDoor = openedDoor;
        roomData.direction = direction;
        roomData.message = message;

        roomData.DoorOpenCheck(); //�h�A�̊J�󋵃t���O���݂ăh�A��\��/��\�����\�b�h
    }

    void LoadKeysPosition()
    {
        //Key1�����擾��������
        if (!GameManager.keysPickedState[0])
        {
            //�SKey1�X�|�b�g�̎擾
            GameObject[] keySpots = GameObject.FindGameObjectsWithTag("KeySpot");

            //�S�X�|�b�g�����Ԃɓ_��
            foreach (GameObject spots in keySpots)
            {
                //�L�^���Ă���X�|�b�gNO�ƈꏏ���ǂ���
                if (spots.GetComponent<KeySpot>().spotNum == key1PositionNumber)
                {
                    //Key1�̐���
                    Instantiate(
                        key,
                        spots.transform.position,
                        Quaternion.identity
                        );
                }
            }
        }

        //Key2�����擾��������
        if (!GameManager.keysPickedState[1])
        {
            //Key2�X�|�b�g�̎擾
            GameObject keySpot2 = GameObject.FindGameObjectWithTag("KeySpot2");
            //Key�̐���
            GameObject obj = Instantiate(
                key,
                keySpot2.transform.position,
                Quaternion.identity
                );
            //��������Key�̃^�C�v��ς��Ă���
            obj.GetComponent<KeyData>().keyType = KeyType.key2;
        }

        //Key3�����擾��������
        if (!GameManager.keysPickedState[2])
        {
            //Key3�X�|�b�g�̎擾
            GameObject keySpot3 = GameObject.FindGameObjectWithTag("KeySpot3");
            //Key�̐���
            GameObject obj = Instantiate(
                key,
                keySpot3.transform.position,
                Quaternion.identity
                );
            //��������Key�̃^�C�v��ς��Ă���
            obj.GetComponent<KeyData>().keyType = KeyType.key3;
        }

    }

    void LoadItemsPosition()
    {
        //�S���̃A�C�e���X�|�b�g���擾
        GameObject[] itemSpots = GameObject.FindGameObjectsWithTag("ItemSpot");

        for (int i = 0; i < items.Length; i++)
        {
            if (!GameManager.itemsPickedState[i])
            {
                //�X�|�b�g�̑S�`�F�b�N�i�����_���l�ƃX�|�b�g�ԍ��̈�v�j
                //��v���Ă���΁A�����ɃA�C�e���𐶐�
                foreach (GameObject spots in itemSpots)
                {
                    if (spots.GetComponent<ItemSpot>().spotNum == itemsPositionNumber[i])
                    {
                        GameObject obj = Instantiate(
                            items[i],
                            spots.transform.position,
                            Quaternion.identity
                            );

                        //���������A�C�e���Ɏ��ʔԍ�������U���Ă���
                        if (obj.CompareTag("Bill"))
                        {
                            obj.GetComponent<BillData>().itemNum = i;
                        }
                        else
                        {
                            obj.GetComponent<DrinkData>().itemNum = i;
                        }
                    }
                }
            }

        }
    }

    void LoadDoorsPosition()
    {
        //�S�X�|�b�g�̎擾
        GameObject[] roomSpots = GameObject.FindGameObjectsWithTag("RoomSpot");

        //�o�����(��1�`��3��3�̏o������j�̕������J��Ԃ�
        for (int i = 0; i < doorsPositionNumber.Length; i++)
        {
            
            //�S�X�|�b�g������肵��rand�Ɠ����̃X�|�b�g��T��
            foreach (GameObject spots in roomSpots)
            {
                if (spots.GetComponent<RoomSpot>().spotNum == doorsPositionNumber[i])
                {
                    //���[���𐶐�
                    GameObject obj = Instantiate(
                        room,
                        spots.transform.position,
                        Quaternion.identity
                        );

                    //���������h�A�̃Z�b�e�B���O
                    DoorSetting(
                        obj, //�ΏۃI�u�W�F�N�g
                        "fromRoom" + (i + 1), //���������h�A�̎��ʖ�
                        "Room" + (i + 1), //�����̏o������ɐG�ꂽ�Ƃ��ǂ��ɍs���̂�
                        "Main", //�s����ƂȂ�V�[����
                        GameManager.doorsOpenedState[i], //�h�A�̊J���̏󋵂�static����ǂݎ��
                        DoorDirection.down, //���̏o������ɖ߂������̃v���C���[�̔z�u
                        messages[i]
                        );
                }
            }
        }

        //�_�~�[���̐���
        foreach (GameObject spots in roomSpots)
        {
            //���łɔz�u�ς݂��ǂ���
            bool match = false;

            foreach (int doorsNum in doorsPositionNumber)
            {
                if (spots.GetComponent<RoomSpot>().spotNum == doorsNum)
                {
                    match = true; //���̃X�|�b�g�ԍ��ɂ͂��łɔz�u�ς�
                    break;
                }
            }

            //�������}�b�`���Ă��Ȃ���΂���܂ŉ����z�u����Ă��Ȃ��Ƃ������ƂȂ̂Ń_�~�[�h�A��ݒu
            if (!match) Instantiate(dummyDoor, spots.transform.position, Quaternion.identity);
        }
    }

    //Player�̔z�u
    void PlayerPosition()
    {
        //�SRoom�I�u�W�F�N�g�̎擾
        GameObject[] roomDatas = GameObject.FindGameObjectsWithTag("Room");

        foreach (GameObject room in roomDatas)
        {
            //���ꂼ���Room��RoomData�X�N���v�g�̏���ϐ�r�ɑ��
            RoomData r = room.GetComponent<RoomData>();

            //�擾���Ă���Room�̎��ʖ����u���ڕW�ɂ��Ă���s��v�̎��ʖ�(static�ϐ�)�Ɠ����Ȃ�
            if (r.roomName == toRoomNumber)
            {
                float posY = 1.5f; //�ŏ��͑ΏۂƂȂ�Room�̏���W
                if (r.direction == DoorDirection.down)
                {
                    posY = -1.5f; //����direction��down�ݒ��Room�Ȃ�v���C���[�̈ʒu�͉����ɂȂ�
                }

                //�v���C���[�̈ʒu�����߂�
                player.transform.position = new Vector2(
                    room.transform.position.x,
                    room.transform.position.y + posY
                    );
                break; //�ړI��Room���������āA�`�F�b�N�̕K�v�����Ȃ��Ȃ����̂�foreach�𒆒f
            }
        }
    }

}
