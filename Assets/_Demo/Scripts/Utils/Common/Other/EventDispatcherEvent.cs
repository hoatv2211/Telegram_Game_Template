using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Common;

public class EventFinishTransaction : BaseEvent
{
    public bool isEnable;
    public bool isRunLoading;
    public string message;
    public bool disable;
}

public class EventReloadListNFT : BaseEvent
{
}

public class EventReloadNFT : BaseEvent
{

}

public class EventTokenChanged: BaseEvent
{
}

public class EventSignedFinish : BaseEvent
{
}


public class SetPositionWave: BaseEvent
{
    public List<Transform> trans;
}