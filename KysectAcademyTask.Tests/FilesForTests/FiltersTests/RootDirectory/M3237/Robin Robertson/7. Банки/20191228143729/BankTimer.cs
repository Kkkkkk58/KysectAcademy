using System;
using System.Collections.Generic;
using System.Text;

namespace OOPLab7.Observers
{
    static class BankTimer
    {
        private static List<ISubscriber> _subscribers = new List<ISubscriber>();
        public static int Days { get; private set; } = 1;
        public static void NextDay()
        {
            Days += 1;
            if (Days == 30)
            {
                NextMonth();
            }
            foreach (var subscriber in _subscribers)
            {
                subscriber?.UpdateDay();
            }
        }
        public static void NextMonth()
        {
            Days = 1;
            foreach (var subscriber in _subscribers)
            {
                subscriber?.UpdateMonth();
            }
        }
        public static void AddSubscriber(ISubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

    }
}
