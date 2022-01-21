using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Common.Abstractions
{
    public abstract class Enumeration: IComparable
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name;

        public override bool Equals(object obj)
        {
            var objValue = obj as Enumeration;

            if (objValue == null)
                return false;

            if (!GetType().Equals(obj.GetType()))
                return false;

            return Id.Equals(objValue.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public int CompareTo(object obj)
        {
            return Id.CompareTo(((Enumeration)obj).Id);
        }

        public static IEnumerable<T> GetAll<T>() where T: Enumeration
        {
            return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();
        }

        public static T FromValue<T>(int value) where T: Enumeration
        {
            var item = GetAll<T>().FirstOrDefault(x => x.Id == value);

            if (item == null)
                throw new InvalidOperationException($"{value} is not a valid value for {typeof(T)}");

            return item;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            var item = GetAll<T>().FirstOrDefault(x => x.Name == displayName);

            if (item == null)
                throw new InvalidOperationException($"{displayName} is not a valid display name for {typeof(T)}");

            return item;
        }
    }
}
