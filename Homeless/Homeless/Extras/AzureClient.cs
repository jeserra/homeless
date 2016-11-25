using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System.Collections.ObjectModel;
using Homeless.Models;
namespace Homeless.Extras
{
    public class AzureClient
    {
        private IMobileServiceClient _client;
        private IMobileServiceSyncTable<House> _table;

        private const string dbPath = "homesDb";
        private const string serviceUri = "http://homelessapp.azurewebsites.net";

        public AzureClient()
        {
            _client = new MobileServiceClient(serviceUri);

            var store = new MobileServiceSQLiteStore(dbPath);
            store.DefineTable<DTO.House>();
            _client.SyncContext.InitializeAsync(store);

            _table = _client.GetSyncTable<House>();
        }

        public async Task< IEnumerable<House>> GetHouses()
        {
            var empty = new House[0];
            try
            {

                if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                    await SyncAsync();

                return await _table.ToEnumerableAsync();
            }
            catch(Exception ex)
            {
                return empty;
            }
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await _client.SyncContext.PushAsync();
                await _table.PullAsync("allHouses", _table.CreateQuery());
            }
            catch(MobileServicePushFailedException pushEx)
            {
                if (pushEx.PushResult != null)
                    syncErrors = pushEx.PushResult.Errors;
            }
        }

        public async void AddHouse (House house)
        {
            await _table.InsertAsync(house);
        }
    }
}
