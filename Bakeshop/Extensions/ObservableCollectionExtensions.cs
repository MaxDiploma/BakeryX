using Bakeshop.Common.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Bakeshop.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static ObservableCollection<T> SortObservableCollection<T, X>(this ObservableCollection<T> collection, OrderingTypes type, Func<T, X> predicate)
        {
            var orderedCollection = new List<T>();

            switch (type)
            {
                case OrderingTypes.Ascending:
                    orderedCollection = collection.OrderBy(predicate).ToList();
                    break;

                case OrderingTypes.Descending:
                    orderedCollection = collection.OrderByDescending(predicate).ToList();
                    break;
            }

            var result = new ObservableCollection<T>();

            foreach (var item in orderedCollection)
            {
                result.Add(item);
            }

            return result;
        }
    }
}
