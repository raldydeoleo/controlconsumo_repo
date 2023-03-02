using SQLite.Net.Platform.XamarinAndroid;
using SQLite.Net.Interop;

namespace ControlConsumo.Droid.Managers
{
    class DataBase : ISQLitePlatform
    {
        public ISQLiteApi SQLiteApi { get; private set; }
        public IStopwatchFactory StopwatchFactory { get; private set; }
        public IReflectionService ReflectionService { get; private set; }
        public IVolatileService VolatileService { get; private set; }

        public DataBase()
        {
            SQLiteApi = new SQLiteApiAndroid();
            StopwatchFactory = new StopwatchFactoryAndroid();
            ReflectionService = new ReflectionServiceAndroid();
            VolatileService = new VolatileServiceAndroid();
        }
    }
}