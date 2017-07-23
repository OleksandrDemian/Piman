public class DialogEvent : WaveEvent
{
    private string id;

    public DialogEvent(string id)
    {
        this.id = id;
    }

    public override void TriggerEvent()
    {
        Dialog dialog = XmlReader.GetDialog(id);

        dialog.onDialogEnd = delegate ()
        {
            DialogWindow.Instance.Close();
            WaveManager.Instance.NextEvent();
        };

        DialogWindow.Instance.OpenDialogWindow(dialog);
    }
}