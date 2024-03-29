﻿using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;

namespace Door2DoorLib.Logs
{
    /// <summary>
    /// Object class for the ConsoleLog entity.
    /// </summary>
    internal class ConsoleLog : ILog
    {
        #region Fields
        private string _message;
        private MessageTypes _messageType;
        private DateTime _date;
        #endregion

        #region Constructor
        public ConsoleLog(string message, DateTime date, MessageTypes messageType)
        {
            _message = message;
            _date = date;
            _messageType = messageType;
        }
        #endregion

        #region Write Log
        /// <summary>
        /// Prints log to console
        /// </summary>
        public void WriteLog()
        {
            Console.WriteLine($"{_date.ToString("dd-MM-yyyy hh:mm")} - {_messageType.ToString()} - {_message}");
        }
        #endregion
    }
}
