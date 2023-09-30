using System.Threading;
using System.Windows.Forms;

namespace FluxAPI.Classes
{
    public static class ThreadBox
    {
        public static void MsgThread(string msgBoxContent = "",
            string msgBoxTitle = "",
            MessageBoxButtons boxButtons = MessageBoxButtons.OK,
            MessageBoxIcon boxIcon = MessageBoxIcon.None,
            MessageBoxDefaultButton boxDefaultButton = MessageBoxDefaultButton.Button1,
            MessageBoxOptions boxOptions = MessageBoxOptions.DefaultDesktopOnly)
        {
            var msgBoxThread = new Thread(
                () =>
                {
                    var dialogResult = MessageBox.Show(msgBoxContent, msgBoxTitle, boxButtons, boxIcon,
                        boxDefaultButton, boxOptions);
                }
            );
            msgBoxThread.Start();
            Utility.Cw(msgBoxTitle + ", " + msgBoxContent);
        }
    }
}