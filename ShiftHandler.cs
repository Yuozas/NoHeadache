using System.Windows.Forms;

namespace NoHeadache
{
    public static class ShiftHandler
    {
        public static bool TryShift(Keys key)
        {
            switch (key)
            {
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                    KeyPoster.ShiftKey(key);
                    return true;

                default:
                    return false;
            }
        }
    }
}