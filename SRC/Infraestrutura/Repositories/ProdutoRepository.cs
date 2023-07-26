using Ltj.Domain.Interface.Repository;
using Ltj.Shared.Entities;
using MySqlConnector;
using System.Data;
using System.Data.Common;

namespace Infrastructure.Repositories
{
    public class ProdutoRepository :  IProdutoRepository
    {
        public AppDb Db { get; }

        public ProdutoRepository(AppDb db)
        {
            Db = db;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                Db.Connection.Open();
                using var txn = await Db.Connection.BeginTransactionAsync();
                using var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"Delete From `Produto` WHERE id = @idp ";
                                 
                                        
                
                cmd.Parameters.AddWithValue("@idp", id);

                cmd.Transaction = txn;
                await cmd.ExecuteNonQueryAsync();
                await txn.CommitAsync();
                return true;
            }
            catch (Exception) { return false; }
            finally { await Db.Connection.CloseAsync(); }

        }

        

        public async Task<ProdutoEntity> Get(string id) //inserir o try
        {
            try
            {
                Db.Connection.Open();
                using var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"SELECT * FROM PRODUTO WHERE id = @idp";

                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@id",
                    DbType = DbType.Int32,
                    Value = id,
                });
                return (await ReadAllAsync(await cmd.ExecuteReaderAsync())).First();
            }
            catch (Exception) { return new ProdutoEntity(); }
            finally { await Db.Connection.CloseAsync(); }

        }

        public async Task<List<ProdutoEntity>> GetAll()
        {
            try
            {
                Db.Connection.Open();
                using var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"SELECT `Id`, `Title`, `Content` FROM `TBL-Name` ORDER BY `Id` DESC LIMIT 10;";
                return await ReadAllAsync(await cmd.ExecuteReaderAsync());
            }
            catch (Exception) { return new List<ProdutoEntity>(); }
            finally{ await Db.Connection.CloseAsync(); }           
        }

        public async Task<bool> InsertAsync(ProdutoEntity prod)
        {            
            try
            {
                Db.Connection.Open();
                using var txn = await Db.Connection.BeginTransactionAsync();
                using var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `Produto` 
                                        (   `Marca`,`Nome`,
                                            `Tipo`,`Quantidade`,
                                            `PrecoCusto`,`PrecoVenda`  
                                        ) 
                                        VALUES (@Mar, @Nom,@Tip,@Quanti,@PreCusto,@PreVenda)";
                cmd.Parameters.AddWithValue("@Mar", prod.Marca);
                cmd.Parameters.AddWithValue("@Nom", prod.Nome);
                cmd.Parameters.AddWithValue("@Tip", prod.Tipo);
                cmd.Parameters.AddWithValue("@Quanti", prod.Quantidade);
                cmd.Parameters.AddWithValue("@PreCusto", prod.PrecoCusto);
                cmd.Parameters.AddWithValue("@PreVenda", prod.PrecoVenda);
                
                cmd.Transaction = txn;

                await cmd.ExecuteNonQueryAsync();
                await txn.CommitAsync();

                return true;
            }
            catch (Exception ) { return false; }
            finally { await Db.Connection.CloseAsync(); }
        }

        public async Task<bool> UpdateAsync(ProdutoEntity obj)
        {
            try
            {
                Db.Connection.Open();
                using var txn = await Db.Connection.BeginTransactionAsync();
                using var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"UPDATE `Produto` 
                                     set
                                        `Marca`= @Mar ,`Nome` = @Nom,
                                        `Tipo`= @Tip,`Quantidade` = @Quanti,
                                        `PrecoCusto`= @PreCusto,`PrecoVenda` = @PreVenda                                          
                                    WHERE id = @idp ";


                cmd.Parameters.AddWithValue("@Mar", obj.Marca);
                cmd.Parameters.AddWithValue("@Nom", obj.Nome);
                cmd.Parameters.AddWithValue("@Tip", obj.Tipo);
                cmd.Parameters.AddWithValue("@Quanti", obj.Quantidade);
                cmd.Parameters.AddWithValue("@PreCusto", obj.PrecoCusto);
                cmd.Parameters.AddWithValue("@PreVenda", obj.PrecoVenda);
                cmd.Parameters.AddWithValue("@idp", obj.Id);

                cmd.Transaction = txn;
                await cmd.ExecuteNonQueryAsync();
                await txn.CommitAsync();
                return true;
            }
            catch (Exception) { return false; }
            finally { await Db.Connection.CloseAsync(); }

           
        }



        

        private async Task<List<ProdutoEntity>> ReadAllAsync(DbDataReader reader)
        {
            var produtos = new List<ProdutoEntity>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var produto = new ProdutoEntity
                    {
                        Id = reader.GetInt32(0),
                        DtAlteracao = reader.GetDateTime(1),
                        DtInclusao= reader.GetDateTime(2),
                        Marca = reader.GetString(4),
                        Nome= reader.GetString(5),
                        PrecoCusto  =  reader.GetFloat(6),
                        PrecoVenda= reader.GetFloat(7),
                        Quantidade  =reader.GetInt32(8),
                        Tipo = reader.GetString(9)
                    };
                    produtos.Add(produto);
                }
            }
            return produtos;
        }

    }
}
