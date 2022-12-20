using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample.Winfrom.Helpers
{
    public class UIHelper
    {
        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="control"></param>
        /// <param name="action"></param>
        public static void Invoke(Control control, Action action)
        {
            Action del = delegate ()
            {
                action();
            };
            control.Invoke(del);
        }

        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="control"></param>
        /// <param name="message"></param>
        public static void ShowMessageBox(Control control, string message)
        {
            Invoke(control, () =>
            {
                Clipboard.SetDataObject(message);
                MessageBox.Show(control, message);
            });
        }

        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="control"></param>
        /// <param name="message"></param>
        /// <param name="messageBoxButtons"></param>
        /// <returns></returns>
        public static DialogResult ShowMessageBox(Control control, string message, MessageBoxButtons messageBoxButtons)
        {
            DialogResult reslt = DialogResult.None;
            UIHelper.Invoke(control, () =>
            {
                Clipboard.SetDataObject(message);
                reslt = MessageBox.Show(control, "确认", message, messageBoxButtons);
            });
            return reslt;
        }
    }
}
