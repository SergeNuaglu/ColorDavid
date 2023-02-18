using UnityEngine.Events;

public class FullGiftScreen : Screen
{
    public event UnityAction Closed;

    public override void Close()
    {
        base.Close();
        Closed?.Invoke();
    }
}
