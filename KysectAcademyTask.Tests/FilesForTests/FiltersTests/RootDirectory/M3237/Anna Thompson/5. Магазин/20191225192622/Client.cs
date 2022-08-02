using System;
using LABA_5.API.Interfaces;
using LABA_5.Database.API;
using LABA_5.API.Models;
using LABA_5.API;
using System.Collections.Generic;

namespace LABA_5
{
    public class Client
    {
        private IMarket ApiMarket { get; set; }
        public LABA_5.Database.API.Interfaces.IStore dbStore { get; set; }
        private LABA_5.Database.API.Interfaces.IStore dbFileStore { get; set; }

        public Client()
        {
            dbStore = DatabaseFactory.GetStoreInstance();
            dbFileStore = new LABA_5.FileStore.FileDatabase();
            ApiMarket = new Market(dbFileStore);
            ShowMenu();
        }

        private void ShowMenu()
        {
            Console.WriteLine("1. Create store");
            Console.WriteLine("2. Create item");
            Console.WriteLine("3. Add items in store");
            Console.WriteLine("4. Find store with mallest item price");
            Console.WriteLine("5. Find store with mallest items sum price");
            Console.WriteLine("6. Buy items");
            Console.WriteLine("7. Change item price in store");
            Console.WriteLine("8. Convert DB to FileDB");
            Console.WriteLine("9. Convert FileDB to DB");

            var i = Convert.ToInt32(Console.ReadLine());

            switch (i)
            {
                case 1: CreateStore(); break;
                case 2: CreateItem(); break;
                case 3: AddItems(); break;
                case 4: GetStoreWithSmallestPrice(); break;
                case 5: GetStoreWithSmallestSum(); break;
                case 6: BuyItems(); break;
                case 7: ChangeItemPriceInStore(); break;
                case 8: Converter.Converter.ToFileStore(dbFileStore, dbStore); dbFileStore = new LABA_5.FileStore.FileDatabase(); break;
                case 9: Converter.Converter.ToDatabaseStore(dbFileStore, dbStore); dbStore = DatabaseFactory.GetStoreInstance(); break;
            }

            ShowMenu();
        }

        private void CreateStore()
        {
            Console.Write("Store name - ");
            var response = ApiMarket.CreateStore(Console.ReadLine());
        }

        private void CreateItem()
        {
            Console.Write("Item name - ");
            var response = ApiMarket.CreateItem(Console.ReadLine());
        }

        private void AddItems()
        {
            var storeName = SelectStore();
            ApiMarket.AddItemsInStore(storeName, SelectItems());
        }

        private void GetStoreWithSmallestPrice()
        {
            var response = ApiMarket.GetStoreWithSmallestPrice(SelectItem());
            if (response != null)
            {
                Logger.Info(response.ToString());
            }
            else
            {
                Logger.Error("Not found store");
            }
        }

        private void GetStoreWithSmallestSum()
        {
            var response = ApiMarket.GetStoreWithSmallestSum(SelectItems());
            if (response != null)
            {
                Logger.Info(response.ToString());
            }
            else
            {
                Logger.Error("Not found store");
            }
        }

        private void BuyItems()
        {
            var storeName = SelectStore();
            var response = ApiMarket.BuyItemsInStore(storeName, SelectItems());
            if (response.Success)
            {
                Logger.Info("Buy completed!");
            }
            else
            {
                Logger.Error(response.Error);
            }
        }

        private void ChangeItemPriceInStore()
        {
            var storeName = SelectStore();
            var itemName = SelectItem();
            Console.Write("Price - ");
            var price = Convert.ToDecimal(Console.ReadLine());
            ApiMarket.ChangeItemPriceInStore(storeName, itemName, price);
        }

        private string SelectStore()
        {
            Console.WriteLine("Select store:");
            var chose = new Dictionary<int, string>();
            int index = 1;
            foreach (var storeName in ApiMarket.GetStores())
            {
                chose.Add(index, storeName);
                Console.WriteLine($"{index}: {storeName}");
                index++;
            }

            return chose[Convert.ToInt32(Console.ReadLine())];
        }

        private Dictionary<string, int> SelectItems()
        {
            var answer = new Dictionary<string, int>();
            answer.Add(SelectItem(), GetValue());
            while (true)
            {
                Console.Write("Continue select items [y/n]: ");
                if (Console.ReadLine().ToLower() == "y")
                {
                    var nameItem = SelectItem();
                    if (answer.ContainsKey(nameItem))
                        answer[nameItem] += GetValue();
                    else
                        answer.Add(nameItem, GetValue());
                }
                else
                    break;
            }
            return answer;
        }

        private string SelectItem()
        {
            Console.WriteLine("Select item:");
            var chose = new Dictionary<int, string>();
            int index = 1;
            foreach (var itemName in ApiMarket.GetItems())
            {
                chose.Add(index, itemName);
                Console.WriteLine($"{index}: {itemName}");
                index++;
            }

            return chose[Convert.ToInt32(Console.ReadLine())];
        }

        private int GetValue()
        {
            Console.Write("Value - ");
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
