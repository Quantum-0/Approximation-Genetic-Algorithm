using System;

namespace GenAlgTest
{
    public class LogEvent : EventArgs
    {
        public string Text;

        public LogEvent(string text)
        {
            Text = text;
        }
    }
}