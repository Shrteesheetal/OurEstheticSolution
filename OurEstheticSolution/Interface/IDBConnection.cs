using System.Data;

namespace OurEstheticSolution.Interface
{
    public interface IDBConnection
    {
        IDbConnection Connect();
    }
   
}
