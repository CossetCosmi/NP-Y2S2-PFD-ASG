using PFD.Model.Interface;
using PFD.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFD.Repository
{
    internal abstract class Repository<MODEL, ID> : IRepository<MODEL, ID> where MODEL : IModel<ID>
    {
        protected abstract string Table { get; }
        protected abstract string Id { get; }
        protected abstract IRowMapper<MODEL> RowMapper { get; }

        protected SqlConnection Connection { get; } = new SqlConnection(App.CONNECTION_STRING);

        public List<MODEL> FindAll()
        {
            SqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {Table}";

            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<MODEL> modelList = new List<MODEL>();

            while (reader.Read())
                modelList.Add(RowMapper.Convert(reader));

            reader.Close();
            Connection.Close();

            return modelList;
        }

        public MODEL FindById(ID id)
        {
            throw new NotImplementedException();
        }
    }
}
