using System.Collections.Generic;

using UnityEngine;

public class StatueSprite : MonoBehaviour
{
    public ConversationUI conversationUI;
    public Sprite portrait;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            conversationUI.OpenWindow(portrait, new List<string>()
            {
               "Bem vinda a Ilha Poti\n\nUm paraíso ecológico que celebra a união entre a natureza e as pessoas a dezenas de gerações!",
               "Aproveite sua visita e não esqueça de esperimentar nosso delicioso Poti ao Pesto!!"
            });
        }
    }
}
