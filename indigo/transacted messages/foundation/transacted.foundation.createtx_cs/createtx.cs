using System;
using System.Transactions;

namespace Microsoft.Samples.Transactions.Quickstarts
{
    /// <summary>
    /// Shows how interop with System.Data.SqlConnection.EnlistDistributedTransaction works
    /// </summary>
    class CreateTx
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            ITransactionManager tm = null;
            ICommittableTransaction tx = null;
            TransactionOptions options = new TransactionOptions();

            try
            {
                switch(args[0])
                {
                    case "1":
                        tx = Transaction.Create();
                        break;
                    case "2":
                        options.IsolationLevel = IsolationLevel.ReadCommitted;
                        options.Timeout = new TimeSpan(0, 0, 30);
                        tx = Transaction.Create(options);
                        break;
                    case "3":
                        tm = TransactionManager.DefaultTransactionManager;
                        tx = tm.CreateTransaction();
                        break;
                    case "4":
                        options.IsolationLevel = IsolationLevel.ReadCommitted;
                        options.Timeout = new TimeSpan(0, 0, 30);
                        tm = TransactionManager.DefaultTransactionManager;
                        tx = tm.CreateTransaction(options);
                        break;
                    default:
                        Console.WriteLine("Usage CreateTx <option [1-4]>");
                        Console.WriteLine("1 - Transaction.Create()");
                        Console.WriteLine("2 - Transaction.Create(...) with options");
                        Console.WriteLine("3 - TransactionManager.CreateTransaction()");
                        Console.WriteLine("4 - TransactionManager.CreateTransaction(...) with options");
                        return;
                }

                // do work

                // transaction information
                //
                Console.WriteLine("Transaction information:");
                Console.WriteLine("ID:             {0}", tx.Identifier);
                Console.WriteLine("status:         {0}", tx.Status);
                Console.WriteLine("isolationlevel: {0}", tx.IsolationLevel);

                // commit or rollback the transaction
                //
                while(true)
                {
                    Console.Write("Commit or Rollback? [C|R] ");
                    ConsoleKeyInfo c = Console.ReadKey();
                    Console.WriteLine();

                    if ((c.KeyChar == 'C') || (c.KeyChar == 'c'))
                    {
                        tx.Commit();
                        break;
                    }
                    else if ((c.KeyChar == 'R') || (c.KeyChar == 'r'))
                    {
                        tx.Rollback();
                        break;
                    }
                }

                Console.WriteLine("Transaction information:");
                Console.WriteLine("status:         {0}", tx.Status);

                tx = null;

            }
            catch(System.Transactions.TransactionException ex)
            {
                if (tx != null) tx.Rollback();
                Console.WriteLine(ex);
            }
            catch(System.Exception ex)
            {
                if (tx != null) tx.Rollback();
                Console.WriteLine(ex);
            }

        }

    }

}
