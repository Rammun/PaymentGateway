using PaymentGateway.DAL.Interfaces;
using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.DAL.Implementations
{
    public sealed class FakeTransactionRepository : Singleton<FakeTransactionRepository>, ITransactionRepository
    {
        List<Transaction> transactions;
        ReadOnlyCollection<Transaction> transactionReadOnly;

        private FakeTransactionRepository()
        {
            transactions = new List<Transaction>();
            transactionReadOnly = new ReadOnlyCollection<Transaction>(transactions);
        }

        public IEnumerable<Transaction> GetAll()
        {
            return transactionReadOnly;
        }

        public Transaction GetById(int id)
        {
            return transactions.Find(t => t.Id == id);
        }

        public Transaction GetByOrderId(int id)
        {
            return transactions.Find(t => t.Order_id == id);
        }

        public void Add(Transaction transaction)
        {
            if (transaction == null)
                throw new NullReferenceException("Параметр transaction равен null");
            transactions.Add(transaction); ;
        }

        public void Update(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var transaction = transactions.Find(o => o.Id == id);
            if (transaction == null)
                throw new Exception("Попытка удалить несуществующую транзакцию");
            transactions.Remove(transaction);
        }
    }
}
