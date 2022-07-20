using SelfTraining.DataContext;
using SelfTraining.Models;
using Dapper;
using SelfTraining.DTO;
using System.Data;

namespace SelfTraining.Repositries
{
    public class IzharRepo : IIzharRepo
    {
        private readonly DapperContext _context;
        public IzharRepo(DapperContext context)
        {
            _context = context;
        }

        public async Task<Izhar> CreateFamily(CreateFamilyDTO createFamily)
        {
            var query = "insert into Family (Name,Email,Contact,Address) values (@Name,@Email,@Contact,@Address)"+"Select Cast(Scope_Identity()as int)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", createFamily.Name,DbType.String);
            parameters.Add("Email", createFamily.Email, DbType.String);
            parameters.Add("Contact", createFamily.Contact, DbType.String);
            parameters.Add("Address",createFamily.Address, DbType.String);
            using(var connection = _context.CreateConnection())
            {
                var id=await connection.QuerySingleAsync<int>(query,parameters);
                var createdFamily = new Izhar
                {
                    Id = id,
                    Name = createFamily.Name,
                    Email = createFamily.Email,
                    Contact = createFamily.Contact,
                    Address = createFamily.Address,
                };
                return createdFamily;
            }
        }

        public async Task DeleteFamily(int id)
        {
            var query = "Delete From Family Where Id=@Id";
            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query,new { id });
            }    
        }

        public async Task<Izhar> GetFamilyById(int id)
        {
            var query = "Select * from Family where Id=@Id";
            using (var connection=_context.CreateConnection())
            {
                var family=await connection.QuerySingleOrDefaultAsync<Izhar>(query,new { id });
                return family;
            }
        }

        public async Task<IEnumerable<Izhar>> GetAllFamily()
        {
            var query = "Select * from Family";
            using(var connection=_context.CreateConnection())
            {
                var family=await connection.QueryAsync<Izhar>(query);
                return family.ToList();
            }
        }

        public async Task UpdateFamily(int id,UpdateFamilyDTO updateFamily)
        {
            var query = "Update Family Set Name=@Name,Email=@Email,Contact=@Contact,Address=@Address Where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", updateFamily.Name, DbType.String);
            parameters.Add("Email", updateFamily.Email, DbType.String);
            parameters.Add("Contact", updateFamily.Contact, DbType.String);
            parameters.Add("Address", updateFamily.Address, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
