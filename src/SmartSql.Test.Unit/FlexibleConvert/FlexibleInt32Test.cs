﻿using System;
using System.Collections.Generic;
using System.Text;
using SmartSql.Test.Entities;
using Xunit;

namespace SmartSql.Test.Unit.FlexibleConvert
{
    public class FlexibleInt32Test : FlexibleTest
    {
        [Fact]
        public void Test()
        {
            var entity = DbSession.QuerySingle<FlexibleInt32>(new RequestContext
            {
                RealSql = SQL
            });
            Assert.Equal(1, entity.Int32);
        }
    }
}
