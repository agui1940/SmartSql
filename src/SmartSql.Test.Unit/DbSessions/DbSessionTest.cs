﻿using SmartSql.Configuration.Tags;
using SmartSql.Reflection;
using SmartSql.Test.Entities;
using System;
using System.Threading.Tasks;
using SmartSql.Data;
using Xunit;

namespace SmartSql.Test.Unit.DbSessions
{
    public class DbSessionTest : AbstractXmlConfigBuilderTest
    {
        #region Insert_From_RealSql
        private const string INSERT_SQL = @"INSERT INTO T_AllPrimitive
              (Boolean
              ,[Char]
              ,[Byte]
              ,[Int16]
              ,[Int32]
              ,[Int64]
              ,[Single]
              ,[Decimal]
              ,[DateTime]
              ,[String]
              ,[Guid]
              ,[TimeSpan]
              ,[NumericalEnum]
              ,[NullableBoolean]
              ,[NullableChar]
              ,[NullableByte]
              ,[NullableInt16]
              ,[NullableInt32]
              ,[NullableInt64]
              ,[NullableSingle]
              ,[NullableDouble]
              ,[NullableDecimal]
              ,[NullableDateTime]
              ,[NullableGuid]
              ,[NullableTimeSpan]
              ,[NullableNumericalEnum]
              ,[NullableString])
        VALUES
              (@Boolean
              ,@Char
              ,@Byte
              ,@Int16
              ,@Int32 
              ,@Int64
              ,@Single
              ,@Decimal
              ,@DateTime
              ,@String
              ,@Guid
              ,@TimeSpan
              ,@NumericalEnum
              ,@NullableBoolean
              ,@NullableChar
              ,@NullableByte
              ,@NullableInt16
              ,@NullableInt32
              ,@NullableInt64
              ,@NullableSingle
              ,@NullableDouble
              ,@NullableDecimal
              ,@NullableDateTime
              ,@NullableGuid
              ,@NullableTimeSpan
              ,@NullableNumericalEnum
              ,@NullableString);
        Select SCOPE_IDENTITY();";
        [Fact]
        public void Insert_From_RealSql()
        {
            var id = DbSession.ExecuteScalar<long>(new RequestContext
            {
                RealSql = INSERT_SQL,
                Request = new AllPrimitive
                {
                    DateTime = DateTime.Now,
                    String = "SmartSql",
                }
            });
        }
        #endregion

        [Fact]
        public void Insert()
        {
            var id = DbSession.ExecuteScalar<long>(new RequestContext
            {
                Scope = nameof(AllPrimitive),
                SqlId = "Insert",
                Request = new AllPrimitive
                {
                    DateTime = DateTime.Now,
                    String = "SmartSql",
                }
            });
        }
        [Fact]
        public void InsertByIdGen()
        {
            var id = DbSession.ExecuteScalar<long>(new RequestContext
            {
                Scope = nameof(AllPrimitive),
                SqlId = "InsertByIdGen",
                Request = new AllPrimitive
                {
                    DateTime = DateTime.Now,
                    String = "SmartSql",
                }
            });
        }

        [Fact]
        public async Task QueryAsync()
        {
            var list = await DbSession.QueryAsync<dynamic>(new RequestContext
            {
                RealSql = "SELECT Top (5) T.* From T_AllPrimitive T With(NoLock)"
            });

            Assert.NotNull(list);
        }

        [Fact]
        public async Task InsertAsync()
        {
            var id = await DbSession.ExecuteScalarAsync<long>(new RequestContext
            {
                Scope = nameof(AllPrimitive),
                SqlId = "Insert",
                Request = new AllPrimitive
                {
                    DateTime = DateTime.Now,
                    String = "SmartSql",
                }
            });
            Assert.NotEqual(0, id);
        }

        [Fact]
        public void InsertFromSqlParameters()
        {
            var insertParameters = RequestConvert.Instance.ToSqlParameters(new AllPrimitive
            {
                DateTime = DateTime.Now,
                String = "SmartSql",
            }, false);

            var id = DbSession.ExecuteScalar<long>(new RequestContext
            {
                RealSql = INSERT_SQL,
                Request = insertParameters
            });
            Assert.NotEqual(0, id);
        }

        //[Fact]
        public void Update()
        {
            var id = DbSession.ExecuteScalar<int>(new RequestContext
            {
                Scope = nameof(AllPrimitive),
                SqlId = "Update",
                Request = new AllPrimitive
                {
                    DateTime = DateTime.Now,
                    String = "SmartSql",
                }
            });
        }
        //[Fact]
        public void Delete()
        {
            var id = DbSession.ExecuteScalar<int>(new RequestContext
            {
                Scope = nameof(AllPrimitive),
                SqlId = "Delete",
                Request = new AllPrimitive
                {
                    DateTime = DateTime.Now,
                    String = "SmartSql",
                }
            });
        }
        [Fact]
        public void DeleteCheckIncludeRequired()
        {
            try
            {
                var id = DbSession.ExecuteScalar<int>(new RequestContext
                {
                    Scope = nameof(AllPrimitive),
                    SqlId = "DeleteCheckIncludeRequired",
                    Request = new { }
                });
            }
            catch (TagRequiredFailException ex)
            {
                Assert.True(true);
            }
        }
        [Fact]
        public void DeleteCheckIsNotEmptyRequired()
        {
            try
            {
                var id = DbSession.ExecuteScalar<int>(new RequestContext
                {
                    Scope = nameof(AllPrimitive),
                    SqlId = "DeleteCheckIsNotEmptyRequired",
                    Request = new { }
                });
            }
            catch (TagRequiredFailException ex)
            {
                Assert.True(true);
            }
        }




        //Create PROCEDURE[dbo].[SP_QueryUser]
        //@Total int = 0 Out
        //    AS
        //BEGIN
        //    SET NOCOUNT ON;
        //Set @Total = (Select Count(*) From T_User T With(NoLock));
        //SELECT Top 10 T.* From T_User T With(NoLock)
        //END

        [Fact]
        public void SP()
        {
            SqlParameterCollection dbParameterCollection = new SqlParameterCollection();
            dbParameterCollection.Add(new SqlParameter
            {
                Name = "Total",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Output
            });
            RequestContext context = new RequestContext
            {
                CommandType = System.Data.CommandType.StoredProcedure,
                RealSql = "SP_QueryUser",
                Request = dbParameterCollection
            };
            var list = DbSession.Query<User>(context);
            dbParameterCollection.TryGetParameterValue("Total", out int total);
        }

    }
}
