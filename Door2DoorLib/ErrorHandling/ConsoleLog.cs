using Door2DoorLib.Interfaces;

namespace Door2DoorLib.ErrorHandling
{
    internal class ConsoleLog : ILog
    {
        #region Fields
        private string _error;
        #endregion

        #region Constructor
        public ConsoleLog(string error)
        {
            _error = error;
        }
        #endregion

        #region Write Log
        public void WriteLog()
        {
            Console.WriteLine(_error);
        }
        #endregion
    }
}
