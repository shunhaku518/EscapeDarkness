using UnityEngine;

public class SpotLight : MonoBehaviour
{
    public PlayerController playerCnt; //PlayerController�R���|�[�l���g
    public float rotationSpeed = 20.0f; //�X�|�b�g���C�g�̉�]���x

    void LateUpdate()
    {
        //���O�܂ł̃X�|�b�g���C�g�̉�]�l�iZ���̂ݎ擾�j
        //float currentAngle = transform.eulerAngles.z;

        //�v���C���[�̊p�x
        float targetAngle = playerCnt.angleZ;

        //�^�[�Q�b�g�ƂȂ�p�x�𒲐�
        Quaternion targetRotation = Quaternion.Euler(0, 0, (targetAngle - 90));

        //���݂̉�]���i���O�̉�]�j���i�^�[�Q�b�g�̉�]�j�ɂȂ�悤��
        //�Ȃ߂炩�ɕ�Ԃ��� Quaternion.Slerp���\�b�h
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }
}