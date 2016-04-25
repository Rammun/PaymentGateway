using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Domain
{
    public class Singleton<T> where T : class
    {
        static readonly Lazy<T> instance = new Lazy<T>(() => (T)typeof(T)
            .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[0], null)
            .Invoke(null));

        public static T Instance
        {
            get
            {
                return Singleton<T>.instance.Value;
            }
        }
    }
}
