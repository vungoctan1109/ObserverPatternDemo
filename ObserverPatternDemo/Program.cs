﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace ObserverPatternDemo
{
    internal class Program
    {
        public interface IObserver
        {
            void Update(ISubject subject);
        }

        public interface ISubject
        {
            void Attach(IObserver observer);

            void Detach(IObserver observer);

            void Notify();
        }

        public class Subject : ISubject
        {
            public int State { get; set; } = -0;

            private List<IObserver> _observers = new List<IObserver>();

            public void Attach(IObserver observer)
            {
                Console.WriteLine("Subject: Attached an observer.");
                this._observers.Add(observer);
            }

            public void Detach(IObserver observer)
            {
                this._observers.Remove(observer);
                Console.WriteLine("Subject: Detached an observer.");
            }

            // Trigger an update in each subscriber.
            public void Notify()
            {
                Console.WriteLine("Subject: Notifying observers...");

                foreach (var observer in _observers)
                {
                    observer.Update(this);
                }
            }

            public void SomeBusinessLogic()
            {
                Console.WriteLine("\nSubject: I'm doing something important.");
                this.State = new Random().Next(0, 10);

                Thread.Sleep(1500);

                Console.WriteLine("Subject: My state has just changed to: " + this.State);
                this.Notify();
            }
        }

        private class ConcreteObserverA : IObserver
        {
            public void Update(ISubject subject)
            {
                if ((subject as Subject).State < 3)
                {
                    Console.WriteLine("ConcreteObserverA: Reacted to the event.");
                }
            }
        }

        private class ConcreteObserverB : IObserver
        {
            public void Update(ISubject subject)
            {
                if ((subject as Subject).State == 0 || (subject as Subject).State >= 2)
                {
                    Console.WriteLine("ConcreteObserverB: Reacted to the event.");
                }
            }
        }

        private static void Main(string[] args)
        {
            var subject = new Subject();
            var observerA = new ConcreteObserverA();
            subject.Attach(observerA);

            var observerB = new ConcreteObserverB();
            subject.Attach(observerB);

            subject.SomeBusinessLogic();
            subject.SomeBusinessLogic();

            subject.Detach(observerB);

            subject.SomeBusinessLogic();
        }
    }
}