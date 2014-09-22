using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.Database
{
    public static class NPartyDbExtensions
    {
        public static int Update<T>(this T entity)
            where T : NPartyDb<T>
        {
            return NPartyDb<T>.Instance.Update(entity);
        }

        public static int Insert<T>(this T entity)
            where T : NPartyDb<T>
        {
            return NPartyDb<T>.Instance.Insert(entity);
        }

        public static int Insert<T>(this IEnumerable<T> entities)
            where T : NPartyDb<T>
        {
            return NPartyDb<T>.Instance.Insert(entities);
        }

        public static int Delete<T>(this T entity)
            where T : NPartyDb<T>
        {
            return NPartyDb<T>.Instance.Delete(entity);
        }

        public static IEnumerable<T[]> Segment<T>(this IEnumerable<T> source, int segmentSize)
        {
            int counter = 0;
            T[] currentArray = new T[segmentSize];
            using (IEnumerator<T> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (counter > 0 && counter % segmentSize == 0)
                    {
                        yield return currentArray;
                        currentArray = new T[segmentSize];
                    }

                    currentArray[counter % segmentSize] = enumerator.Current;

                    counter++;
                }

            }

            yield return currentArray;
        }
    }
}