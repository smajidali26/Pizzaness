using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.ObjectModel;
using System.Collections;
using System.Reflection;

namespace Core
{
    [Serializable]
    public abstract class BaseEntityController
    {
        public static T FillEntity<T>(IDataReader reader) where T : class, new()
        {
            return FillEntity<T>(reader, true);
        }
        /// <summary>
        /// Generic method to fill an object from IDataReader
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="reader">IDataReader</param>
        /// <returns></returns>
        public static T FillEntity<T>(IDataReader reader, bool closeReader) where T : class, new()
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            ObjectMapperCollection<T> objReader = null;

            T filledObject = null;
            try
            {                
                objReader = new ObjectMapperCollection<T>(reader);

                foreach (T obj in objReader)
                {
                    filledObject = obj;
                }
            }
            finally
            {
                if (reader.IsClosed == false)
                {
                    if (closeReader)
                    {
                        reader.Close();
                    }
                }
            }
            return filledObject;
        }
        
        /// <summary>
        /// Generic method to fill a list from DataReader
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="reader">IDataReader</param>
        /// <returns></returns>
        public static Collection<T> FillEntities<T>(IDataReader reader) where T : class, new()
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            Collection<T> objectList = new Collection<T>();
            try
            {
                using (var objReader = new ObjectMapperCollection<T>(reader))
                {
                    foreach (T obj in objReader)
                    {
                        objectList.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                if (reader.IsClosed == false)
                    reader.Close();
                throw ex;
            }
            finally
            {
                if (reader.IsClosed == false)
                    reader.Close();
            }
            return objectList;
        }
    }


    # region Helper Classes

    [Serializable]
    public sealed class ObjectMapperCollection<T> : IEnumerable<T>, IEnumerable, IDisposable where T : class, new()
    {
        private Enumerator enumerator;
        // IDataReader reader;

        public ObjectMapperCollection(IDataReader reader)
        {
            //this.reader = reader;
            this.enumerator = new Enumerator(reader);
        }

        public IEnumerator<T> GetEnumerator()
        {
            Enumerator e = this.enumerator;

            if (e == null)
            {
                throw new InvalidOperationException("Cannot enumerate more than once");
            }

            this.enumerator = null;
            return e;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        [Serializable]
        class Enumerator : IEnumerator<T>, IEnumerator, IDisposable
        {
            IDataReader reader;

            PropertyInfo[] properties;

            int[] propertyLookup;

            T current;


            internal Enumerator(IDataReader rdr)
            {
                this.reader = rdr;
                this.properties = typeof(T).GetProperties();
            }

            public T Current
            {
                get { return this.current; }
            }

            object IEnumerator.Current
            {
                get { return this.current; }
            }

            public bool MoveNext()
            {
                if (this.reader.Read())
                {
                    if (this.propertyLookup == null)
                    {
                        this.InitPropertyLookup();
                    }
                    T instance = new T();

                    for (int i = 0, n = this.properties.Length; i < n; i++)
                    {
                        int index = this.propertyLookup[i];

                        if (index >= 0)
                        {
                            PropertyInfo pi = this.properties[i];

                            if (pi.CanWrite)
                            {
                                if (this.reader.IsDBNull(index))
                                {
                                    pi.SetValue(instance, null, null);
                                }
                                else
                                {
                                    pi.SetValue(instance, this.reader.GetValue(index), null);
                                }
                            }
                        }
                    }
                    this.current = instance;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
            }

            public void Dispose()
            {
                if (this.reader.IsClosed == false)
                {
                    this.reader.Close();
                }
                this.reader.Dispose();
            }


            private void InitPropertyLookup()
            {
                Dictionary<string, int> map = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                for (int i = 0, n = this.reader.FieldCount; i < n; i++)
                {
                    map.Add(this.reader.GetName(i), i);
                }

                this.propertyLookup = new int[this.properties.Length];

                for (int i = 0, n = this.properties.Length; i < n; i++)
                {
                    int index;
                    if (map.TryGetValue(this.properties[i].Name, out index))
                    {
                        this.propertyLookup[i] = index;
                    }
                    else
                    {
                        this.propertyLookup[i] = -1;
                    }
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (this.enumerator != null)
            {
                this.enumerator.Dispose();
            }
        }

        #endregion
    }

    # endregion Helper Classes
}
