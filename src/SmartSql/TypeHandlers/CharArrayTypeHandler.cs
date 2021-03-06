﻿using SmartSql.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SmartSql.TypeHandlers
{
    public class CharArrayTypeHandler : AbstractTypeHandler<Char[], Char[]>
    {
        public override Char[] GetValue(DataReaderWrapper dataReader, int columnIndex, Type targetType)
        {
            int bufferLength = (int)dataReader.GetChars(columnIndex, 0, null, 0, 0);
            Char[] charArray = new Char[bufferLength];
            dataReader.GetChars(columnIndex, 0, charArray, 0, bufferLength);
            return charArray;
        }
    }
    public class CharArrayAnyTypeHandler : AbstractTypeHandler<Char[], AnyFieldType>
    {
        public override Char[] GetValue(DataReaderWrapper dataReader, int columnIndex, Type targetType)
        {
            int bufferLength = (int)dataReader.GetChars(columnIndex, 0, null, 0, 0);
            Char[] charArray = new Char[bufferLength];
            dataReader.GetChars(columnIndex, 0, charArray, 0, bufferLength);
            return charArray;
        }
    }
}
