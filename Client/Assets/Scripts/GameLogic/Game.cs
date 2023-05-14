using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singleton<Game>
{
    public LoginModule LoginModule;
    public PlayerModule PlayerModule;

    public void Init()
    {
        LoginModule.Init();
        PlayerModule.Init();
    }
}