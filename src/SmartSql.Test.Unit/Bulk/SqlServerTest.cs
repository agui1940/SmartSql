﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SmartSql.Bulk.SqlServer;
using SmartSql.Bulk;
using System.Threading.Tasks;
using SmartSql.Test.Entities;

namespace SmartSql.Test.Unit.Bulk
{
    public class SqlServerTest : AbstractXmlConfigBuilderTest
    {
        [Fact]
        public void Insert()
        {
            var data = DbSession.GetDataTable(new RequestContext
            {
                Scope = nameof(AllPrimitive),
                SqlId = "Query",
                Request = new { Taken = 100 }
            });
            data.TableName = "T_AllPrimitive";
            IBulkInsert bulkInsert = new BulkInsert(DbSession);
            bulkInsert.Table = data;
            bulkInsert.Insert();
        }
        [Fact]
        public void InsertByList()
        {
            IBulkInsert bulkInsert = new BulkInsert(DbSession);
            var list = DbSession.Query<AllPrimitive>(new RequestContext
            {
                Scope = nameof(AllPrimitive),
                SqlId = "Query",
                Request = new { Taken = 100 }
            });
            bulkInsert.Insert(list);
        }
        [Fact]
        public async Task InsertAsync()
        {
            var data = await DbSession.GetDataTableAsync(new RequestContext
            {
                Scope = nameof(AllPrimitive),
                SqlId = "Query",
                Request = new { Taken = 100 }
            });
            data.TableName = "T_AllPrimitive";
            IBulkInsert bulkInsert = new BulkInsert(DbSession);
            bulkInsert.Table = data;
            await bulkInsert.InsertAsync();
        }
        [Fact]
        public async Task InsertByListAsync()
        {
            var list = await DbSession.QueryAsync<AllPrimitive>(new RequestContext
            {
                Scope = nameof(AllPrimitive),
                SqlId = "Query",
                Request = new { Taken = 100 }
            });
            IBulkInsert bulkInsert = new BulkInsert(DbSession);
            await bulkInsert.InsertAsync<AllPrimitive>(list);
        }
    }
}
