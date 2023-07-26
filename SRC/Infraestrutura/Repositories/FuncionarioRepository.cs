using Ltj.Domain.Interface.Repository;
using Ltj.Shared.Entities;
using Ltj.Shared.Enum;
using MySqlConnector;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;

namespace Infrastructure.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        public AppDb Db { get; }

        public FuncionarioRepository(AppDb db)
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
                cmd.CommandText = @"Delete From `funcionario` WHERE id = @idp ";
                                 
                                        
                
                cmd.Parameters.AddWithValue("@idp", id);

                cmd.Transaction = txn;
                await cmd.ExecuteNonQueryAsync();
                await txn.CommitAsync();
                return true;
            }
            catch (Exception) { return false; }
            finally { await Db.Connection.CloseAsync(); }

        }

        

        public async Task<FuncionarioEntity> Get(string id) 
        {
            try
            {
                Db.Connection.Open();
                using var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"SELECT * FROM FUNCIONARIO WHERE id = @idp";
             
                cmd.Parameters.AddWithValue("@idp", id);
                return (await ReadAllAsync(await cmd.ExecuteReaderAsync())).First();
            }
            catch (Exception x) { return new FuncionarioEntity(); }
            finally { await Db.Connection.CloseAsync(); }

        }

        public async Task<List<FuncionarioEntity>> GetAll()
        {
            try
            {
                Db.Connection.Open();
                using var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"SELECT * FROM FUNCIONARIO;";

                return await ReadAllAsync(await cmd.ExecuteReaderAsync());
            }
            catch (Exception EX) { return new List<FuncionarioEntity>(); }
            finally{ await Db.Connection.CloseAsync(); }           
        }

        public async Task<bool> InsertAsync(FuncionarioEntity prod)
        {            
            try
            {
                Db.Connection.Open();
                using var txn = await Db.Connection.BeginTransactionAsync();
                using var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `Funcionario` 
                                        (   `Nome`,`CPF`,`Pis`,
                                            `Sexo`,`Status`,
                                            `Motivo`, `DtNascimento`
                                        ) 
                                        VALUES ( @NomFun,@CpfFun,@PisFun,@SexoFun,@StatusFun,@MotivoFun, @DtNascimentofun)";

                

                cmd.Parameters.AddWithValue("@NomFun", prod.Nome);
                cmd.Parameters.AddWithValue("@CpfFun", prod.CPF);
                cmd.Parameters.AddWithValue("@PisFun", prod.PIS);
                //cmd.Parameters.AddWithValue("@IdadeFun", prod.DtNascimento);
                cmd.Parameters.AddWithValue("@SexoFun", (prod.Sexo.ToString()));
                cmd.Parameters.AddWithValue("@StatusFun", (prod.Status.ToString()));
                cmd.Parameters.AddWithValue("@MotivoFun", prod.Motivo);
                cmd.Parameters.AddWithValue("@DtNascimentoFun", prod.DtNascimento);


                cmd.Transaction = txn;

                await cmd.ExecuteNonQueryAsync();
                await txn.CommitAsync();

                return true;
            }
            catch (Exception ex) { return false; }
            finally { await Db.Connection.CloseAsync(); }
        }

        public async Task<bool> UpdateAsync(FuncionarioEntity obj)
        {
            try
            {
                Db.Connection.Open();
                using var txn = await Db.Connection.BeginTransactionAsync();
                using var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"UPDATE `funcionario` 
                                     set
                                        `Nome`= @NomFun ,`CPF` = @CpfFun,
                                        `Pis`= @PisFun,
                                        `Sexo`= @SexoFun,`Status`= @StatusFun, 
                                        `Motivo`= @MotivoFun, `DtNascimento` = @DtNascimento,

                                     WHERE id = @idp ";


                cmd.Parameters.AddWithValue("@NomFun", obj.Nome);
                cmd.Parameters.AddWithValue("@CpfFun", obj.CPF);
                cmd.Parameters.AddWithValue("@PisFun", obj.PIS);
                //cmd.Parameters.AddWithValue("@IdadeFun", obj.DtNascimento);
                cmd.Parameters.AddWithValue("@SexoFun", obj.Sexo);
                cmd.Parameters.AddWithValue("@StatusFun", obj.Status);
                cmd.Parameters.AddWithValue("@MotivoFun", obj.Motivo);
                cmd.Parameters.AddWithValue("@DtNascimentoFun", obj.DtNascimento);

                cmd.Parameters.AddWithValue("@idp", obj.Id);

                cmd.Transaction = txn;
                await cmd.ExecuteNonQueryAsync();
                await txn.CommitAsync();
                return true;
            }
            catch (Exception ex) {
                return false;
            }
            finally { await Db.Connection.CloseAsync(); }

           
        }



        private async Task<List<FuncionarioEntity>> ReadAllAsync(DbDataReader reader)
        {
            var funcionarios = new List<FuncionarioEntity>();

            using (reader)
            {
                var idOrdinal = reader.GetOrdinal("id");
                var nomeOrdinal = reader.GetOrdinal("nome");
                var cpfOrdinal = reader.GetOrdinal("cpf");
                var pisOrdinal = reader.GetOrdinal("pis");
                var sexoOrdinal = reader.GetOrdinal("sexo");
                var statusOrdinal = reader.GetOrdinal("status");
                var motivoOrdinal = reader.GetOrdinal("motivo");
                var dtNascimentoOrdinal = reader.GetOrdinal("dtnascimento");

                while (await reader.ReadAsync())
                {
                    var funcionario = new FuncionarioEntity
                    {
                        Id = reader.GetInt32(idOrdinal),
                        Nome = reader.GetString(nomeOrdinal),
                        CPF = reader.GetString(cpfOrdinal),
                        PIS = reader.GetString(pisOrdinal),
                        Sexo = string.IsNullOrEmpty(reader[sexoOrdinal].ToString()) ? Sexo.Masculino : (Sexo)Enum.Parse(typeof(Sexo), reader.GetString(sexoOrdinal)),
                        Status = string.IsNullOrEmpty(reader[statusOrdinal].ToString()) ? StatusFuncionario.Ativo : (StatusFuncionario)Enum.Parse(typeof(StatusFuncionario), reader.GetString(statusOrdinal)),
                        Motivo = reader[motivoOrdinal].ToString(),
                        DtNascimento = reader.GetDateTime(dtNascimentoOrdinal)
                    };

                    funcionarios.Add(funcionario);
                }
            }
            return funcionarios;
        }


    }
}
