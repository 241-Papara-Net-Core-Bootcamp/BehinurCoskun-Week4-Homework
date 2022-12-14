using AutoMapper;
using Dapper;//ref ekledni ExecuteAsync ile
using Microsoft.Data.SqlClient;//ref eklendi SqlConnection ile
using Microsoft.Extensions.Configuration; // ref ekledi Configuration ile
using PaparaThirdWeek.Data.Abstracts;
using PaparaThirdWeek.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Data.Concretes
{
    
    public class DapperRepository<T> : IDapperRepository<T> where T : BaseEntity
    {
        public IConfiguration Configuration { get; } 

        public DapperRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<bool> Add(T entity)
        {
           var sql= @"INSERT INTO [dbo].[Companies]
           ([Name]
           ,[Adress]
           ,[City]
           ,[TaxNumber]
           ,[Email]
           ,[IsDeleted]
           ,[CreatedDate]
           ,[CreatedBy]
           ,[LastUpdateAt]
           ,[LastUpdateBy])
     VALUES(@Name, @Adress,@City,@TaxNumber,@Email,@IsDeleted,@CreatedDate,@CreatedBy,@LastUpdateAt,@LastUpdateBy)";
            try
            {
                using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                //connectionstring'ten connectionu çağırıdm 
                {
                    connection.Open();
                    await connection.ExecuteAsync(sql, entity);//execute komutu ef deki set metodunun yerine olan metot crud işlemlerini, gerçekleştirmektedir.
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<bool> DeleteById(int id)
        {
            var sql = "delete from Companies where Id=@Id";
            try
            {
                using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    await connection.ExecuteAsync(sql, new { Id = id });
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
        public async Task<IReadOnlyList<T>> GetAll()
        {
            var sql = "select * from Companies";
            using(var connection=new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<T>(sql);
                return result.ToList();
            }
        }
        public async Task<T> GetById(int id)
        {
            var sql = "select * from Companies where Id=@Id";
            string temp = Configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(temp))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<T>(sql, new { Id = id });
                return result;
            }
        }
        public async Task<bool> Update(T entity, int id)
        {
            var sql = "update Companies set Name=@Name, Adress=@Adress,City=@City,TaxNumber=@TaxNumber,Email=@Email,IsDeleted=@IsDeleted,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,LastUpdateAt=@LastUpdateAt,LastUpdateBy=@LastUpdateBy";
            try
            {
                using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    await connection.ExecuteAsync(sql, entity);
                    return true;
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
