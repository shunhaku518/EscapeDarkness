using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�v���C���[�̊�b�X�e�[�^�X")]
    public float playerSpeed = 3.0f;

    float axisH;�@//�������̓��͏�
    float axisV;�@//�c�����̓��͏�

    [Header("�v���C���[�̊p�x�v�Z�p")]
    public float angleZ = -90f;

    [Header("�I��/�I�t�̑ΏۃX�|�b�g���C�g")]
    public GameObject spotLight;�@//�Ώۂ̃X�|�b�g���C�g

    bool inDamage; //�_���[�W�����ǂ����̃t���O�Ǘ�

    //�R���|�[�l���g
    Rigidbody2D rbody;
    Animator anime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�R���|�[�l���g�̎擾
        rbody = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();

        //�X�|�b�g���C�g���������Ă���΃X�|�b�g���C�g�\��
        if (GameManager.hasSpotLight)
        {
            spotLight.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���łȂ���Ή������Ȃ�
        if (GameManager.gameState != GameState.playing) return;

        Move(); //�㉺���E�̓��͒l�̎擾
        angleZ = GetAngle(); //���̎��̊p�x��ϐ�angleZ�ɔ��f
        Animation(); //angleZ�𗘗p���ăA�j���[�V����
    }

    private void FixedUpdate()
    {
        //�v���C���łȂ���Ή������Ȃ�
        if(GameManager.gameState !=GameState.playing) return;

        //�_���[�W�t���O�������Ă����
        if(inDamage)
        {
            //�_�ŉ��o
            //Sin���\�b�h�̊p�x���ɃQ�[���J�n����̌o�ߎ��Ԃ�^����
            float val = Mathf.Sin(Time.time * 50);

            if (val > 0)
            {
                //�`��@�\��L��
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                //�`��@�\�𖳌�
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }

            //���͂ɂ��Velocity������Ȃ��悤�ɂ����Ń��^�[��
            return;
        }

        //���͏󋵂ɉ�����Player�𓮂���
        //normalized�i���K���j�Ŏ΂߂̐��l��1�ɒ�������
        rbody.linearVelocity = (new Vector2(axisH,axisV)).normalized * playerSpeed;
    }

    public void Move()
    {
        //axisH��axisV�ɓ��͏󋵂�������
        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");
    }

    public float GetAngle()
    {
        //���ݍ��W�̎擾
        Vector2 fromPos = transform.position;

        //���̏u�Ԃ̃L�[���͒l(axisH,axisV)�ɉ������\�����W�̎擾
        Vector2 toPos = new Vector2(fromPos.x + axisH, fromPos.y + axisV);

        float angle; //return�����l�̏���

        //��������������̓��͂�����΁A�V���Ɋp�x�Z�o
        if (axisH != 0 || axisV !=0)
        {
            float dirX = toPos.x - fromPos.x;
            float dirY = toPos.y - fromPos.y;

            //�������ɍ���Y�A�������ɒ��X��^����Ɗp�x�����W�A���`���ŎZ�o�i�~���̒����ŕ\���j
            float rad = Mathf.Atan2(dirY, dirX);

            //���W�A���l���I�C���[�l(�f�O���[)�ɕϊ�
            angle = rad * Mathf.Rad2Deg;
        }

        //�������͂���Ă��Ȃ���΁A�O�t���[���̊p�x���𐘂��u��
        else
        {
            angle = angleZ;
        }


        return angle;
    }

    void Animation()
    {
        //���炩�̓��͂�����ꍇ
        if(axisH != 0 || axisV != 0)
        {
            //�ЂƂ܂�Run�A�j���𑖂点�܂�
            anime.SetBool("run", true);

            //���p�����߂�@�p�����[�^direction int�^
            //int�^��direction ���F0�@��F1  �E�F2  ���F����ȊO

            if (angleZ > -135f && angleZ < -45f) //������
            {
                anime.SetInteger("direction", 0);
            }
            else if (angleZ >= -45f && angleZ <= 45f) //�E����
            {
                anime.SetInteger("direction", 2);
                transform.localScale = new Vector2(1, 1);
            }
            else if (angleZ > 45f && angleZ < 135f) //�����
            {
                anime.SetInteger("direction", 1);
            }
            else //������
            {
                anime.SetInteger("direction", 3);
                transform.localScale = new Vector2(-1, 1);
            }
        }

        else //�������͂��Ȃ��ꍇ
        {
            anime.SetBool("run", false); //����t���O��OFF
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�Ԃ��������肪Enemy��������
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetDamage(collision.gameObject); //�_���[�W�����̊J�n
        }
    }

    void GetDamage(GameObject enemy)
    {
        //�X�e�[�^�X��playing�łȂ���Ή��������I���
        if(GameManager.gameState != GameState.playing) return;

        GameManager.playerHP--; //�v���C���[HP��1���炷

        if(GameManager.playerHP > 0)
        {
            //�����܂ł̃v���C���[�̓�������������X�g�b�v
            rbody.linearVelocity = Vector2.zero; //new Vector2(0,0)
            //�v���C���[�ƓG�Ƃ̍����擾���A���������߂�
            Vector3 v = (transform.position - enemy.transform.position).normalized;
            //���܂��������ɉ������
            rbody.AddForce(v * 4, ForceMode2D.Impulse);

            //�_�ł��邽�߂̃t���O
            inDamage = true;

            //���ԍ���0.25�b��ɓ_�Ńt���O����
            Invoke("DamageEnd", 0.25f);


        }
        else
        {
            //�cHP���c���Ă��Ȃ���΃Q�[���I�[�o�[
            GameOver();
        }
    }

    void DamageEnd()
    {
        inDamage = false; //�_�Ń_���[�W�t���O������
        gameObject.GetComponent<SpriteRenderer>().enabled = true; //�v���C���[���m���ɕ\��
    }

    void GameOver()
    {
        //�Q�[���̏�Ԃ�ς���
        GameManager.gameState = GameState.gameover;


        //�Q�[���I�[�o�[���o
        GetComponent<CircleCollider2D>().enabled = false; //�����蔻��̖�����
        rbody.linearVelocity = Vector2.zero ; //�������~�߂�
        rbody.gravityScale = 1.0f; //�d�͂̕���
        anime.SetTrigger("dead"); //���S�ɃA�j���N���b�v�̔���
        rbody.AddForce(new Vector2(0,5),ForceMode2D.Impulse); //��ɒ��ˏグ��
        Destroy(gameObject, 1.0f); //1�b��ɑ��݂�����

    }

}



