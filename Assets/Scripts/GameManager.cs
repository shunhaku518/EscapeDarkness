using UnityEngine;

//ゲーム状態を管理する列挙型
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

    public static GameState gameState; //ゲームのステータス
    public static bool[] doorOpenedState; //ドアの開閉状況
    public static int key1;
    public static int key2;
    public static int key3;
    public static bool[] keyssPickedState; //鍵の取得状況

    public static int bill = 10; //お札の残数
    public static bool[] itemsPickedState; //アイテムの取得状況

    static public bool hasSpotLight; //スポットライトを持っているかどうか

    public static int playerHP = 3; //プレイヤーのHP

    void Start()
    {
        //まずはゲームは開始状態にする
        gameState = GameState.playing;
    }

    
}
