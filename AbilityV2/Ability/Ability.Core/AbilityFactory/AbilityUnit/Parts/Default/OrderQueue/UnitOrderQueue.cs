// <copyright file="UnitOrderQueue.cs" company="EnsageSharp">
//    Copyright (c) 2017 Moones.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.Utilities;

    using Ensage;

    internal class UnitOrderQueue : IUnitOrderQueue
    {

        private IUnitOrder processedOrder;

        #region Constructors and Destructors

        public UnitOrderQueue(IAbilityUnit unit)
        {
            this.Unit = unit;
            this.Unit.AddOrderIssuer(this);
        }

        #endregion

        #region Public Properties

        public DataProvider<IUnitOrder> NewOrderQueued { get; } = new DataProvider<IUnitOrder>();

        public Notifier QueueEmpty { get; } = new Notifier();

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Properties

        private IOrderedEnumerable<KeyValuePair<uint, IUnitOrder>> Queue { get; set; }

        private Dictionary<uint, IUnitOrder> OrdersDictionary { get; } = new Dictionary<uint, IUnitOrder>();

        #endregion

        #region Public Methods and Operators


        public void Dispose()
        {
            this.OrdersDictionary.Clear();
            this.Queue = null;
            this.NewOrderQueued.Dispose();
            this.QueueEmpty.Dispose();
        }

        private uint lastId;

        private bool queueEmpty = true;

        private uint count;

        private Sleeper sleeper = new Sleeper();

        public void Update()
        {
            
        }

        public bool Enabled { get; set; }

        public uint Id { get; set; }

        public bool Issue()
        {
            if (this.queueEmpty)
            {
                return false;
            }

            if (this.sleeper.Sleeping)
            {
                return false;
            }

            if (!this.processedOrder.CanExecute())
            {
                if (!this.GetNextOrder(true))
                {
                    return false;
                }
            }

            Console.WriteLine("processing order " + this.processedOrder.OrderType);
            var delay = this.processedOrder.Execute();

            this.sleeper.Sleep(delay + Game.Ping);

            if (this.processedOrder.ExecuteOnce)
            {
                this.GetNextOrder(true);
            }

            return true;
        }

        public bool IssueNow(IUnitOrder order)
        {
            if (this.sleeper.Sleeping)
            {
                return true;
            }

            if (!order.CanExecute())
            {
                return false;
            }

            Console.WriteLine("processing order " + order.OrderType);
            var delay = order.Execute();

            this.sleeper.Sleep(delay + Game.Ping);

            if (order.ExecuteOnce)
            {
                return false;
            }

            return true;
        }

        public bool PreciseIssue()
        {
            return false;
        }

        public void EnqueueOrder(IUnitOrder order)
        {
            this.count++;
            this.lastId++;
            order.Id = this.lastId;
            order.Enqueue();

            if (this.queueEmpty)
            {
                if (this.IssueNow(order))
                {
                    this.processedOrder = order;
                    this.queueEmpty = false;
                    this.NewOrderQueued.Next(order);
                }
            }
            else
            {
                if (this.processedOrder.Priority < order.Priority)
                {
                    this.OrdersDictionary.Add(this.processedOrder.Id, this.processedOrder);
                    this.processedOrder = order;
                    this.NewOrderQueued.Next(order);
                }
                else
                {
                    this.OrdersDictionary.Add(this.lastId, order);
                }
            }
        }

        private IUnitOrder GetHighestPriorityOrder()
        {
            var priority = -1u;
            IUnitOrder order = null;

            foreach (var unitOrder in this.OrdersDictionary)
            {
                if (unitOrder.Value.Priority > priority)
                {
                    order = unitOrder.Value;
                    priority = unitOrder.Value.Priority;
                }
            }

            //if (order != null)
            //{
            //    this.OrdersDictionary.Remove(order.Id);
            //}

            return order;
        }

        public bool GetNextOrder(bool removeProcessed)
        {
            if (removeProcessed)
            {
                this.count--;
                this.processedOrder.Dequeue();
                this.OrdersDictionary.Remove(this.processedOrder.Id);
            }

            if (this.count == 0)
            {
                this.queueEmpty = true;
                this.QueueEmpty.Notify();
                this.processedOrder = null;
                return false;
            }
            else if (this.count == 1)
            {
                this.processedOrder = this.OrdersDictionary.First().Value;
                return true;
            }
            else if (this.count > 0)
            {
                this.processedOrder = this.GetHighestPriorityOrder();
                return true;
            }

            return false;
        }

        public void Initialize()
        {
        }

        public DataProvider<IUnitOrder> StartedExecution { get; } = new DataProvider<IUnitOrder>();

        public bool IsWorking { get; set; }

        #endregion
    }
}