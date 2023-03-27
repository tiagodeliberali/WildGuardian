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
               "Bem vinda a Ilha Poti\n\nUm para�so ecol�gico que celebra a uni�o entre a natureza e as pessoas a dezenas de gera��es!",
               "Aproveite sua visita e n�o esque�a de esperimentar nosso delicioso Poti ao Pesto!!"
            });
        }
    }
}
