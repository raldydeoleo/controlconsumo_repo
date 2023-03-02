using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo
{
    public class MyDbConnection : IDisposable
    {
        public MyDbConnection(SQLiteConnectionWithLock connection)
        {
            LockConnection = connection;
            Connection = new SQLiteAsyncConnection(() => connection);
        }

        public SQLiteConnectionWithLock LockConnection { get; private set; }

        public SQLiteAsyncConnection Connection { get; set; }

        public Boolean Exist { get; set; }

        public void Dispose()
        {
            if (Connection != null)
            {
                Connection = null;
                LockConnection.Close();
                LockConnection.Dispose();
                LockConnection = null;
            }
        }
    }
}
